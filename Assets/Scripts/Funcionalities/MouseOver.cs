using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card cardScript;
    public AudioSource soundefx;

    void Start()
    {
        soundefx = GetComponent<AudioSource>();
        cardScript = gameObject.GetComponent<Card>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) is false && !cardScript.onlyShow){
            transform.localScale = cardScript.GetDisplayScale();
            transform.localPosition = cardScript.GetDisplayPosition();
            soundefx.Play();
            transform.SetSiblingIndex(100);
        } else if (cardScript.onlyShow) {
            cardScript.SetBackgroundColor(Color.white);
        }
        cardScript.mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        if (!cardScript.onlyShow){
            transform.localScale = cardScript.GetInitialScale();
            transform.localPosition = cardScript.GetInitialPosition();
            transform.SetSiblingIndex(cardScript.handIndex);
        } else {
            cardScript.SetBackgroundColor(cardScript.backgroundOriginalColor);
        }
        cardScript.mouseOver = false;
        cardScript.Unselect();
    }
}
