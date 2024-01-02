using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the game start menu functionality.
/// </summary>
public class GameStartMenu : MonoBehaviour
{
    [SerializeField] Sound gameplayMusic;
    [SerializeField] Sound menuMusic;

    // UI Pages
    [Header("UI Pages")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject selectDifficulty;
    [SerializeField] private GameObject selectWeapon;
    [SerializeField] private GameObject selectNickname;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject about;

    // Main Menu Buttons
    [Header("Main Menu Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button aboutButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button quitButton;

    // Select weapon Buttons
    [Header("Select weapon Buttons")]
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;

    // Select nickname
    [Header("Select nickname")]
    [SerializeField] private Button selectNicknameBtn;
    [SerializeField] private TMP_InputField nicknameInputField;

    [Space]
    [SerializeField] private List<Button> returnButtons;

    void Awake()
    {
        EnablePlayerUIRays();
        EnableView(mainMenu);

        // Adding listeners to buttons
        startButton.onClick.AddListener(() => EnableView(selectNickname));
        selectNicknameBtn.onClick.AddListener(() => EnableView(selectDifficulty));
        aboutButton.onClick.AddListener(() => EnableView(about));
        optionButton.onClick.AddListener(() => EnableView(options));
        tutorialButton.onClick.AddListener(() => EnableView(tutorial));
        rightButton.onClick.AddListener(() => StartGame(isRightHand: true));
        leftButton.onClick.AddListener(() => StartGame(isRightHand: false));
        quitButton.onClick.AddListener(QuitGame);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(() => EnableView(mainMenu));
        }
    }

    private void Start()
    {
        AudioManager.Instance.PlayGlobal(menuMusic, SoundType.Music);
    }

    private void EnablePlayerUIRays()
    {
        SetPlayerPreferences setPlayer = FindObjectOfType<SetPlayerPreferences>();
        setPlayer.SetHandItems(HandHeldType.UIRays);
    }

    /// <summary>
    /// Starts the game based on the selected hand.
    /// </summary>
    public void StartGame(bool isRightHand)
    {
        HideAll();
        AudioManager.Instance.PlayGlobal(gameplayMusic, SoundType.Music);
        Systems.Instance.KatanaRight = isRightHand;
        SceneLoader.Instance.LoadScene(SceneEnum.Gameplay);
    }

    /// <summary>
    /// Sets the game difficulty based on the selected DifficultySO.
    /// </summary>
    public void SetDifficulty(DifficultySO difficultySO)
    {
        Systems.Instance.difficultyLevel = difficultySO;
        EnableView(selectWeapon);
    }

    /// <summary>
    /// Enables the specified view while hiding others.
    /// </summary>
    public void EnableView(GameObject view)
    {
        HideAll();
        view.SetActive(true);
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    private void QuitGame()
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
