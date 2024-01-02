using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the health system for a game character.
/// </summary>
public class HealthManager : Singleton<HealthManager>
{
    /// <summary>
    /// Event triggered upon the character's death.
    /// </summary>
    public Action OnDeath;

    /// <summary>
    /// Event triggered upon any change in health.
    /// </summary>
    public Action OnHealChange;

    [SerializeField] Transform healthContainer;
    [SerializeField] HealthIcon healthIcon;

    int currentHealth;
    int maxHealth;

    List<HealthIcon> healthIcons = new List<HealthIcon>();

    /// <summary>
    /// Initializes the health system based on game difficulty.
    /// </summary>
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

    /// <summary>
    /// Inflicts damage on the character.
    /// </summary>
    public void TakeHit()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
        }

        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }

        OnHealChange?.Invoke();
    }

    /// <summary>
    /// Heals the character, optionally to maximum health.
    /// </summary>
    /// <param name="toMaximum">Flag indicating whether to heal to maximum health.</param>
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

    /// <summary>
    /// Displays the current health status visually.
    /// </summary>
    private void DisplayHealth()
    {
        foreach (var health in healthIcons)
        {
            health.Kill();
        }

        for (int i = 0; i < currentHealth; i++)
        {
            healthIcons[i].Restore();
        }
    }
}
