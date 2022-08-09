using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{ 
    public List<GameObject> handCards = new List<GameObject>();
    public List<GameObject> discardPile = new List<GameObject>();
    public List<GameObject> deckPile = new List<GameObject>();
    public List<GameObject> gameDeck = new List<GameObject>();

    public float handPosY;
    public int handCardsDistance;
    public int playerInitialHand;
    public int playerMaxCardsInHand;

    public GameObject cardsDisplay;

    public TextMeshProUGUI deckCountText;
    public TextMeshProUGUI disCardPileText;

    private DeckManager deckManager;

    void Start()
    {
        deckManager = gameObject.GetComponent<DeckManager>();
        AddCardsToDeck(deckManager.deckList);
        Shuffle(deckPile);
        BuyCards(playerInitialHand);
    }

    public void AddCardsToDeck(List<GameObject> list)
    {
        foreach(GameObject card in list){
            InstanceCardInDeck(card);
        }
        RedefineCards(deckPile);
    }

    public void AddCardtoDeck(GameObject card)
    {
        RedefineCard(InstanceCardInDeck(card));
    }

    public GameObject InstanceCardInDeck(GameObject card)
    {
        GameObject instancedCard = Instantiate(card, cardsDisplay.transform, false);
        RedefineCard(instancedCard);
        deckPile.Add(instancedCard);
        return instancedCard;
    }

    public void RedefineCards(List<GameObject> list)
    {
        foreach (GameObject card in list) {
            RedefineCard(card);
        }
    }

    public void RedefineCard(GameObject card)
    {
        Card cardScript = card.GetComponent<Card>();
            cardScript.onlyShow = false;
            cardScript.PosStart();
    }

    public void BuyCard()
    {
        if (handCards.Count < playerMaxCardsInHand && deckPile.Count > 0) {
            GameObject card = deckPile[0];
            Card cardScript = card.GetComponent<Card>();
            deckPile.Remove(card);
            handCards.Add(card);
            card.SetActive(true);
            cardScript.handIndex = handCards.Count - 1;
            card.transform.SetSiblingIndex(cardScript.handIndex);
            RepositionCards();
            deckCountText.text = deckPile.Count.ToString();
        } else if (handCards.Count < playerMaxCardsInHand && discardPile.Count != 0){
            PutDiscardPileOnDeck();
            Shuffle(deckPile);
            BuyCard();
        }
    }

    public void BuyCards(int num)
    {
        for (int i = 0; i < num; i++) {
            BuyCard();
        }
    }

    public void BuyInitialHand()
    {
        for (int i = 0; i < playerInitialHand; i++) {
            BuyCard();
        }
    }

    public void DiscardCard(GameObject card)
    {
        Card cardScript = card.GetComponent<Card>();
        handCards.Remove(card);
        discardPile.Add(card);
        card.gameObject.SetActive(false);
        
        foreach (GameObject cards in handCards){
            Card cardsScript = cards.GetComponent<Card>();
            if (cardsScript.handIndex > cardScript.handIndex){
                cardsScript.handIndex--;
                cards.transform.SetSiblingIndex(cardScript.handIndex);
            }
        }
        RepositionCards();
        disCardPileText.text = discardPile.Count.ToString();
    }

    void Shuffle<T>(List<T> inputList)
    {
        for (int i = 0; i < inputList.Count; i++) {

            T temp = inputList[i];
            int rand = Random.Range(i, inputList.Count);
            inputList[i] = inputList[rand];
            inputList[rand] = temp;

        }
    }

    public void RepositionCards()
    {
        foreach (GameObject card in handCards) {
            Card cardScript = card.GetComponent<Card>();
            Vector3 initpos = new Vector3(cardScript.handIndex*handCardsDistance - (handCards.Count-1)*65, handPosY, 0);
            Vector3 displaypos = new Vector3(cardScript.handIndex*handCardsDistance - (handCards.Count-1)*65, handPosY + 45, 0);
            cardScript.SetInitialPosition(initpos);
            cardScript.SetDisplayPosition(displaypos);
            cardScript.Reposition();
        }
    }

    public void PutDiscardPileOnDeck()
    {
        foreach (GameObject card in discardPile) {
            deckPile.Add(card);
            //card.gameObject.SetActive(false);
        }
        discardPile.Clear();
        ResetText();
    }

    public void PutHandCardsOnDeck()
    {
        foreach (GameObject card in handCards) {
            deckPile.Add(card);
            card.gameObject.SetActive(false);
        }
        handCards.Clear();
        ResetText();
    }

    public void PutCardOnDiscardPile(GameObject card)
    {
        card.SetActive(false);
        discardPile.Add(card);
        Card cardScript = card.GetComponent<Card>();
        RedefineCard(card);
        AdjustHandIndex();
        disCardPileText.text = discardPile.Count.ToString();
    }

    public void AdjustHandIndex()
    {
        int i = 0;
        foreach (GameObject card in handCards)
        {
            Card cardScript = card.GetComponent<Card>();
            cardScript.handIndex = i;
            card.transform.SetSiblingIndex(i);
            i++;
        }
    }

    public void ResetCards()
    {
        PutDiscardPileOnDeck();
        PutHandCardsOnDeck();
        Shuffle(deckPile);
    }

    void ResetText()
    {
        disCardPileText.text = discardPile.Count.ToString();
        deckCountText.text = deckPile.Count.ToString();
    }

}
