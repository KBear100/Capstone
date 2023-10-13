using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] TMP_Text[] itemsText;
    [SerializeField] TMP_Text weaponText;
    [SerializeField] TMP_Text equipmentText;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image[] images;
    [SerializeField] Image weaponImage;
    [SerializeField] public int maxItems = 8;

    [Header("Stats")]
    public List<string> items = new List<string>();
    public int numItems = 0;

    [HideInInspector] public bool usedItem;
    [HideInInspector] public string itemUsed;

    private Image useImage;

    void Start()
    {
        foreach (var item in itemsText) item.text = "";
        weaponText.text = "Fist";
        usedItem = false;
        itemUsed = "";
        useImage = null;
    }

    void Update()
    {
        switch(weaponText.text)
        {
            case "Fist":
                weaponImage.sprite = sprites[3];
                MainManager.weaponMod = 0;
                break;
            case "Dagger":
                weaponImage.sprite = sprites[1];
                MainManager.weaponMod = 2;
                break;
            case "Sword":
                weaponImage.sprite = sprites[2];
                MainManager.weaponMod = 4;
                break;
        }
    }

    public void Clear()
    {
        foreach (var item in itemsText) item.text = "";
        foreach (var image in images) image.sprite = null;
        usedItem = false;
        itemUsed = "";
    }

    public void Display()
    {
        for(int i = 0; i < numItems; i++)
        {
            itemsText[i].text = items[i];
            switch (itemsText[i].text)
            {
                case "Potion":
                    images[i].sprite = sprites[0];
                    break;
                case "Dagger":
                    images[i].sprite = sprites[1];
                    break;
                case "Sword":
                    images[i].sprite = sprites[2];
                    break;

            }
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

                RemoveItem(item, useImage);
            }
        }
        else
        {
            if (item.text == "Potion") MainManager.playerHealth += 10;
            itemUsed = item.text;
            usedItem = true;

            RemoveItem(item, useImage);
        }
    }

    public void GetImage(Image image)
    {
        useImage = image;
    }

    public void Weapon()
    {
        if (equipmentText.text == "Choose New Weapon" || equipmentText.text == "Not a Weapon") equipmentText.text = "Current Weapon";
        else equipmentText.text = "Choose New Weapon";
    }

    private void RemoveItem(TMP_Text item, Image image)
    {
        if (numItems > 0) numItems--;
        items.Remove(item.text);
        item.text = "";
        image.sprite = null;
    }
}
