using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class FightPlayer : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] Inventory inventory;
    [Header("Enemy")]
    [SerializeField] GameObject enemy;

    int weaponMod;
    int damage;
    int incomingDamage;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Attack()
    {
        damage = Random.Range(1, 5) + weaponMod;

    }

    public void Defend()
    {

    }

    public void Item(TMP_Text item)
    {
        inventory.items.Remove(item.text);
        item.text = "";
        if (inventory.numItems > 0) inventory.numItems--;
    }
}
