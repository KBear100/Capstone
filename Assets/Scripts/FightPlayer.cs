using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class FightPlayer : MonoBehaviour
{
    [SerializeField] public Animator animator;

    [HideInInspector] public bool turn = true;
    [HideInInspector] public string action;
    [HideInInspector] public float damage;
    [HideInInspector] public float incomingDamage;

    void Start()
    {
        incomingDamage = 1;
    }

    void Update()
    {
        if (MainManager.inventory.usedItem && turn)
        {
            action = "Item";
            MainManager.inventory.usedItem = false;
            MainManager.ExitInventory();
        }
    }

    public void Attack()
    {
        damage = Random.Range(1, 6) + MainManager.weaponMod;
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
