using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Creature
{
    public EnemyScriptable enemyScriptable;
    public Image image;
    
    public int goldReward;

    void Start()
    {
        alive = true;
        SetHealth(GetMaxHealth());
        healthBar.SetMaxFill(GetMaxHealth());
        SetMaxHealth(enemyScriptable.health);
        image.sprite = enemyScriptable.artWork;
    }
    
    public void Attack(Creature target)
    {
        if (IsAlive())
        target.TakeDamage(GetDamage());
    }

}
