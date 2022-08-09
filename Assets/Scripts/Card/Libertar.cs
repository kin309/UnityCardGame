using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libertar : Card, IUsable
{
    public void ChangeDescription(){
        description = $"Recupere {GetEnergyRecovery()} de energia.";
        SetTextsAndImages();
    }

    public override void Effect()
    {
        gameManager.playerCharacter.RecoverEnergy(GetEnergyRecovery());
    }
}
