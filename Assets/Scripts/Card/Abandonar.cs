using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abandonar : Card, IUsable
{

    public void ChangeDescription(){
        description = $"Compre {GetBuy()} cartas.";
        SetTextsAndImages();
    }

    public override void Effect()
    {
    }

    public override void AfterDiscardEffect()
    {
        DiscardCard();
    }
}