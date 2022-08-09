using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private Card card;

    private void Start() 
    {
        card = GetComponent<Card>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if is colliding with enemy
        if (collision.CompareTag("Enemy") && card.targetable) {
            card.creatureColliding = collision.GetComponentInParent<Creature>();
            card.collidingWithCreature = true;
            card.SetBackgroundColor(Color.white);
        // Checks if is colliding with the player
        } else if (collision.CompareTag("Player") && card.targetable) {
            card.creatureColliding = collision.GetComponentInParent<Creature>();
            card.collidingWithCreature = true;
            card.SetBackgroundColor(Color.white);
        // Checks if is colliding with neutral play area
        } else if (collision.CompareTag("Neutral") && !card.targetable) {
            card.collidingWithCreature = true;
            card.SetBackgroundColor(Color.white);
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if ((collision.CompareTag("Enemy") || collision.CompareTag("Player")) && card.targetable){
            card.collidingWithCreature = false;
            card.SetBackgroundColor(card.backgroundOriginalColor);
        } else if (collision.CompareTag("Neutral") && !card.targetable) {
            card.collidingWithCreature = false;
            card.SetBackgroundColor(card.backgroundOriginalColor);
        }
        card.creatureColliding = null;
    }
}
