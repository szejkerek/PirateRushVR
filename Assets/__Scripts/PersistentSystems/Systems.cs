using System;
using UnityEngine;

public class Systems : Singleton<Systems>
{
    public string Nickname => nickname;
    string nickname;
    public bool KatanaRight = true;
    public int TickRate = 32;
    public DifficultyLevel difficultyLevel;

    protected override void Awake()
    {
        base.Awake();
        nickname = GetLastNickname();
    }
    string GetLastNickname()
    {
        if (PlayerPrefs.HasKey("nickname"))
            return PlayerPrefs.GetString("nickname");

        string uuid = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0,4);
        return $"Guest{uuid}";
    }

    public void SetNickname(string value)
    {
        PlayerPrefs.SetString("nickname", value);
        nickname = value;
    }
}
