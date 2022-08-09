using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public abstract class Card : MonoBehaviour, ICauseDamage
{

    #region variables

    [SerializeField] private CardScriptable cardScriptable;

    protected GameManager gameManager;
    protected CardManager cardManager;
    protected CombatManager combatManager;

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI descriptionText;
    private TextMeshProUGUI costText;

    private Image artworkImage;
    private Image background;

    public int handIndex;
    public bool hasBeenPlayed;

    private Vector3 initialPosition;
    private Vector3 initialScale;

    private Vector3 displayPosition;
    private Vector3 displayScale;

    private new string name;
    protected string description;

    private int damage;
    private int heal;
    private int buy;
    private int discard;
    private int cost;
    private int energyRecovery;
    private string element;
    public int attackTimes = 1;

    private int baseDamage;
    private int baseHeal;
    private int baseBuy;
    private int baseCost;

    private bool canDamage;
    private bool canHeal;
    private bool canBuy;
    private bool canDiscardCard;
    private bool canChangeForm;
    private bool canRecoverEnergy;
    public bool targetable;
    public bool selected;
    public bool playable;

    public bool onlyShow;
    public bool mouseOver;

    public Color backgroundOriginalColor;

    public bool collidingWithCreature;
    public Creature creatureColliding;

    public string selectedElement;
    List<TextMeshProUGUI> textElements = new List<TextMeshProUGUI>();
    List<Image> imageElements = new List<Image>();

    #endregion

    public bool IsPlayable()
    {
        if (targetable == true && creatureColliding.IsAlive() || targetable == false)
            return true;
        else 
            return false;
    }

    public void CauseDamage()
    {
        int totalDamage = damage + combatManager.playerCharacter.GetDamage();
        creatureColliding.TakeDamage(totalDamage);
    }

    public void BuyCard()
    {
        cardManager.BuyCards(buy);
    }

    public void DiscardCard()
    {
        gameManager.ShowDiscardScreen(discard);
    }

    public virtual void PosStart()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cardManager = gameManager.gameObject.GetComponent<CardManager>();
        combatManager = gameManager.gameObject.GetComponent<CombatManager>();
        GetTextMeshComponents();
        GetImageComponents();
        Define();
    }

    public void Select()
    {
        selected = true;
        SetBackgroundColor(Color.cyan);
    }

    public void Unselect()
    {
        selected = false;
        SetBackgroundColor(backgroundOriginalColor);
    }
    
    public void Define()
    {
        description = cardScriptable.description;

        SetTextsAndImages();

        // Atributtes
        name = cardScriptable.name;
        damage = cardScriptable.damage;
        heal = cardScriptable.heal;
        buy = cardScriptable.buy;
        discard = cardScriptable.discard;
        energyRecovery = cardScriptable.energyRecovery;
        cost = cardScriptable.cost;
        attackTimes = cardScriptable.attackTimes;
        targetable = cardScriptable.targetable;

        baseDamage = damage;
        baseHeal = heal;
        baseBuy = buy;
        baseCost = cost;

        canDamage = cardScriptable.canDamage;
        canHeal = cardScriptable.canHeal;
        canBuy = cardScriptable.canBuy;
        canDiscardCard = cardScriptable.canDiscardCard;
        canChangeForm = cardScriptable.canChangeForm;
        canRecoverEnergy = cardScriptable.canRecoverEnergy;
        
        // Configuration
        initialScale = transform.localScale;
        displayScale = transform.localScale*1.5f;
        backgroundOriginalColor = background.color;

        transform.localPosition = initialPosition;
    }

    public void ResetBaseStats()
    {
        damage = baseDamage;
        heal = baseHeal;
        buy = baseBuy;
        cost = baseCost;
    }

    protected void GetTextMeshComponents()
    {
        foreach (TextMeshProUGUI component in GetComponentsInChildren<TextMeshProUGUI>()){
            textElements.Add(component);
        }
        costText = textElements[0];
        nameText = textElements[1];
        descriptionText = textElements[2];
    }

    protected void GetImageComponents()
    {
        foreach (Image component in GetComponentsInChildren<Image>()){
            imageElements.Add(component);
        }
        background = imageElements[0];
        artworkImage = imageElements[3];
    }

    public void SetTextsAndImages(){
        // Text Mesh
        nameText.text = cardScriptable.name;
        costText.text = cardScriptable.cost.ToString();
        descriptionText.text = description;

        // Images
        artworkImage.sprite = cardScriptable.artworkImage;
    }

    public void Reposition()
    {
        transform.localPosition = initialPosition;
        gameObject.SetActive(true);
    }

    public void IncreaseDamage(int value) { damage += value; }

    public void DecreaseDamage(int value)
    {
        damage -= value;
        if (damage < 0) {
            damage = 0;
        }
    }

    public void IncreaseBaseDamage(int value)
    {
        baseDamage = value;
        IncreaseDamage(value);
    }

    public void DecreaseBaseDamage(int value)
    {
        baseDamage -= value;
        if (baseDamage < 0) {
            baseDamage = 0;
        }
        DecreaseDamage(value);
    }

    public void IncreaseHeal(int value) { heal += value; }

    public void DecreaseHeal(int value)
    {
        heal -= value;
        if (heal < 0) {
            heal = 0;
        }
    }

    public void IncreaseBaseHeal(int value)
    {
        baseHeal += value;
        IncreaseHeal(value);
    }

    public void DecreaseBaseHeal(int value)
    {
        baseHeal -= value;
        DecreaseHeal(value);
    }

    public void IncreaseBuy(int value) { buy += value; }

    public void DecreaseBuy(int value)
    {
        buy -= value;
        if (buy < 0) {
            buy = 0;
        }
    }

    public void IncreaseBaseBuy(int value)
    {
        baseBuy += value;
        buy += value;
    }

    public void DecreaseBaseBuy(int value)
    {
        baseBuy -= value;
        DecreaseBuy(value);
    }

    // Name
    public string GetName() { return name; }

    public void SetName(string nameString)
    {
        name = nameString;
        nameText.text = name;
    }

    // Damage
    public int GetDamage() { return damage; }

    public void SetDamage(int value)
    {
        damage = value;
    }

    // Base Damage
    public int GetBaseDamage() { return baseDamage; }

    public void SetBaseDamage(int value)
    {
        baseDamage = value;
        SetDamage(value);
    }

    // Heal
    public int GetHeal() { return heal; }

    public void SetHeal(int value)
    {
        heal = value;
    }

    // Base Heal
    public int GetBaseHeal() { return baseHeal; }

    public void SetBaseHeal(int value)
    {
        baseHeal = value;
        SetHeal(value);
    }

    // Buy
    public int GetBuy() { return buy; }

    public void SetBuy(int value)
    {
        buy = value;
    }

    // Base Buy
    public int GetBaseBuy() { return baseBuy; }

    public void SetBaseBuy(int value)
    {
        baseBuy = value;
        buy = value;
    }

    // Cost
    public int GetCost() { return cost; }

    public void SetCost(int value)
    {
        cost = value;
        costText.text = cost.ToString();
    }

    public void IncreaseCost(int value)
    {
        cost += value;
        costText.text = cost.ToString();
    }

    public void DecreaseCost(int value)
    {
        cost -= value;
        if (cost < 0) {
            cost = 0;
        }
        costText.text = cost.ToString();
    }

    // Cost
    public int GetBaseCost() { return baseCost; }

    public void SetBaseCost(int value)
    {
        baseCost = value;
        SetCost(value);
    }

    public void IncreaseBaseCost(int value)
    {
        baseCost += value;
        IncreaseCost(value);
    }

    public void DecreaseBaseCost(int value)
    {
        baseCost -= value;
        if (baseCost < 0) {
            baseCost = 0;
        }
        costText.text = baseCost.ToString();
        DecreaseCost(value);
    }

    // EnergyRecovery
    public int GetEnergyRecovery() { return energyRecovery; }

    public void SetEnergyRecovery(int value)
    {
        energyRecovery = value;
    }

    public void IncreaseEnergyRecovery(int value)
    {
        energyRecovery += value;
    }

    public void DecreaseEnergyRecovery(int value)
    {
        energyRecovery -= value;
        if (energyRecovery < 0) {
            energyRecovery = 0;
        }
    }

    // Element
    public string GetElement() { return element; }

    public void SetElement(string elementName)
    {
        element = elementName;
    }

    // Initial Position
    public Vector3 GetInitialPosition() { return initialPosition; }

    public void SetInitialPosition(Vector3 initPos)
    {
        initialPosition = initPos;
    }

    // Initial Scale
    public Vector3 GetInitialScale() { return initialScale; }

    public void SetInitialScale(Vector3 initScale)
    {
        initialScale = initScale;
    }

    // Display Position
    public Vector3 GetDisplayPosition() { return displayPosition; }

    public void SetDisplayPosition(Vector3 displayPos)
    {
        displayPosition = displayPos;
    }

    // Display Scale
    public Vector3 GetDisplayScale() { return displayScale; }

    public void SetDisplayScale(Vector3 displayScal)
    {
        displayScale = displayScal;
    }

    // Background Color
    public Color GetBackgroundColor() { return background.color; }

    public void SetBackgroundColor(Color bgcolor)
    {
        background.color = bgcolor;
    }

    // Can Damage
    public bool CanDamage() { return canDamage; }

    public bool CanHeal() { return canHeal; }

    public bool CanBuy() { return canBuy; }

    public bool CanDiscard() { return canDamage; }

    public bool CanChangeForm() { return canDamage; }

    public bool CanRecoverEnergy() { return canRecoverEnergy; }

    public ScriptableObject GetScriptableCard() { return cardScriptable; }

    public virtual void Effect(){
    }

    public virtual void AfterDiscardEffect() {
    }


    public void SetScriptableCard(CardScriptable scriptable) { cardScriptable = scriptable; }
}