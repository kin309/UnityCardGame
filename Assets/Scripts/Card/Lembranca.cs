using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lembranca : Card, IUsable
{
    public void ChangeDescription(){
        description = $"Cure {GetHeal()} de vida.";
        SetTextsAndImages();
    }

    public override void Effect()
    {
        creatureColliding.Heal(GetHeal());
    }
}
