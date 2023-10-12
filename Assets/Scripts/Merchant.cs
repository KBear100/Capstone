using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider2D))]

public class Merchant : MonoBehaviour
{
    [SerializeField] GameObject shopUI;
    [SerializeField] TMP_Text goldText;
    [SerializeField] TMP_Text shoppingText;
    [SerializeField] Inventory inventory;

    private string item;

    private void Start()
    {
        shoppingText.text = "Welcome to my Shop!";
    }

    private void Update()
    {
        goldText.text = "Your gold: " + MainManager.gold.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        shoppingText.text = "Welcome to my Shop!";
        shopUI.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        shopUI.gameObject.SetActive(false);
    }

    public void ExitShop()
    {
        shopUI.gameObject.SetActive(false);
    }

    public void Buy(TMP_Text price)
    {
        if (inventory.items.Count == inventory.maxItems)
        {
            shoppingText.text = "You Can't Carry Any More";
            return;
        }

        float cost = float.Parse(price.text.Remove(price.text.IndexOf('g')));

        if(MainManager.gold < cost) shoppingText.text = "You Don't Have Enough Gold!";
        else
        {
            Add(item);
            MainManager.gold -= cost;
        }
    }

    public void GetItem(TMP_Text item)
    {
        this.item = item.text;
    }

    public void Add(string item)
    {
        inventory.numItems++;
        inventory.items.Add(item);
    }
}
