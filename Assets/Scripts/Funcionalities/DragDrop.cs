using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Card card;

    [SerializeField]
    private float dampingSpeed = .0182f;

    private RectTransform dragginObjectRectTransform;
    private Vector3 velocity = Vector3.zero;

    private CanvasGroup canvasGroup;

    private GameManager gameManager;


    private void Start() 
    {
        card = gameObject.GetComponent<Card>();
        dragginObjectRectTransform = transform as RectTransform;
        canvasGroup = GetComponent<CanvasGroup>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!card.onlyShow) {
                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(dragginObjectRectTransform, eventData.position,
                eventData.pressEventCamera, out var globalMousePosition)){
                dragginObjectRectTransform.position = Vector3.SmoothDamp(dragginObjectRectTransform.position, globalMousePosition, ref velocity, dampingSpeed);
                transform.SetSiblingIndex(100);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!card.onlyShow) {
            canvasGroup.blocksRaycasts = false;
            gameManager.cardDragged = card;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!card.onlyShow){
            canvasGroup.blocksRaycasts = true;
            transform.localPosition = GetComponent<Card>().GetInitialPosition();
            transform.SetSiblingIndex(card.handIndex);
        }
    }
    
}
