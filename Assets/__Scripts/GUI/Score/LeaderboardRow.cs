using TMPro;
using UnityEngine;

public class LeaderboardRow : MonoBehaviour
{
    [SerializeField] TMP_Text Lp;
    [SerializeField] TMP_Text Nickname;
    [SerializeField] TMP_Text Score;

    public void Init(int lp,HighscoreEntry entry)
    {
        Lp.text = lp.ToString();
        Nickname.text = entry.Nickname;
        Score.text = Mathf.CeilToInt(entry.Score).ToString();

        if (GlobalSettingManager.Instance.GetNickname() == entry.Nickname)
        {
            Lp.color = Color.yellow;
            Score.color = Color.yellow;
            Nickname.color = Color.yellow;
        }
    }
}
