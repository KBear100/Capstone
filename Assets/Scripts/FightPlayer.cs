using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class FightPlayer : MonoBehaviour
{
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
        if (MainManager.inventory.usedItem)
        {
            action = "Item";
            MainManager.inventory.usedItem = false;
            MainManager.ExitInventory();
        }
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

    public void Item()
    {
        MainManager.inventory.Display();
        MainManager.inventoryUI.SetActive(true);
    }
}
