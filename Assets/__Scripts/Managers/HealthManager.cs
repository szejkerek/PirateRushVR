using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : Singleton<HealthManager>   
{
    public Action OnDeath;
    public Action OnHealChange;

    [SerializeField] Transform healthContainer;
    [SerializeField] HealthIcon healthIcon;

    int currentHealth;
    int maxHealth;

    List<HealthIcon> healthIcons = new List<HealthIcon>();

    private void Start()
    {
        maxHealth = Systems.Instance.difficultyLevel.MaxHealth;

        for (int i = 0; i < maxHealth; i++)
        {
            healthIcons.Add(Instantiate(healthIcon, healthContainer));
        }

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
        foreach (var health in healthIcons)
        {
            health.Kill();
        }

        for (int i = 0;i < currentHealth; i++) { //od drugiej stroy
            healthIcons[i].Restore();
        }
     }
}
