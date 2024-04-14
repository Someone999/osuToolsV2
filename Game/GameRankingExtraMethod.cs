namespace osuToolsV2.Game;

public static class GameRankingExtraMethod
{
    public static bool HigherThan(this GameGrade a, GameGrade gameGrade)
    {
        return (int) a > (int) gameGrade;
    }
    
    public static bool HigherOrEquals(this GameGrade a, GameGrade gameGrade)
    {
        return (int) a >= (int) gameGrade;
    }
}