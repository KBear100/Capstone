using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyFightAI : MonoBehaviour
{
    [SerializeField] public string member;

    [HideInInspector] public bool turn = false;
    [HideInInspector] public string action;
    [HideInInspector] public float damage;
    [HideInInspector] public float incomingDamage;

    void Start()
    {
        if (this.gameObject.activeInHierarchy) turn = true;
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
        damage = Random.Range(1, 6);
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
