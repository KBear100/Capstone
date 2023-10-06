using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class FightPlayer : MonoBehaviour
{
    [Header("Stats")]
    public float health = 100;
    [SerializeField] Inventory inventory;
    [Header("Enemy")]
    [SerializeField] GameObject enemy;

    [HideInInspector] public bool turn = true;
    [HideInInspector] public string action;
    [HideInInspector] public int damage;
    [HideInInspector] public float incomingDamage;

    int weaponMod;

    void Start()
    {
        incomingDamage = 1;
    }

    void Update()
    {
        
    }

    public void Attack()
    {
        damage = Random.Range(1, 5) + weaponMod;
        action = "Attack";
    }

    public void Defend()
    {
        incomingDamage = 0.5f;
        action = "Defend";
    }

    public void Item(TMP_Text item)
    {
        inventory.items.Remove(item.text);
        item.text = "";
        if (inventory.numItems > 0) inventory.numItems--;
        action = "Item";
    }
}
