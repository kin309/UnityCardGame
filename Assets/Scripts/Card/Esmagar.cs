using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esmagar : Card, ICauseDamage
{
    public void ChangeDescription(){
        description = $"Cause {GetDamage()} de dano.";
        SetTextsAndImages();
    }

    public override void Effect()
    {
        CauseDamage();
    }

}
