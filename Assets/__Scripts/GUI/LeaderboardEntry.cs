using TMPro;
using UnityEngine;

public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField] TMP_Text Nickname;
    [SerializeField] TMP_Text Score;

    public void Init(HighscoreEntry entry)
    {
        Nickname.text = entry.Nickname;
        Score.text = Mathf.CeilToInt(entry.Score).ToString();
    }
}
