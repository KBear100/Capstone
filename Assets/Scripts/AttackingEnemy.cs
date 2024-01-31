using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingEnemy : MonoBehaviour
{
    [Header("Looks")]
    [SerializeField] Sprite[] sprites;
    [SerializeField] public Animator animator;
    [SerializeField] public RuntimeAnimatorController[] controllers;
    [Header("Sounds")]
    [SerializeField] public AudioSource attackSound;
    [SerializeField] AudioClip[] soundEffects;
    [HideInInspector] public float health;
    [HideInInspector] public float gold;
    [HideInInspector] public float incomingDamage;
    [HideInInspector] public int damage;
    [HideInInspector] public int action = 0;
    [HideInInspector] public bool turn = false;
    [HideInInspector] public string type;
    [HideInInspector] public int maxDamage;

    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        type = MainManager.enemyTypes[Random.Range(0, MainManager.enemyTypes.Length)];
        switch(type)
        {
            case "Zombie":
                spriteRenderer.sprite = sprites[0];
                maxDamage = 6;
                health = 15;
                gold = 3;
                animator.runtimeAnimatorController = controllers[0];
                attackSound.clip = soundEffects[0];
                break;
            case "Ninja":
                spriteRenderer.sprite = sprites[1];
                maxDamage = 11;
                health = 20;
                gold = 5;
                animator.runtimeAnimatorController = controllers[1];
                attackSound.clip = soundEffects[1];
                break;
            case "Sniper":
                spriteRenderer.sprite = sprites[2];
                maxDamage = 16;
                health = 30;
                gold = 7;
                animator.runtimeAnimatorController = controllers[2];
                attackSound.clip = soundEffects[2];
                break;
        }

        incomingDamage = 1;
    }

    void Update()
    {
        
    }

    public void Turn()
    {
        action = Random.Range(1, 3);
        switch(action)
        {
            case 1:
                damage = Random.Range(0, maxDamage + 1);
                break;
            case 2:
                incomingDamage = 0.5f;
                break;
        }
    }

    public string RandomAttack()
    {
        int rand = Random.Range(1, 5);
        if (rand == 1) return "Meeri";
        else if (rand == 2 && MainManager.partyMembers.Contains("Steel")) return "Steel";
        else if (rand == 3 && MainManager.partyMembers.Contains("Gracy")) return "Gracy";
        else if (rand == 4 && MainManager.partyMembers.Contains("Stacy")) return "Stacy";
        else
        {
            return "Meeri";
        }
    }
}
