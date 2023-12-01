using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : Singleton<HealthManager>   
{
    public Action OnDeath;
    public Action OnHealChange;

    [SerializeField] TMP_Text healthDisplay;
    int currentHealth;
    int maxHealth;

    private void Start()
    {
        maxHealth = Systems.Instance.difficultyLevel.MaxHealth;
        currentHealth = maxHealth;
        OnHealChange += DisplayHealth;
        DisplayHealth();
    }

    public void TakeHit()
    {
        if(currentHealth > 0)
        {
            currentHealth--;
        }

        if(currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }

        OnHealChange?.Invoke();
    }

    public void Heal(bool toMaximum = false)
    {
        if (toMaximum)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth++;
            currentHealth = Math.Clamp(currentHealth, 0, maxHealth);
        }
        OnHealChange?.Invoke();
    }

    private void DisplayHealth()
    {
        healthDisplay.text = $"Health: {currentHealth}";
    }
}
