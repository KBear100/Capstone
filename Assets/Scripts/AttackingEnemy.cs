using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingEnemy : MonoBehaviour
{
    public float health;
    public float gold;
    public float incomingDamage;
    public int damage;
    public int turn;
    public string type;
    public int maxDamage;

    void Start()
    {
        maxDamage = 5;
        type = MainManager.enemyTypes[Random.Range(0, MainManager.enemyTypes.Length)];

        switch(type)
        {
            case "Zombie":
                maxDamage = 6;
                health = 10;
                break;
            case "Ninja":
                maxDamage = 9;
                health = 15;
                break;
            case "Sniper":
                maxDamage = 11;
                health = 20;
                break;
        }

        incomingDamage = 1;
    }

    void Update()
    {
        
    }

    public void Turn()
    {
        turn = Random.Range(1, 3);
        switch(turn)
        {
            case 1:
                damage = Random.Range(0, maxDamage + 1);
                break;
            case 2:
                incomingDamage = 0.5f;
                break;
        }
    }
}
