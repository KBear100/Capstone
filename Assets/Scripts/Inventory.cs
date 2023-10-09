using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] TMP_Text[] itemsText;
    [SerializeField] TMP_Text weaponText;
    [SerializeField] TMP_Text equipmentText;
    [SerializeField] public int maxItems = 8;

    [Header("Stats")]
    public List<string> items = new List<string>();
    public int numItems = 0;

    [HideInInspector] public bool usedItem;
    [HideInInspector] public string itemUsed;

    void Start()
    {
        foreach (var item in itemsText) item.text = "";
        weaponText.text = "Fist";
        usedItem = false;
        itemUsed = "";
    }

    void Update()
    {
        switch(weaponText.text)
        {
            case "Fist":
                MainManager.weaponMod = 0;
                break;
            case "Dagger":
                MainManager.weaponMod = 2;
                break;
            case "Sword":
                MainManager.weaponMod = 4;
                break;
        }
    }

    public void Clear()
    {
        foreach (var item in itemsText) item.text = "";
        usedItem = false;
        itemUsed = "";
    }

    public void Display()
    {
        for(int i = 0; i < numItems; i++)
        {
            itemsText[i].text = items[i];
        }
    }

    public void Use(TMP_Text item)
    {
        if (item.text == "") return;
        if (equipmentText.text == "Choose New Weapon" || equipmentText.text == "Not a Weapon")
        {
            if (item.text == "Potion") equipmentText.text = "Not a Weapon";
            else
            {
                weaponText.text = item.text;
                equipmentText.text = "Current Weapon";

                RemoveItem(item);
            }
        }
        else
        {
            if (item.text == "Potion") MainManager.playerHealth += 10;
            itemUsed = item.text;
            usedItem = true;

            RemoveItem(item);
        }
    }

    public void Weapon()
    {
        if (equipmentText.text == "Choose New Weapon" || equipmentText.text == "Not a Weapon") equipmentText.text = "Current Weapon";
        else equipmentText.text = "Choose New Weapon";
    }

    private void RemoveItem(TMP_Text item)
    {
        if (numItems > 0) numItems--;
        items.Remove(item.text);
        item.text = "";
    }
}
