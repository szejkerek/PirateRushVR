using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDifficultyButton : MonoBehaviour
{
    [SerializeField] DifficultyLevel selectedLevel;
    GameStartMenu gameStartMenu;
    Button button;
    TMP_Text buttonText;

    private void Awake()
    {
        gameStartMenu = FindObjectOfType<GameStartMenu>();
        button = GetComponent<Button>();
        buttonText = button.GetComponentInChildren<TMP_Text>();

        if(string.IsNullOrEmpty(selectedLevel.DifficultyName))
        {
            buttonText.text = "NAME NOT SET";
        }
        else
        {
            buttonText.text = selectedLevel.DifficultyName;
        }

        button.onClick.AddListener(() => gameStartMenu.SetDifficulty(selectedLevel));
    }
}
