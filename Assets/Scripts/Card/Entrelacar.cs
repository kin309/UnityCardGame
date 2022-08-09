using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrelacar : Card, IUsable
{

    public void ChangeDescription(){
        description = $"Compre {GetBuy()} cartas.";
        SetTextsAndImages();
    }

    public override void Effect()
    {
        BuyCard();
    }
}