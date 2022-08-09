using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicsManager : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerCharacter playerReference;
    public List<GameObject> gameRelics;
    public GameObject relicsDisplayArea;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        playerReference = gameManager.playerCharacter;
    }

    public void AddRelic(Relic relic)
    {
        playerReference.relics.Add(relic);
        playerReference.IncreaseDamage(relic.damageBoost);
        playerReference.IncreaseHeal(relic.healBoost);
    }
}
