using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolpeSucessivo : Card, IUsable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Effect()
    {
        CauseDamage();
        cardManager.BuyCards(GetBuy());
    }
}
