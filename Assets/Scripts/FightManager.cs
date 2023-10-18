using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] FightPlayer player;
    [Header("Party")]
    [SerializeField] PartyFightAI[] partyAI;
    [Header("Enemy")]
    [SerializeField] AttackingEnemy enemy;
    [Header("UI")]
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

        if (MainManager.partyMembers.Contains("Steel")) partyAI[0].gameObject.SetActive(true);
        if (MainManager.partyMembers.Contains("Gracy")) partyAI[1].gameObject.SetActive(true);
        if (MainManager.partyMembers.Contains("Stacy")) partyAI[2].gameObject.SetActive(true);
    }

    void Update()
    {
        playerHealthUI.value = MainManager.playerHealth;
        enemyHealthUI.value = enemy.health;

        if (enemy.health <= 0)
        {
            fightText.text = "Enemy Defeated! You gain " + enemy.gold + " gold.";

            doneTimer -= Time.deltaTime;
            if (doneTimer <= 0)
            {
                MainManager.gold += enemy.gold;
                MainManager.fight = false;
                SceneManager.UnloadSceneAsync("Fight1");
            }
        }

        //Player
        if (player.turn)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                fightText.text = "Meeri's Turn";
                PlayerTurn(player.action);
            }
        }
        //Steel
        else if (partyAI[0].turn)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                fightText.text = "Steel's Turn";
                PartyTurn(partyAI[0], partyAI[0].action);
            }
        }
        //Gracy
        else if (partyAI[1].turn)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                fightText.text = "Gracy's Turn";
                PartyTurn(partyAI[1], partyAI[1].action);
            }
        }
        //Stacy
        else if (partyAI[2].turn)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                fightText.text = "Stacy's turn";
                PartyTurn(partyAI[2], partyAI[2].action);
            }
        }
        //Enemy
        else
        {
            if (MainManager.playerHealth <= 0)
            {
                fightText.text = "You Lose";
                return;
            }

            enemy.incomingDamage = 1;
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

                player.turn = true;
                foreach(PartyFightAI party in partyAI)
                {
                    if (party.gameObject.activeInHierarchy) party.turn = true;
                }
                timer = wait;
            }
        }
    }

    public void PlayerTurn(string action)
    {
        //Attack
        if (action == "Attack")
        {
            fightText.text = "Enemy Took " + player.damage * enemy.incomingDamage + " Damage.";
            enemy.health -= player.damage;

            Clear();
            player.turn = false;
        }
        //Defend
        else if (action == "Defend")
        {
            fightText.text = "Player Defended";

            Clear();
            player.turn = false;
        }
        //Item
        else if (action == "Item")
        {
            fightText.text = "Player Used an Item";

            Clear();
            player.turn = false;
        }
    }

    public void PartyTurn(PartyFightAI member, string action)
    {
        //Attack
        if (action == "Attack")
        {
            fightText.text = "Enemy Took " + member.damage * enemy.incomingDamage + " Damage.";
            enemy.health -= member.damage;

            Clear();
            member.turn = false;
        }
        //Defend
        else if (action == "Defend")
        {
            fightText.text = member.member + " Defended";

            Clear();
            member.turn = false;
        }
        //Item
        else if (action == "Item")
        {
            fightText.text = member.member + " Used an Item";

            Clear();
            member.turn = false;
        }
    }

    public void Clear()
    {
        player.action = "";
        foreach(PartyFightAI party in partyAI)
        {
            party.action = "";
            timer = wait;
        }
    }
}
