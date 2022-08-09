using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espalhar : Card, IUsable
{
    public void ChangeDescription(){
        description = $"Cause {GetDamage()} de dano.";
        SetTextsAndImages();
    }

    public override void Effect()
    {
        foreach(Enemy enemy in combatManager.enemiesInCombat){
            enemy.TakeDamage(GetDamage());
        }
    }
}
