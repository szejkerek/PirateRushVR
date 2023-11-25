public class ScoreManager : Singleton<ScoreManager>
{
    float multiplier = 0;
    HighscoreEntry entry;
    Leaderboard leaderboard;

    protected override void Awake()
    {
        base.Awake();
        entry = new HighscoreEntry(0, Systems.Instance.Nickname);
        leaderboard = new Leaderboard();
        leaderboard.Load();
    }

    public void AddPoints(float points)
    {
        leaderboard.UpdateScore(entry);
    }
}
