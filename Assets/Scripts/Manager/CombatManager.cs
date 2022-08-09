using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public int deadEnemiesCount;
    public bool combatEnded;
    public PlayerCharacter playerCharacter;
    public List<Enemy> enemiesList = new List<Enemy>();
    public List<Enemy> enemiesInCombat = new List<Enemy>();

    private GameManager gameManager;
    private RewardManager rewardManager;

    // Start is called before the first frame update
    void Start()
    {
        rewardManager = GetComponent<RewardManager>();
        gameManager = GetComponent<GameManager>();
    }

    public void DisplayEnemies()
    {
        foreach (Enemy creat in enemiesList)
        {
            Enemy creature = Instantiate(creat, GameObject.Find("Enemies Display Area").transform, false);
            creature.SetDamage(1);
            enemiesInCombat.Add(creature);
        }
    }

    public void CheckDeadCreatures()
    {
        deadEnemiesCount = 0;
        foreach (Creature enemy in enemiesInCombat) {
            if (!enemy.IsAlive()) {
                deadEnemiesCount++;
            }
        }
        if (!playerCharacter.IsAlive()) {
            EndCombat("lose");
            return;
        } else if (deadEnemiesCount == enemiesInCombat.Count) {
            EndCombat("win");
        }
    }

    void EndCombat(string state)
    {
        if (state == "win"){
            gameManager.interfacePanel.SetActive(true);
            rewardManager.GetCombatGoldReward();
        } else {
            Debug.Log("Perdeu");
        }
        combatEnded = true;
        rewardManager.combatInterface.SetActive(false);
        rewardManager.SetButtonsActive();
    }

    public void LoadCombat()
    {
        DisplayEnemies();
    }
}
