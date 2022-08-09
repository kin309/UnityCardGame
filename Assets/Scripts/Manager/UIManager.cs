using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Reward Panel")]
    public Image rewardPanelGoldImage;
    public Image rewardPanelRelicImage;
    public Image rewardPanelCardImage;
    
    [Space(10), Header("PosCombat")]
    public TextMeshProUGUI posCombatGoldNumberTextMesh;
    public Image posCombatGoldImage;

    public Image posCombatMenuImage;
    public Image posCombatDeckImage;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        posCombatGoldNumberTextMesh.text = gameManager.playerCharacter.gold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
