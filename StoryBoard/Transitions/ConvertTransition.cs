using System.Text;

namespace osuToolsV2.StoryBoard.Transitions
{
    public class ConvertTransition: ITransition
    {
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public List<double> StartTransition { get; internal set; } = new();
        public List<double> EndTransition { get; internal set; } = new();
        public override string ToString()
        {
            StringBuilder st = new StringBuilder(),end = new StringBuilder();
            st.Append('(');
            end.Append('(');
            for (int i = 0; i < StartTransition.Count; i++)
            {
                st.Append(StartTransition[i]);
                end.Append(EndTransition[i]);
                if (i < EndTransition.Count - 1)
                {
                    st.Append(',');
                    end.Append(',');
                }
            }
            st.Append(')');
            end.Append(')');
            return st + " -> " + end;
        }
    }

}
