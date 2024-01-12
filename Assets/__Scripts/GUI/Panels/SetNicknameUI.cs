using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI class responsible for setting and managing player nicknames.
/// </summary>
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

    /// <summary>
    /// Generates a guest nickname and sets it in the input field.
    /// </summary>
    public void GenerateGuestNickButton()
    {
        nicknameInput.text = GenerateGuestNickname();
    }

    /// <summary>
    /// Generates a random guest nickname.
    /// </summary>
    public static string GenerateGuestNickname()
    {
        string uuid = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 5);
        return $"Guest-{uuid}";
    }

    private void OnEnable()
    {
        string nickname = GlobalSettingManager.Instance.GetNickname();

        if (String.IsNullOrEmpty(nickname))
        {
            nicknameInput.text = GenerateGuestNickname();
        }
        else
        {
            nicknameInput.text = nickname;
        }
    }

    /// <summary>
    /// Sets the nickname based on the UI input field.
    /// </summary>
    public void SetNicknameFromUI()
    {
        if (nicknameInput.text == "")
        {
            nicknameWarning.ShowWarning("Nickname cannot be empty!");
            return;
        }

        if (nicknameInput.text.Length > nicknameMaxLength)
        {
            nicknameWarning.ShowWarning("Nickname is too long!");
            return;
        }

        GlobalSettingManager.Instance.SetNickname(nicknameInput.text);
    }
}
