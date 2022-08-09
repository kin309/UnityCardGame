using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<GameObject> gameCardsList;
    public List<GameObject> deckList;
    public DeckScriptable deckScriptable;

    void Start()
    {
        AddCardsToGame();
    }

    void AddCardToGame(GameObject cardPrefab)
    {
        Card newCardScript = cardPrefab.GetComponent<Card>();
        newCardScript.PosStart();
        cardPrefab.SetActive(false);
        deckList.Add(cardPrefab);
    }

    void AddCardsToGame()
    {
        for (int i = 0; i < deckScriptable.list.Count; i++) {
            AddCardToGame(deckScriptable.list[i]);
        }
    }

}
