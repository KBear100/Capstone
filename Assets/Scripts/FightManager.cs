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
        fightText.text = "Prepare to Fight!";
        timer = wait;
        doneTimer = wait;
        enemyHealthUI.maxValue = enemy.health;
        playerHealthUI.value = MainManager.playerHealth;
    }

    void Update()
    {
        if(player.turn)
        {
            if (player.action == "Attack")
            {
                fightText.text = "Enemy Took " + player.damage + " Damage.";
                enemyHealthUI.value -= player.damage;
                enemy.health -= player.damage;
                if (enemy.health <= 0)
                {
                    fightText.text = "Enemy Defeated!";
                    //player.action = "";
                    doneTimer -= Time.deltaTime;
                    if(doneTimer <= 0)
                    {
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
        }
        else
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                float damage = Random.Range(0, 5) * player.incomingDamage;
                fightText.text = "Player Took " + damage + " Damage!";
                playerHealthUI.value -= damage;
                MainManager.playerHealth -= damage;
                player.incomingDamage = 1;
                if (MainManager.playerHealth <= 0) fightText.text = "You Lose";
                else player.turn = true;
            }
        }
    }
}
