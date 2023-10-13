using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingEnemy : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [HideInInspector] public float health;
    [HideInInspector] public float gold;
    [HideInInspector] public float incomingDamage;
    [HideInInspector] public int damage;
    [HideInInspector] public int turn;
    [HideInInspector] public string type;
    [HideInInspector] public int maxDamage;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        maxDamage = 5;
        type = MainManager.enemyTypes[Random.Range(0, MainManager.enemyTypes.Length)];

        switch(type)
        {
            case "Zombie":
                spriteRenderer.sprite = sprites[0];
                maxDamage = 6;
                health = 10;
                break;
            case "Ninja":
                spriteRenderer.sprite = sprites[1];
                maxDamage = 9;
                health = 15;
                break;
            case "Sniper":
                spriteRenderer.sprite = sprites[2];
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
