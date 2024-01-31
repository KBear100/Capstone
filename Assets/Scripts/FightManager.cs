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
    [SerializeField] AudioSource shield;
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
    [SerializeField] GameObject[] shieldSprite;
    [SerializeField] float wait;
    [SerializeField] float colorWait;

    float timer;
    float colorTimer;
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

        foreach (var shield in shieldSprite) shield.SetActive(false);
    }

    void Update()
    {
        if(colorTimer > 0)
        {
            colorTimer -= Time.deltaTime;
        }
        else
        {
            player.GetComponent<SpriteRenderer>().color = Color.white;
            partyAI[0].GetComponent<SpriteRenderer>().color = Color.white;
            partyAI[1].GetComponent<SpriteRenderer>().color = Color.white;
            partyAI[2].GetComponent<SpriteRenderer>().color = Color.white;
            enemy[0].GetComponent<SpriteRenderer>().color = Color.white;
            enemy[1].GetComponent<SpriteRenderer>().color = Color.white;
            enemy[2].GetComponent<SpriteRenderer>().color = Color.white;
        }

        playerHealthUI.value = MainManager.playerHealth;
        steelHealthUI.value = MainManager.steelHealth;
        gracyHealthUI.value = MainManager.gracyHealth;
        stacyHealthUI.value = MainManager.stacyHealth;
        enemyHealthUI[0].value = enemy[0].health;
        enemyHealthUI[1].value = enemy[1].health;
        enemyHealthUI[2].value = enemy[2].health;

        foreach (AttackingEnemy foe in enemy)
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
            if(numEnemies == 1) fightText.text = "Enemy Defeated! You gain " + enemy[0].gold  + " gold.";
            if(numEnemies == 2) fightText.text = "Enemies Defeated! You gain " + (enemy[0].gold + enemy[1].gold)  + " gold.";
            if(numEnemies == 3) fightText.text = "Enemies Defeated! You gain " + (enemy[0].gold + enemy[1].gold + enemy[2].gold) + " gold.";

            doneTimer -= Time.deltaTime;
            if (doneTimer <= 0)
            {
                if (numEnemies == 1) MainManager.gold += enemy[0].gold;
                if (numEnemies == 2) MainManager.gold += enemy[0].gold + enemy[1].gold;
                if (numEnemies == 3) MainManager.gold += enemy[0].gold + enemy[1].gold + enemy[2].gold;
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
        if (MainManager.steelHealth <= 0)
        {
            partyAI[0].gameObject.SetActive(false);
            partyAI[0].turn = false;
        }
        if (MainManager.gracyHealth <= 0)
        {
            partyAI[1].gameObject.SetActive(false);
            partyAI[1].turn = false;
        }
        if (MainManager.stacyHealth <= 0)
        {
            partyAI[2].gameObject.SetActive(false);
            partyAI[2].turn = false;
        }

        //Player
        if (player.turn)
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                foreach (AttackingEnemy foe in enemy)
                {
                    foe.health = 1;
                }
            }
            player.incomingDamage = 1;
            shieldSprite[0].SetActive(false);
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
            shieldSprite[1].SetActive(false);
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
            shieldSprite[2].SetActive(false);
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
            shieldSprite[3].SetActive(false);
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
            enemy[0].incomingDamage = 1;
            shieldSprite[4].SetActive(false);
            EnemyTurn(enemy[0], 0);
        }
        else if (enemy[1].turn)
        {
            enemy[1].incomingDamage = 1;
            shieldSprite[5].SetActive(false);
            EnemyTurn(enemy[1], 1);
        }
        else if (enemy[2].turn)
        {
            enemy[2].incomingDamage = 1;
            shieldSprite[6].SetActive(false);
            EnemyTurn(enemy[2], 2);
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
                if (enemy[0].gameObject.activeInHierarchy) targetButtons[0].SetActive(true);
                if (enemy[1].gameObject.activeInHierarchy) targetButtons[1].SetActive(true);
                if(numEnemies == 3) if (enemy[2].gameObject.activeInHierarchy) targetButtons[2].SetActive(true);
                return;
                //To attack function
            }

            float damage = player.damage * enemy[0].incomingDamage;
            fightText.text = enemy[0].type + " Took " + damage + " Damage.";
            enemy[0].health -= damage;
            player.animator.SetTrigger("Attack");
            swordSwing.Play();
            Damaged(enemy[0].gameObject);

            Clear();
            player.turn = false;
        }
        //Defend
        else if (action == "Defend")
        {
            fightText.text = "Meeri Defended";
            player.incomingDamage = 0.5f;
            shield.Play();
            shieldSprite[0].SetActive(true);
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
                if (enemy[0].gameObject.activeInHierarchy) targetButtons[0].SetActive(true);
                if (enemy[1].gameObject.activeInHierarchy) targetButtons[1].SetActive(true);
                if (numEnemies == 3) if (enemy[2].gameObject.activeInHierarchy) targetButtons[2].SetActive(true);
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
            member.incomingDamage = 0.5f;
            shield.Play();
            if (member.member == "Steel") shieldSprite[1].SetActive(true);
            if (member.member == "Gracy") shieldSprite[2].SetActive(true);
            if (member.member == "Stacy") shieldSprite[3].SetActive(true);
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

    private void EnemyTurn(AttackingEnemy enemy, int num)
    {
        enemy.Turn();
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (enemy.action == 1)
            {
                string attacked = enemy.RandomAttack();
                float damage = 0;
                enemy.animator.SetTrigger("Attack");
                enemy.attackSound.Play();

                if (attacked == "Meeri")
                {
                    Damaged(player.gameObject);
                    damage = enemy.damage * player.incomingDamage;
                    MainManager.playerHealth -= damage;
                    fightText.text = attacked + " Took " + damage + " Damage!";
                }
                if (attacked == "Steel")
                {
                    Damaged(partyAI[0].gameObject);
                    damage = enemy.damage * partyAI[0].incomingDamage;
                    MainManager.steelHealth -= damage;
                    fightText.text = attacked + " Took " + damage + " Damage!";
                }
                if (attacked == "Gracy")
                {
                    Damaged(partyAI[1].gameObject);
                    damage = enemy.damage * partyAI[1].incomingDamage;
                    MainManager.gracyHealth -= damage;
                    fightText.text = attacked + " Took " + damage + " Damage!";
                }
                if (attacked == "Stacy")
                {
                    Damaged(partyAI[2].gameObject);
                    damage = enemy.damage * partyAI[2].incomingDamage;
                    MainManager.stacyHealth -= damage;
                    fightText.text = attacked + " Took " + damage + " Damage!";
                }
            }
            else if (enemy.action == 2)
            {
                fightText.text = enemy.type + " Defended";
                enemy.incomingDamage = 0.5f;
                shield.Play();
                if(num == 0) shieldSprite[4].SetActive(true);
                if(num == 1) shieldSprite[5].SetActive(true);
                if(num == 2) shieldSprite[6].SetActive(true);
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
        float damage = player.damage * enemy.incomingDamage;
        fightText.text = enemy.type + " Took " + damage + " Damage.";
        enemy.health -= damage;
        player.animator.SetTrigger("Attack");
        swordSwing.Play();
        Damaged(enemy.gameObject);

        targetButtons[0].SetActive(false);
        targetButtons[1].SetActive(false);
        targetButtons[2].SetActive(false);

        Clear();
        player.turn = false;
    }

    private void PartyAttack(AttackingEnemy enemy, PartyFightAI member)
    {
        if (player.turn) return;
        float damage = member.damage * enemy.incomingDamage;
        fightText.text = enemy.type + " Took " + damage + " Damage.";
        enemy.health -= damage;
        swordSwing.Play();
        member.animator.SetTrigger("Attack");
        Damaged(enemy.gameObject);

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

    public void Damaged(GameObject charater)
    {
        Color damageColor = Color.red;

        charater.GetComponent<SpriteRenderer>().color = damageColor;

        colorTimer = colorWait;
    }
}
