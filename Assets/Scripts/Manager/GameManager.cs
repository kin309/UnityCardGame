using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string actualTurn;
    public PlayerCharacter playerCharacter;

    private CardManager cardManager;
    private CombatManager combatManager;
    private DeckManager deckManager;
    private RewardManager rewardManager;
    private RelicsManager relicsManager;

    public Card cardDragged;
    public Card cardSelected;

    public int usedCardsCount;
    public int damageTakenThisTurn;
    public int damageCausedThisTurn;

    public int discardCounter;
    public int discardedCards;

    public GameObject displayArea;
    public GameObject interfacePanel;
    public GameObject cardSelectionFrame;
    public GameObject posCombatInterface;
    

    public GameObject discardPanel;
    public GameObject discardDisplay;
    public List<GameObject> discardPanelCards;

    public GameObject handCardsDisplay;
    
    void Start()
    {
        cardManager = GetComponent<CardManager>();
        combatManager = GetComponent<CombatManager>();
        deckManager = GetComponent<DeckManager>();
        rewardManager = GetComponent<RewardManager>();
        relicsManager = GetComponent<RelicsManager>();
        actualTurn = "player";
    }

    void Update()
    {
        if (actualTurn == "player") {
            if(cardDragged != null && cardDragged.collidingWithCreature && Input.GetMouseButtonUp(0) && cardDragged.GetCost() <= playerCharacter.currentEnergy) {
                PlayCard();
            }
        }

        if (Input.GetKeyDown(KeyCode.B)) {
            cardManager.BuyCard();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            //cardManager.PutCardsOnDeck();
        }
        if (Input.GetKeyDown(KeyCode.T)) {
            //cardManager.IncreaseCardsCost();
        }

        rewardManager.SelectCardReward();
        SelectCardToDiscard();
    }

    void ResetCardsStats()
    {
        cardManager.PutHandCardsOnDeck();
        foreach(GameObject card in cardManager.deckPile){
            Card cardscript = card.GetComponent<Card>();
            cardscript.ResetBaseStats();
        }
    }

    void PlayCard()
    {
        if (playerCharacter.IsAlive()){
            if (combatManager.combatEnded == false && cardDragged != null && cardDragged.IsPlayable()) {
                playerCharacter.ConsumeEnergy(cardDragged.GetCost());
                cardDragged.Effect();
                cardManager.DiscardCard(cardDragged.gameObject);
                cardDragged.AfterDiscardEffect();
                combatManager.CheckDeadCreatures();
            }
        }
    }

    public void PassTurn()
    {
        if (actualTurn == "player") {
            actualTurn = "enemy";
            StartCoroutine(EnemyTurn());
        } else {
            actualTurn = "player";
            playerCharacter.RecoverEnergy(1);
            cardManager.BuyCards(2);
        }
        damageCausedThisTurn = 0;
        damageTakenThisTurn = 0;
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(0.8f);
        foreach(Enemy enemy in combatManager.enemiesInCombat) {
            if (!combatManager.combatEnded) {
                enemy.Attack(playerCharacter);
                combatManager.CheckDeadCreatures();
            }
        }
        yield return new WaitForSeconds(0.8f);
        PassTurn();
    }

    public void ResetAll()
    {
        playerCharacter.RecoverAllEnergy();
        //playerCharacter.HealAllLife();
        cardManager.ResetCards();
        cardManager.BuyInitialHand();
        combatManager.combatEnded = false;
        foreach (Enemy enemy in combatManager.enemiesInCombat) {
            Destroy(enemy.gameObject);
        }
        combatManager.enemiesInCombat.Clear();
    }

    public void ShowDiscardScreen(int num)
    {
        if (num > 0) {
            discardPanel.SetActive(true);

            foreach(GameObject card in cardManager.handCards) {
                discardPanelCards.Add(card);
                card.transform.SetParent(discardDisplay.transform);
            }

            cardManager.handCards.Clear();
            
            foreach (GameObject card in discardPanelCards) {
                Card cardScript = card.GetComponent<Card>();
                cardManager.RedefineCard(card);
                cardScript.onlyShow = true;
            }
            discardCounter = num;
        }
        
    }

    public void CloseRewardInterface()
    {
        foreach(GameObject obj in rewardManager.cards){
            Destroy(obj);
        }
        rewardManager.cards.Clear();
        ResetAll();
        interfacePanel.SetActive(false);
        posCombatInterface.SetActive(true);
    }

    public void SelectCardToDiscard()
    {
        foreach(GameObject card in discardPanelCards) {
            Card cardScript = card.GetComponent<Card>();
            if (cardScript.mouseOver && Input.GetMouseButtonDown(0)) {
                card.transform.SetParent(handCardsDisplay.transform);
                cardScript.SetBackgroundColor(cardScript.backgroundOriginalColor);
                cardManager.PutCardOnDiscardPile(card);
                discardPanelCards.Remove(card);
                discardedCards++;
                return;
            }
            if (discardCounter == discardedCards) {
                    foreach(GameObject cardo in discardPanelCards){
                        cardo.transform.SetParent(handCardsDisplay.transform);
                        Card cardoScript = cardo.GetComponent<Card>();
                        cardoScript.SetBackgroundColor(cardoScript.backgroundOriginalColor);
                        cardoScript.onlyShow = false;
                        cardManager.handCards.Add(cardo);
                    }
                    discardPanelCards.Clear();
                    discardPanel.SetActive(false);
                    cardManager.AdjustHandIndex();
                    cardManager.RepositionCards();
                    discardedCards = 0;
                    return;
            }
        }
    }

    public void KillEnemies()
    {
        foreach (Enemy enemy in combatManager.enemiesInCombat) {
            enemy.TakeDamage(enemy.GetMaxHealth());
        }
        combatManager.CheckDeadCreatures();
    }
}
