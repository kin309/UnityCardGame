using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICauseDamage
{
    public void CauseDamage(Creature target, int damage)
    {
        target.TakeDamage(damage);
    }
}
