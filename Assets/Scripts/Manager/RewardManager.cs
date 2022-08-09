using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardManager : MonoBehaviour
{
    private DeckManager deckManager;
    private CardManager cardManager;
    private GameManager gameManager;
    private CombatManager combatManager;
    private RelicsManager relicsManager;
    private UIManager uIManager;

    public GameObject rewardFrame;
    public GameObject combatInterface;

    public GameObject relicButton;
    public GameObject cardButton;
    public GameObject goldButton;

    public GameObject relicReward;

    public TextMeshProUGUI goldText;

    public List<GameObject> cards = new List<GameObject>();
    public List<GameObject> gameCards;

    public int numOfRewards;
    public int goldReward;  

    void Start()
    {
        numOfRewards = 3;
        deckManager = GetComponent<DeckManager>();
        cardManager = GetComponent<CardManager>();
        gameManager = GetComponent<GameManager>();
        combatManager = GetComponent<CombatManager>();
        relicsManager = GetComponent<RelicsManager>();
        uIManager = GetComponent<UIManager>();
    }

    public void CreateCardReward()
    {
        gameCards = new List<GameObject>(deckManager.gameCardsList);
        for (int i = 0; i < numOfRewards; i++){
            int randomCardIndex = Random.Range(0, gameCards.Count);
            GameObject card = Instantiate(gameCards[randomCardIndex], gameManager.displayArea.transform, false);
            Card cardScript = card.GetComponent<Card>();
            cardScript.onlyShow = true;
            cardScript.PosStart();
            card.SetActive(true);
            cards.Add(card);
            gameCards.Remove(gameCards[randomCardIndex]);
        }
    }

    public void CreateRelicReward()
    {
        int randomRelicIndex = Random.Range(0, relicsManager.gameRelics.Count);
        GameObject relic = Instantiate(relicReward, relicsManager.relicsDisplayArea.transform, false);
        relicReward = relic;
    }

    public void SelectCardReward()
    {
        foreach (GameObject card in new List<GameObject>(cards)) {
            Card cardScript = card.GetComponent<Card>();
            if(cardScript.mouseOver && Input.GetMouseButtonDown(0)){
                cardScript.SetBackgroundColor(cardScript.backgroundOriginalColor);
                cardManager.InstanceCardInDeck(card);
                foreach(GameObject obj in cards){
                    Destroy(obj);
                }
                cards.Clear();
                gameManager.cardSelectionFrame.SetActive(false);
                cardButton.SetActive(false);
            }
        }
    }

    public void GetCombatGoldReward()
    {
        goldReward = 0;
        foreach (Enemy enemy in combatManager.enemiesInCombat)
        {
            goldReward += enemy.goldReward;
            goldText.text = $"{goldReward} gold";
        }
    }

    public void AddGoldToPlayer()
    {
        gameManager.playerCharacter.gold += goldReward;
        uIManager.posCombatGoldNumberTextMesh.text = gameManager.playerCharacter.gold.ToString();
    }

    public void SetButtonsActive()
    {
        cardButton.SetActive(true);
        relicButton.SetActive(true);
        goldButton.SetActive(true);
    }

}
