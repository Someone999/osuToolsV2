using System.Text;
using osuToolsV2.Graphic;
using osuToolsV2.StoryBoard.Commands;
using osuToolsV2.StoryBoard.Commands.Resources;
using osuToolsV2.StoryBoard.Enums;
using osuToolsV2.StoryBoard.Transitions.TransitionCreators;

namespace osuToolsV2.StoryBoard
{
    public class StoryBoardCommandParser
    {
        private string[] _lines;
        public StoryBoardCommandParser(string file)
        {
            string[] lines = File.ReadAllLines(file);
            _lines = lines;
        }

        public StoryBoardCommandParser(string[] lines)
        {
            _lines = lines;
        }

        static bool IsValid(string s)
        {
            return !s.StartsWith("[") && !s.StartsWith("//");
        }

        static int GetSpaceCount(string s)
        {
            int i = 0;
            while (s[i] == ' ')
            {
                i++;
            }
            return i;
        }

        // internal static IReadOnlyList<int> GetIndexes(string s)
        // {
        //     int i = 0;
        //     List<int> indexes = new List<int>();
        //     indexes.Add(0);
        //     bool isFirst = true;
        //     while (i < s.Length)
        //     {
        //         if (isFirst)
        //         {
        //             i++;
        //         }
        //         
        //         if (i >= s.Length - 1)
        //         {
        //             indexes.Add(i);
        //             break;
        //         }
        //         
        //         if (s[i] == ',')
        //         {
        //             indexes.Add(i);
        //             while (true)
        //             {
        //                
        //                 
        //                 i++;
        //                 if (s[i] == ',')
        //                 {
        //                     indexes.Add(i);
        //                     i++;
        //                     break;
        //                 }
        //             }
        //         }
        //     }
        //     return indexes.AsReadOnly();
        // }
        
        static IReadOnlyList<string> ArgumentParser(string str)
        {
            List<string> args = new List<string>();
            StringBuilder currentToken = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case ',':
                        args.Add(currentToken.ToString());
                        currentToken.Clear();
                        break;
                    case '"':
                        i++;
                        while (i < str.Length && str[i] != '"')
                        {
                            currentToken.Append(str[i]);
                            i++;
                        }
                        
                        break;
                    default:
                        currentToken.Append(str[i]);
                        break;
                }
            }

            if (currentToken.Length > 0)
            {
                args.Add(currentToken.ToString());
            }

            return args.AsReadOnly();
        }

        public IReadOnlyList<StoryBoardCommandBase> Parse()
        {
            List<StoryBoardCommandBase> commands = new();
            StoryBoardCommandBase? parentCommand = null;
            int lastSpace = int.MaxValue;
            StoryBoardCommandBase? lastCommand = null;
            foreach (var line in _lines)
            {
                if (!IsValid(line))
                {
                    continue;
                }
                int space = GetSpaceCount(line);
                var nLine = line.TrimStart();
                var parts = ArgumentParser(nLine);
                string type = parts[0];
                StoryBoardCommandBase? currentCommand = CommandSelector(type, parts, parentCommand);
                if (currentCommand == null)
                {
                    continue;
                }
                if (space == 0)
                {
                    parentCommand = currentCommand;
                    commands.Add(currentCommand);
                }
                else if (space > lastSpace)
                {
                    currentCommand.AsSubStoryBoardCommand().Parent = lastCommand;
                    lastCommand?.SubCommands.Add(currentCommand.AsSubStoryBoardCommand());
                }
                else if (space < lastSpace)
                {
                    parentCommand = lastCommand?.AsSubStoryBoardCommand().Parent;
                    parentCommand?.SubCommands.Add(currentCommand.AsSubStoryBoardCommand());
                    currentCommand.AsSubStoryBoardCommand().Parent = parentCommand;
                }
                else
                {
                    parentCommand?.SubCommands.Add(currentCommand.AsSubStoryBoardCommand());
                    currentCommand.AsSubStoryBoardCommand().Parent = parentCommand;
                }
                lastCommand = currentCommand;
                lastSpace = space;
            }
            
            //GC.Collect();
            return commands.AsReadOnly();
        }

        StoryBoardCommandBase? CommandSelector(string cmd, IReadOnlyList<string> data, StoryBoardCommandBase? parent)
        {
            StoryBoardCommandBase? command = ParseMainCommand(cmd, data);
            command ??= ParseSubStoryBoardCommand(cmd, data, parent);
            return command;
        }

        bool TryGetLayerReflect(IReadOnlyList<string> args, out StoryBoardLayer layer)
        {
            var layerStr = args[1];
            return Enum.TryParse(layerStr, out layer);
        }


        double ParseDouble(ReadOnlySpan<char> span)
        {
            return double.Parse(span/*, NumberStyles.Float, CultureInfo.InvariantCulture*/);
        }
        
        
        MainStoryBoardCommand? ParseMainCommand(string type, IReadOnlyList<string> data)
        {
            TryGetLayerReflect(data, out var layer);
            switch (type)
            {
                case "0":
                case "Background":
                    return new Background()
                    {
                        Layer = layer,
                        FileName = data[2].Trim('\"'),
                        Position = new OsuPixel(ParseDouble(data[3]), ParseDouble(data[4]))
                    };
                case "1":
                case "Video":
                    return new Video()
                    {
                        StartTime = double.Parse(data[1]),
                        FileName = data[2].Trim('\"')
                    };
                case "3":
                case "Colour":
                    return null;
                case "4":
                case "Sprite":
                    return new Sprite
                    {
                        Layer = layer,
                        Origin = (StoryBoardOrigin)Enum.Parse(typeof(StoryBoardOrigin),data[2]),
                        FileName = data[3].Trim('\"'),
                        Position = new OsuPixel(ParseDouble(data[4]), ParseDouble(data[5]))
                    };
                case "5":
                case "Sample":
                    return new Sample
                    {
                        StartTime = double.Parse(data[1]),
                        Layer = layer,
                        FileName = data[3].Trim('\"'),
                        Volume = double.Parse(data[4])
                    };
                case "6":
                case "Animation":
                    StoryBoardAnimationLoopType loopType = StoryBoardAnimationLoopType.LoopOnce;
                    if (data.Count > 8)
                    {
                        loopType = (StoryBoardAnimationLoopType)Enum.Parse(typeof(StoryBoardAnimationLoopType),
                            data[8]);
                    }
                    
                    return new Animation
                    {
                        Layer = layer,
                        Origin = (StoryBoardOrigin)Enum.Parse(typeof(StoryBoardOrigin),data[2]),
                        FileName = data[3].Trim('\"'),
                        Position = new OsuPixel(ParseDouble(data[6]), ParseDouble(data[7])),
                        FrameCount = int.Parse(data[6]),
                        FrameDelay = double.Parse(data[7]),
                        LoopType = loopType
                    };
                default: return null;
            }
        }
        

        SubStoryBoardCommand? ParseSubStoryBoardCommand(string type, IReadOnlyList<string> data, StoryBoardCommandBase? parent)
        {
            if (type != "L" && type != "T")
            {
                var easing = (StoryBoardEasing)Enum.Parse(typeof(StoryBoardEasing),data[1]);
                double startTime = double.Parse(data[2]), endTime = string.IsNullOrEmpty(data[3]) ? startTime : double.Parse(data[3]);
                switch (type)
                {
                    case "C":
                        Color color = new Color
                        {
                            Easing = easing,
                            EndTime = endTime,
                            StartTime = startTime,
                            Parent = parent,
                            Transitions = ColorTransitionGenerator.Instance.Create(startTime, endTime, data)
                        };
                        
                        return color;
                    case "F":
                        Fade fade = new Fade
                        {
                            Easing = easing,
                            StartTime = startTime,
                            EndTime = endTime,
                            Parent = parent,
                            Transitions = FadeTransitionGenerator.Instance.Create(startTime, endTime, data)
                        };
                        
                        return fade;
                    case "M":
                        Move m = new Move
                        {
                            Easing = easing,
                            StartTime = startTime,
                            EndTime = endTime,
                            Parent = parent,
                            Transitions = MoveTransitionGenerator.Instance.Create(startTime, endTime, data)
                        };
                       

                        return m;
                    case "MX":
                        MoveX moveX = new MoveX
                        {
                            Easing = easing,
                            StartTime = startTime,
                            EndTime = endTime,
                            Parent = parent,
                            Transitions = MoveXTransitionGenerator.Instance.Create(startTime, endTime, data)
                        };
                        return moveX;
                    case "MY":
                        MoveY moveY = new MoveY
                        {
                            Easing = easing,
                            StartTime = startTime,
                            EndTime = endTime,
                            Parent = parent,
                            Transitions = MoveYTransitionGenerator.Instance.Create(startTime, endTime, data)
                        };
                        
                        return moveY;
                    case "P":
                        Parameter parameter = new Parameter
                        {
                            Easing = easing,
                            StartTime = startTime,
                            EndTime = endTime,
                            Parent = parent,
                            ParameterOperation = data[4] switch
                            {
                                "H" => ParameterOperation.HorizentalFlip,
                                "V" => ParameterOperation.VerticalFlip,
                                "A" => ParameterOperation.AddictiveColorBlend,
                                _ => ParameterOperation.None
                            }
                        };
                        return parameter;
                    case "R":
                        Rotate rotate = new Rotate
                        {
                            Easing = easing,
                            StartTime = startTime,
                            EndTime = endTime,
                            Parent = parent,
                            Transitions = RotateTransitionGenerator.Instance.Create(startTime, endTime, data)
                        };
                       
                        return rotate;
                    case "S":
                        Scale scale = new Scale
                        {
                            Easing = easing,
                            StartTime = startTime,
                            EndTime = endTime,
                            Parent = parent,
                            Transitions = ScaleTransitionGenerator.Instance.Create(startTime, endTime, data)
                        };
                        return scale;
                    case "V":
                        VectorScale vectorScale = new VectorScale
                        {
                            Easing = easing,
                            StartTime = startTime,
                            EndTime = endTime,
                            Parent = parent,
                            Transitions = VectorScaleTransitionGenerator.Instance.Create(startTime, endTime, data)
                        };
                        return vectorScale;
                    default: return null;
                }
            }

            switch (type)
            {
                case "T":
                    Trigger trigger = new Trigger
                    {
                        TriggerType = data[1],
                        StartTime = double.Parse(data[2]),
                        EndTime = double.Parse(data[3]),
                        Parent = parent
                        
                    };
                    return trigger;
                case "L":
                    Loop loop = new Loop
                    {
                        StartTime = double.Parse(data[1]),
                        LoopCount = int.Parse(data[2]),
                        Parent = parent
                    };
                    return loop;
                default: return null;
            }
        }
    }
}
