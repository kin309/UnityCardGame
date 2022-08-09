using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{

#region variables
    [SerializeField] private int maxHealth;
    private int health;
    private int baseDamage;
    private int damage;
    private int baseHeal;
    private int heal;
    public int castTime;
    protected bool alive;

    public ProgressBar healthBar;
#endregion

    void Start()
    {
        alive = true;
        health = maxHealth;
        healthBar.SetMaxFill(maxHealth);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0) {
            health = 0;
            alive = false;
        }
        healthBar.SetFill(health);
    }

    public void Heal(int heal)
    {
        health += heal;
        if (health > maxHealth) {
            health = maxHealth;
        }
        healthBar.SetFill(health);
    }

    public void HealAllLife()
    {
        health = maxHealth;
        healthBar.SetFill(health);
        alive = true;
    }

    public bool IsAlive()
    {
        return alive;
    }

    // Health
    public int GetMaxHealth() { return maxHealth; }

    public void SetMaxHealth(int value) { maxHealth = value; }

    public int GetHealth() { return health; }

    public void SetHealth(int value) { health = value; }

    // Damage
    public int GetDamage() { return baseDamage; }

    public void SetDamage(int value) { baseDamage = value;}

    public int GetCurrentDamage() { return damage; }

    public void SetCurrentDamage(int value) { damage = value; }

    // Healing
    public int GetHeal() { return baseHeal; }

    public void SetHeal(int value) { baseHeal = value; }

    public int GetCurrentHeal() { return heal; }

    public void SetCurrentHeal(int value) { heal = value; }

    public void IncreaseDamage(int value)
    {
        baseDamage += value;
        damage += value;
    }

    public void IncreaseHeal(int value)
    {
        baseHeal += value;
        heal += value;
    }

}
