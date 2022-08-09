using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardScriptable : ScriptableObject
{
    public Sprite artworkImage;

    public int handIndex;
    public bool hasBeenPlayed;

    public new string name;
    public int damage;
    public int heal;
    public int buy;
    public int discard;
    public int cost;
    public int energyRecovery;
    public string description;
    public int attackTimes;

    public int baseDamage;
    public int baseHeal;
    public int baseBuy;
    public int baseCost;

    public bool canDamage;
    public bool canHeal;
    public bool canBuy;
    public bool canDiscardCard;
    public bool canChangeForm;
    public bool canRecoverEnergy;
    public bool targetable;

    public bool collidingWithCreature;
    public Creature creatureColliding;

    public string selectedElement;
}
