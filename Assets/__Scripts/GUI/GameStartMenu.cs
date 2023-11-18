using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject selectDifficulty;
    [SerializeField] private GameObject selectWeapon;
    [SerializeField] private GameObject selectNickname;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject about;

    [Header("Main Menu Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button aboutButton;
    [SerializeField] private Button quitButton;

    [Header("Select weapon Buttons")]
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;    
    
    [Header("Select nickname")]
    [SerializeField] private Button selectNicknameBtn;
    [SerializeField] private TMP_InputField nicknameInputField;
    [SerializeField] private UIWarning nicknameWarning;

    [Space]
    [SerializeField] private List<Button> returnButtons;

    void Awake()
    {
        EnablePlayerUIRays();
        EnableView(mainMenu);

        startButton.onClick.AddListener(() =>{ 
            EnableView(selectNickname);
            nicknameInputField.text = Systems.Instance.Nickname;
        });
        selectNicknameBtn.onClick.AddListener(() => { 
            if(nicknameInputField.text != "")
            {
                EnableView(selectDifficulty);
                Systems.Instance.SetNickname(nicknameInputField.text);
            }
            else
            {
                nicknameWarning.ShowWarning("Nickname cannot be empty!");
            }
            
        });

        aboutButton.onClick.AddListener(() => EnableView(about));
        optionButton.onClick.AddListener(() => EnableView(options));
        rightButton.onClick.AddListener(() => StartGame(rightHand: true));
        leftButton.onClick.AddListener(() => StartGame(rightHand: false));
        quitButton.onClick.AddListener(QuitGame);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(() => EnableView(mainMenu));
        }
    }

    private void EnablePlayerUIRays()
    {
        SetPlayerPreferences setPlayer = FindObjectOfType<SetPlayerPreferences>();
        setPlayer.SetHandItems(HandHeldType.UIRays);
    }

    public void StartGame(bool rightHand)
    {
        HideAll();
        Systems.Instance.KatanaRight = rightHand;
        SceneLoader.Instance.LoadScene(SceneEnum.Gameplay);
    }    

    public void SetDifficulty(DifficultyLevel difficultySO)
    {
        Systems.Instance.difficultyLevel = difficultySO;
        EnableView(selectWeapon);
    }

    public void EnableView(GameObject view)
    {
        HideAll();
        view.SetActive(true);
    }    

    public void QuitGame()
    {
        Application.Quit();
    }

    private void HideAll()
    {
        mainMenu.SetActive(false);
        selectNickname.SetActive(false);
        options.SetActive(false);
        selectDifficulty.SetActive(false);
        about.SetActive(false);
        selectWeapon.SetActive(false);
    }
}
