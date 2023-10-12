using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    [SerializeField] FightPlayer player;
    [SerializeField] AttackingEnemy enemy;
    [SerializeField] TMP_Text fightText;
    [SerializeField] float wait;
    [SerializeField] Slider playerHealthUI;
    [SerializeField] Slider enemyHealthUI;

    float timer;
    float doneTimer;

    void Start()
    {
        fightText.text = "A " + enemy.type + " Approaches. Prepare to Fight!";
        timer = wait;
        doneTimer = wait;
        enemyHealthUI.maxValue = enemy.health;
        playerHealthUI.value = MainManager.playerHealth;
    }

    void Update()
    {
        playerHealthUI.value = MainManager.playerHealth;
        enemyHealthUI.value = enemy.health;
        
        if(player.turn)
        {
            if (player.action == "Attack")
            {
                fightText.text = "Enemy Took " + player.damage * enemy.incomingDamage + " Damage.";
                enemy.health -= player.damage;
                if (enemy.health <= 0)
                {
                    fightText.text = "Enemy Defeated! You gain " + enemy.gold + " gold.";

                    doneTimer -= Time.deltaTime;
                    if(doneTimer <= 0)
                    {
                        MainManager.gold += enemy.gold;
                        SceneManager.UnloadSceneAsync("Fight1");
                    }
                }
                else
                {
                    player.action = "";
                    player.turn = false;
                }
            }
            else if (player.action == "Defend")
            {
                fightText.text = "Player Defended";

                player.action = "";
                player.turn = false;
            }
            else if (player.action == "Item")
            {
                fightText.text = "Player Used an Item";

                player.action = "";
                player.turn = false;
            }
            timer = wait;
            enemy.incomingDamage = 1;
        }
        else
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                enemy.Turn();
                if(enemy.turn == 1)
                {
                    float damage = enemy.damage * player.incomingDamage;

                    fightText.text = "Player Took " + damage + " Damage!";
                    MainManager.playerHealth -= damage;
                }
                else if(enemy.turn == 2)
                {
                    fightText.text = "Enemy Defended";
                }
                enemy.turn = 0;
                player.incomingDamage = 1;

                if (MainManager.playerHealth <= 0) fightText.text = "You Lose";
                else player.turn = true;
            }
        }
    }
}
