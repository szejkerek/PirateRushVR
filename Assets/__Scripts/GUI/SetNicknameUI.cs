using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetNicknameUI : MonoBehaviour
{
    [SerializeField] int nicknameMaxLength = 15;
    [SerializeField] TMP_InputField nicknameInput;
    [SerializeField] Button generateGuestBtn;
    [SerializeField] Button selectBtn;
    [SerializeField] UIWarning nicknameWarning;

    private void Awake()
    {
        generateGuestBtn.onClick.AddListener(GenerateGuestNick);
        selectBtn.onClick.AddListener(SetNicknameFromUI);
    }

    public void GenerateGuestNick()
    {
        string uuid = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 5);
        nicknameInput.text =  $"Guest-{uuid}";
    }

    private void OnEnable()
    {
        nicknameInput.text = Systems.Instance.Nickname;
    }

    public void SetNicknameFromUI()
    {
        if (nicknameInput.text == "")
        {
            nicknameWarning.ShowWarning("Nickname cannot be empty!");
            return;
        }

        if(nicknameInput.text.Length > nicknameMaxLength)
        {
            nicknameWarning.ShowWarning("Nickname is too long!");
            return;
        }

        Systems.Instance.SetNickname(nicknameInput.text);
        
    }
}
