using osuToolsV2.Replays;

namespace osuToolsV2.Utils;

public class Conditions
{
    public bool Result { get; private set; }
    public void Or(bool val) => Result = Result || val;
    public void And(bool val) => Result = Result && val;
    public void Reset(bool initState = false) => Result = initState;
    public Conditions(bool initState = false) => Reset(initState);
}