﻿using System.Text;
using osuToolsV2.Graphic;
using osuToolsV2.StoryBoard.Commands;
using osuToolsV2.StoryBoard.Commands.Resources;
using osuToolsV2.StoryBoard.Enums;
using osuToolsV2.StoryBoard.Transitions;
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

        /*static int GetCommaCount(string str)
        {
            int comma = 0;
            int quote = 0;
            for (int i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                if (ch == ',')
                {
                    if (quote % 2 != 0)
                    {
                        continue;
                    }
                    comma++;
                }

                if (ch == '\"')
                {
                    quote++;
                }
            }

            if (quote % 2 != 0)
            {
                throw new FormatException("Quote not match");
            }
            return comma;
        }
        static int[] GetCommaIdx(string str)
        {
            int commaCount = GetCommaCount(str);
            int[] commas = new int[commaCount + 1];
            commas[0] = 0;
            int comma = 1;
            int quote = 0;
            for (int i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                if (ch == ',')
                {
                    if (quote % 2 != 0)
                    {
                        continue;
                    }
                    commas[comma++] = i;
                }

                if (ch == '\"')
                {
                    quote++;
                }
            }
            if (quote % 2 != 0)
            {
                throw new FormatException("Quote not match");
            }
            return commas;
        }
        static string[] ArgumentParser(string str)
        {
            int[] commaIdx = GetCommaIdx(str);
            string[] args = new string[commaIdx.Length];
            for (int i = 0; i < commaIdx.Length; i++)
            {
                if (i == 0)
                {
                    args[i] = str.Substring(0, commaIdx[1]);
                }
                else if (i != commaIdx.Length - 1)
                {
                    int len = commaIdx[i + 1] - commaIdx[i];
                    args[i] = str.Substring(commaIdx[i] + 1, len - 1);
                }
                else
                {
                    args[i] = str.Substring(commaIdx[i] + 1);
                }
            }
            return args;
        }*/

        static string[] ArgumentParser(string str)
        {
            List<string> args = new List<string>();
            Stack<bool> quoteStack = new Stack<bool>();
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

            return args.ToArray();
        }

        public StoryBoardCommandBase[] Parse()
        {
            //Stack<StoryBoardCommandBase> commandStack = new Stack<StoryBoardCommandBase>();
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
                string[] parts = ArgumentParser(nLine);
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
            return commands.ToArray();
        }

        StoryBoardCommandBase? CommandSelector(string cmd, string[] data, StoryBoardCommandBase? parent)
        {
            StoryBoardCommandBase? command = ParseMainCommand(cmd, data);
            command ??= ParseSubStoryBoardCommand(cmd, data, parent);
            return command;
        }

        MainStoryBoardCommand? ParseMainCommand(string type, string[] data)
        {
            var origin = (StoryBoardOrigin)Enum.Parse(typeof(StoryBoardOrigin),data[2]);
            switch (type)
            {
                case "Background":
                case "0":
                    return new Background()
                    {
                        Layer = (StoryBoardLayer)Enum.Parse(typeof(StoryBoardLayer), data[1]),
                        FileName = data[2].Trim('\"'),
                        Position = new OsuPixel(double.Parse(data[3]), double.Parse(data[4]))
                    };
                case "Video":
                case "1":
                    return new Video()
                    {
                        StartTime = double.Parse(data[1]),
                        FileName = data[2].Trim('\"')
                    };
                case "Colour":
                case "3":
                    return null;
                case "Sprite":
                case "4":
                    return new Sprite
                    {
                        Layer = (StoryBoardLayer)Enum.Parse(typeof(StoryBoardLayer),data[1]),
                        Origin = origin,
                        FileName = data[3].Trim('\"'),
                        Position = new OsuPixel(double.Parse(data[4]),double.Parse(data[5]))
                    };
                case "Sample":
                case "5":
                    return new Sample
                    {
                        StartTime = double.Parse(data[1]),
                        Layer = (StoryBoardLayer)Enum.Parse(typeof(StoryBoardLayer),data[2]),
                        FileName = data[3].Trim('\"'),
                        Volume = double.Parse(data[4])
                    };
                case "Animation":
                case "6":

                    StoryBoardAnimationLoopType loopType = StoryBoardAnimationLoopType.LoopOnce;
                    if (data.Length > 8)
                    {
                        loopType = (StoryBoardAnimationLoopType)Enum.Parse(typeof(StoryBoardAnimationLoopType),
                            data[8]);
                    }
                    
                    return new Animation
                    {
                        Layer = (StoryBoardLayer)Enum.Parse(typeof(StoryBoardLayer),data[1]),
                        Origin = origin,
                        FileName = data[3].Trim('\"'),
                        Position = new OsuPixel(double.Parse(data[4]), double.Parse(data[5])),
                        FrameCount = int.Parse(data[6]),
                        FrameDelay = double.Parse(data[7]),
                        LoopType = loopType
                    };
                default: return null;
            }
        }

        SubStoryBoardCommand? ParseSubStoryBoardCommand(string type, string[] data, StoryBoardCommandBase? parent)
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
