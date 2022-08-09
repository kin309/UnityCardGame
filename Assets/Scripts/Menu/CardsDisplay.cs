using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsDisplay : MonoBehaviour
{
    public List<GameObject> allGameCards;
    public List<GameObject> displayCards;

    void Start()
    {
        foreach (GameObject card in allGameCards) {
            GameObject displayCard = Instantiate(card, this.gameObject.transform, false);
            displayCards.Add(displayCard);
            card.GetComponent<DragDrop>().enabled = false;
            Card cardScript = displayCard.GetComponent<Card>();
            cardScript.SetInitialPosition(displayCard.transform.position);
            cardScript.onlyShow = true;
        }
        for (int i = 0; i < 3; i++){
            ShowCards();
            ShowCards();
        }
        
    }

    void ShowCards(){
        foreach (GameObject card in allGameCards) {
            GameObject displayCard = Instantiate(card, this.gameObject.transform, false);
            displayCards.Add(displayCard);
            card.GetComponent<DragDrop>().enabled = false;
            Card cardScript = displayCard.GetComponent<Card>();
            cardScript.SetInitialPosition(displayCard.transform.position);
            cardScript.onlyShow = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
