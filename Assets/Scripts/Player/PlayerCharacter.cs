using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCharacter : Creature
{
    #region variables

    public CharacterScriptable characterScript;
    public new string name;

    public int energy;
    public int currentEnergy;
    public int InitialHand;
    public int MaxCardsInHand;
    public int gold;

    public Sprite artWork;

    public TextMeshProUGUI energyText;

    [Space(15)]
    public string last_element;
    public string actual_element;

    public List<Relic> relics = new List<Relic>();
    
    #endregion
    
    void Start()
    {
        SetHealth(GetMaxHealth());
        healthBar.SetMaxFill(GetHealth());
        alive = true;
        
        name = characterScript.name;
        energy = characterScript.energy;
        artWork = characterScript.artWork;

        currentEnergy = energy;

        ResetText();
    }

    public void ConsumeEnergy(int value)
    {
        if (value <= currentEnergy){
            currentEnergy -= value;
            ResetText();
        }
    }

    public void RecoverEnergy(int value)
    {
        currentEnergy += value;
        if (currentEnergy > energy) {
            currentEnergy = energy;
        }
        ResetText();
    }

    public void RecoverAllEnergy()
    {
        currentEnergy = energy;
        ResetText();
    }

    public void ResetText()
    {
        energyText.text = $"{currentEnergy}/{energy}";
    }

}