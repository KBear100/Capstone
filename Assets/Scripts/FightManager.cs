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
    [Header("Sounds")]
    [SerializeField] AudioSource swordSwing;
    [Header("Party")]
    [SerializeField] PartyFightAI[] partyAI;
    [Header("Enemy")]
    [SerializeField] AttackingEnemy enemy;
    [Header("UI")]
    [SerializeField] TMP_Text fightText;
    [SerializeField] Slider playerHealthUI;
    [SerializeField] Slider steelHealthUI;
    [SerializeField] Slider gracyHealthUI;
    [SerializeField] Slider stacyHealthUI;
    [SerializeField] Slider enemyHealthUI;
    [SerializeField] TMP_Text enemyName;
    [SerializeField] float wait;

    float timer;
    float doneTimer;

    void Start()
    {
        fightText.text = "A " + enemy.type + " Approaches. Prepare to Fight!";
        enemyName.text = enemy.type;

        timer = wait;
        doneTimer = wait;
        enemyHealthUI.maxValue = enemy.health;
        playerHealthUI.value = MainManager.playerHealth;

        if (MainManager.partyMembers.Contains("Steel"))
        {
            partyAI[0].gameObject.SetActive(true);
            steelHealthUI.gameObject.SetActive(true);
        }
        if (MainManager.partyMembers.Contains("Gracy"))
        {
            partyAI[1].gameObject.SetActive(true);
            gracyHealthUI.gameObject.SetActive(true);
        }
        if (MainManager.partyMembers.Contains("Stacy"))
        {
            partyAI[2].gameObject.SetActive(true);
            stacyHealthUI.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        playerHealthUI.value = MainManager.playerHealth;
        steelHealthUI.value = MainManager.steelHealth;
        gracyHealthUI.value = MainManager.gracyHealth;
        stacyHealthUI.value = MainManager.stacyHealth;
        enemyHealthUI.value = enemy.health;

        if (enemy.health <= 0)
        {
            fightText.text = "Enemy Defeated! You gain " + enemy.gold + " gold.";

            doneTimer -= Time.deltaTime;
            if (doneTimer <= 0)
            {
                MainManager.gold += enemy.gold;
                MainManager.freezePlayer = false;
                SceneManager.UnloadSceneAsync("Fight1");
            }
        }

        //Player
        if (player.turn)
        {
            player.incomingDamage = 1;
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
            partyAI[0].incomingDamage = 1;
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
            partyAI[1].incomingDamage = 1;
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
            partyAI[2].incomingDamage = 1;
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
                    string attacked = enemy.RandomAttack();

                    if(attacked == "Player") MainManager.playerHealth -= damage;
                    if(attacked == "Steel") MainManager.steelHealth -= damage;
                    if(attacked == "Gracy") MainManager.gracyHealth -= damage;
                    if(attacked == "Stacy") MainManager.stacyHealth -= damage;

                    fightText.text = attacked + " Took " + damage + " Damage!";
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
            swordSwing.Play();

            Clear();
            player.turn = false;
        }
        //Defend
        else if (action == "Defend")
        {
            fightText.text = "Meeri Defended";

            Clear();
            player.turn = false;
        }
        //Item
        else if (action == "Item")
        {
            fightText.text = "Meeri Used an Item";

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
            swordSwing.Play();

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
