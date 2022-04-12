using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace osuToolsV2.StoryBoard.Transitions
{
    public interface ITypeInitializationInfo<out T>
    {
        Type TargetType { get; }
        T CreateInstance(double[] startTransitions, double[] endTransitions, double startTime, double endTime);
    }
    public static class TransitionParser
    {
        public static List<ITransition> GenericTransitionsParser<T>(double startTime, double endTime, string[] data, int elementCount,ITypeInitializationInfo<T> initInfo) where T:ITransition
        {
            List<ITransition> transitions = new List<ITransition>();
            Type runtimeType = typeof(T);
            if (data.Length % elementCount != 0 || elementCount < 1)
            {
                throw new InvalidOperationException($"Data can not be splited into {data.Length / elementCount} parts.");
            }

            double[] current = new double[elementCount];
            var next = new double[elementCount];
            for (int i = 0; i < data.Length / elementCount; i++)
            {
                
                double startTm = System.Math.Abs(endTime - startTime) < double.Epsilon ? endTime : startTime + (endTime - startTime) * i,
                    endTm = endTime + (endTime - startTime) * i;
                for (int j = 0; j < elementCount; j++)
                {
                    next[j] = double.Parse(data[i * elementCount + j]);
                }

                int len = elementCount + 2;
                transitions.Add(initInfo.CreateInstance(current, next, startTm, endTm));
                next.CopyTo(current, 0);
            }
            return transitions;
        }
    }
}
