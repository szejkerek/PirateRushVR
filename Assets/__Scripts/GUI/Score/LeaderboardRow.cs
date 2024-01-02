using TMPro;
using UnityEngine;

/// <summary>
/// Represents a row in the leaderboard, displaying a high score entry.
/// </summary>
public class LeaderboardRow : MonoBehaviour
{
    [SerializeField] TMP_Text Lp;
    [SerializeField] TMP_Text Nickname;
    [SerializeField] TMP_Text Score;

    /// <summary>
    /// Initializes the leaderboard row with the provided data.
    /// </summary>
    /// <param name="lp">The position in the leaderboard.</param>
    /// <param name="entry">The high score entry containing the player's data.</param>
    public void Init(int lp, HighscoreEntry entry)
    {
        Lp.text = lp.ToString();
        Nickname.text = entry.Nickname;
        Score.text = Mathf.CeilToInt(entry.Score).ToString();

        // Highlight the current player's entry in the leaderboard
        if (GlobalSettingManager.Instance.GetNickname() == entry.Nickname)
        {
            Lp.color = Color.yellow;
            Score.color = Color.yellow;
            Nickname.color = Color.yellow;
        }
    }
}
