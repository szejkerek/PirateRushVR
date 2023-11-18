using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetNicknameUI : MonoBehaviour
{
    [SerializeField] TMP_InputField nicknameInput;
    [SerializeField] Button generateGuestBtn;
    [SerializeField] Button selectBtn;

    private void Awake()
    {
        generateGuestBtn.onClick.AddListener(GenerateGuestNick);
    }

    public void GenerateGuestNick()
    {
        string uuid = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 5);
        nicknameInput.text =  $"Guest{uuid}";
    }
}
