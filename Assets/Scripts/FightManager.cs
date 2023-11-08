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
    [SerializeField] AttackingEnemy[] enemy;
    [Header("UI")]
    [SerializeField] TMP_Text fightText;
    [SerializeField] Slider playerHealthUI;
    [SerializeField] Slider steelHealthUI;
    [SerializeField] Slider gracyHealthUI;
    [SerializeField] Slider stacyHealthUI;
    [SerializeField] Slider[] enemyHealthUI;
    [SerializeField] TMP_Text[] enemyName;
    [SerializeField] GameObject[] targetButtons;
    [SerializeField] float wait;

    float timer;
    float doneTimer;
    int numEnemies = 1;
    int targetedEnemy;

    void Start()
    {
        if (MainManager.partySize > 0) numEnemies = 2;
        if (MainManager.partySize > 2) numEnemies = 3;
        timer = wait;
        doneTimer = wait;

        if (numEnemies >= 2) enemy[1].gameObject.SetActive(true);
        if (numEnemies == 3) enemy[2].gameObject.SetActive(true);

        if (numEnemies == 1) fightText.text = "A " + enemy[0].type + " Approaches. Prepare to Fight!";
        else fightText.text = "A group of enemies approach. Prepare to Fight!";

        enemyName[0].text = enemy[0].type;
        enemyHealthUI[0].maxValue = enemy[0].health;

        if (numEnemies >= 2)
        {
            enemyName[1].text = enemy[1].type;
            enemyHealthUI[1].gameObject.SetActive(true);
            enemyName[1].gameObject.SetActive(true);
            enemyHealthUI[1].maxValue = enemy[1].health;
        }
        if (numEnemies == 3)
        {
            enemyName[2].text = enemy[2].type;
            enemyHealthUI[2].gameObject.SetActive(true);
            enemyName[2].gameObject.SetActive(true);
            enemyHealthUI[2].maxValue = enemy[2].health;
        }

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
        enemyHealthUI[0].value = enemy[0].health;
        enemyHealthUI[1].value = enemy[1].health;
        enemyHealthUI[2].value = enemy[2].health;

        foreach(AttackingEnemy foe in enemy)
        {
            if (foe.gameObject.activeInHierarchy && foe.health <= 0)
            {
                foe.gameObject.SetActive(false);
                foe.turn = false;
                fightText.text = foe.type + " defeated";
            }
        }

        if (!enemy[0].gameObject.activeInHierarchy && !enemy[1].gameObject.activeInHierarchy && !enemy[2].gameObject.activeInHierarchy)
        {
            fightText.text = "Enemy Defeated! You gain " + enemy[0].gold  + " gold.";

            doneTimer -= Time.deltaTime;
            if (doneTimer <= 0)
            {
                MainManager.gold += enemy[0].gold;
                MainManager.pause = false;
                MainManager.forestMusic.Play();
                SceneManager.UnloadSceneAsync("Fight1");
            }
        }

        if (MainManager.playerHealth <= 0)
        {
            fightText.text = "You Lose";
            doneTimer -= Time.deltaTime;
            if (doneTimer <= 0)
            {
                MainManager.destroyManager = true;
                SceneManager.LoadSceneAsync("Title", LoadSceneMode.Single);
            }
            return;
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
                foreach (AttackingEnemy foe in enemy)
                {
                    if (foe.gameObject.activeInHierarchy)
                    {
                        foe.turn = true;
                    }
                }
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
        //Enemies
        else if (enemy[0].turn)
        {
            EnemyTurn(enemy[0]);
        }
        else if (enemy[1].turn)
        {
            EnemyTurn(enemy[1]);
        }
        else if (enemy[2].turn)
        {
            EnemyTurn(enemy[2]);
        }
        //Repeat
        else
        {
            player.turn = true;
            foreach (PartyFightAI party in partyAI)
            {
                if (party.gameObject.activeInHierarchy) party.turn = true;
            }
        }
    }

    public void PlayerTurn(string action)
    {
        //Attack
        if (action == "Attack")
        {
            if(numEnemies >= 2)
            {
                fightText.text = "Choose an enemy to attack.";
                targetButtons[0].SetActive(true);
                targetButtons[1].SetActive(true);
                if(numEnemies == 3) targetButtons[2].SetActive(true);
                return;
                //To attack function
            }

            fightText.text = enemy[0].type + " Took " + player.damage * enemy[0].incomingDamage + " Damage.";
            enemy[0].health -= player.damage;
            player.animator.SetTrigger("Attack");
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
            if (numEnemies >= 2)
            {
                fightText.text = "Choose an enemy to attack.";
                targetButtons[0].SetActive(true);
                targetButtons[1].SetActive(true);
                if (numEnemies == 3) targetButtons[2].SetActive(true);
                return;
                //To attack function
            }

            fightText.text = enemy[0].type + " Took " + member.damage * enemy[0].incomingDamage + " Damage.";
            enemy[0].health -= member.damage;
            swordSwing.Play();
            member.animator.SetTrigger("Attack");

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

    private void EnemyTurn(AttackingEnemy enemy)
    {
        enemy.Turn();
        enemy.incomingDamage = 1;
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (enemy.action == 1)
            {
                float damage = enemy.damage * player.incomingDamage;
                string attacked = enemy.RandomAttack();

                if (attacked == "Meeri") MainManager.playerHealth -= damage;
                if (attacked == "Steel") MainManager.steelHealth -= damage;
                if (attacked == "Gracy") MainManager.gracyHealth -= damage;
                if (attacked == "Stacy") MainManager.stacyHealth -= damage;

                fightText.text = attacked + " Took " + damage + " Damage!";
            }
            else if (enemy.action == 2)
            {
                fightText.text = enemy.type + " Defended";
            }
            enemy.action = 0;
            enemy.turn = false;
            
            timer = wait;
        }
    }

    public void Attack(AttackingEnemy enemy)
    {
        if (!player.turn && partyAI[0].turn)
        {
            PartyAttack(enemy, partyAI[0]);
            return;
        }
        else if (!player.turn && partyAI[1].turn)
        {
            PartyAttack(enemy, partyAI[1]);
            return;
        }
        else if (!player.turn && partyAI[2].turn)
        {
            PartyAttack(enemy, partyAI[2]);
            return;
        }
        fightText.text = enemy.type + " Took " + player.damage * enemy.incomingDamage + " Damage.";
        enemy.health -= player.damage;
        player.animator.SetTrigger("Attack");
        swordSwing.Play();

        targetButtons[0].SetActive(false);
        targetButtons[1].SetActive(false);
        targetButtons[2].SetActive(false);

        Clear();
        player.turn = false;
    }

    private void PartyAttack(AttackingEnemy enemy, PartyFightAI member)
    {
        if (player.turn) return;
        fightText.text = enemy.type + " Took " + member.damage * enemy.incomingDamage + " Damage.";
        enemy.health -= member.damage;
        swordSwing.Play();
        member.animator.SetTrigger("Attack");

        targetButtons[0].SetActive(false);
        targetButtons[1].SetActive(false);
        targetButtons[2].SetActive(false);

        Clear();
        member.turn = false;
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
