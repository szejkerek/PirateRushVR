using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject selectWeapon;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject about;

    [Header("Main Menu Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button aboutButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private List<Button> returnButtons;

    void Awake()
    {
        EnablePlayerUIRays();
        EnableView(mainMenu);

        rightButton.onClick.AddListener(() => StartGame(rightHand: true));
        leftButton.onClick.AddListener(() => StartGame(rightHand: false));
        startButton.onClick.AddListener(() => EnableView(selectWeapon));
        optionButton.onClick.AddListener(() => EnableView(options));
        aboutButton.onClick.AddListener(() => EnableView(about));
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
        options.SetActive(false);
        about.SetActive(false);
        selectWeapon.SetActive(false);
    }
}
