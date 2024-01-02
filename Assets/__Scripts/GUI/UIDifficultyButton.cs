using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a UI button that handles difficulty selection.
/// </summary>
public class UIDifficultyButton : MonoBehaviour
{
    [SerializeField] DifficultySO selectedLevel;
    GameStartMenu gameStartMenu;
    Button button; 
    TMP_Text buttonText; 

    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        gameStartMenu = FindObjectOfType<GameStartMenu>();
        button = GetComponent<Button>(); 
        buttonText = button.GetComponentInChildren<TMP_Text>(); 

        // Setting the button text based on the selected difficulty level
        if (string.IsNullOrEmpty(selectedLevel.DifficultyName))
        {
            buttonText.text = "NAME NOT SET";
        }
        else
        {
            buttonText.text = selectedLevel.DifficultyName;
        }

        // Adding a listener to the button click event to set the difficulty level
        button.onClick.AddListener(() => gameStartMenu.SetDifficulty(selectedLevel));
    }
}
