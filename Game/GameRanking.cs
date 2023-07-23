namespace osuToolsV2.Game;

public static class GameRankingExtraMethod
{
    public static bool HigherThan(this GameRanking a, GameRanking gameRanking)
    {
        return (int) a > (int) gameRanking;
    }
    
    public static bool HigherOrEquals(this GameRanking a, GameRanking gameRanking)
    {
        return (int) a >= (int) gameRanking;
    }
}
public enum GameRanking
{
    XH = 8,
    X = 7,
    SH = 6,
    S = 5,
    A = 4,
    B = 3,
    C = 2,
    D = 1
}