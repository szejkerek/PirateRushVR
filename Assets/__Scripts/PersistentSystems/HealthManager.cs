using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : Singleton<HealthManager>   
{
    public Action OnDeath;
    int currentHealth;
    int maxHealth;

    private void Start()
    {
        maxHealth = Systems.Instance.difficultyLevel.MaxHealth;
        currentHealth = maxHealth;
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
    }

}
