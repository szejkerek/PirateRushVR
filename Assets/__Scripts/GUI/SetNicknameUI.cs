using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetNicknameUI : MonoBehaviour
{
    public TMP_InputField nicknameInput;
    public string GenerateGuestNick()
    {
        string uuid = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 4);
        return $"Guest{uuid}";
    }
}
