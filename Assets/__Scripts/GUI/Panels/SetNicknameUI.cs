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

    private void Start()
    {
        generateGuestBtn.onClick.AddListener(GenerateGuestNickButton);
        selectBtn.onClick.AddListener(SetNicknameFromUI);
    }

    public void GenerateGuestNickButton()
    {    
        nicknameInput.text = GenerateGuestNickname();
    }

    public static string GenerateGuestNickname()
    {
        string uuid = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 5);
        return $"Guest-{uuid}";
    }

    private void OnEnable()
    {
        string nickname = GlobalSettingManager.Instance.GetNickname();

        if(String.IsNullOrEmpty(nickname))
        {
            nicknameInput.text = GenerateGuestNickname();
        }
        nicknameInput.text = nickname;
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

        GlobalSettingManager.Instance.SetNickname(nicknameInput.text);
        
    }
}
