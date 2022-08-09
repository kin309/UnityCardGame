using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Char", menuName = "Character")]
public class CharacterScriptable : ScriptableObject
{
    public Sprite artWork;

    [Space(15)]
    public new string name;
    public int health;
    public int energy;
    public int defense;
    private int muscleCharge;
    private int fatigueLevel;

    public int initialDeckLen;
    public List<CardScriptable> deck = new List<CardScriptable>();

    [Space(15)]
    public CardManager cardManager;

    [Space(15)]
    public string actual_element;

    public void Exaustion()
    {
        if (muscleCharge < 0) {
            muscleCharge = 0;
        }

        if (muscleCharge == 0) {
            fatigueLevel++;
        }
    }

    

    public int GetMuscleCharge()
    {
        return muscleCharge;
    }

    public int GetFatigueLevel()
    {
        return fatigueLevel;
    }
}
