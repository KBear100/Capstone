using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] TMP_Text[] itemsText;
    [SerializeField] TMP_Text weaponText;
    [SerializeField] TMP_Text equipmentText;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Sprite defaultBackground;
    [SerializeField] Image[] images;
    [SerializeField] GameObject[] partyButtons;
    [SerializeField] TMP_Text[] partyHealth;
    [SerializeField] Image weaponImage;
    [SerializeField] public int maxItems = 8;
    [SerializeField] AudioSource cork;

    [Header("Stats")]
    public List<string> items = new List<string>();
    public int numItems = 0;

    [HideInInspector] public bool usedItem;
    [HideInInspector] public TMP_Text itemUsed;

    private Image useImage;

    void Start()
    {
        foreach (var item in itemsText) item.text = "";
        weaponText.text = "Fist";
        usedItem = false;
        itemUsed = null;
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
        foreach (var image in images) image.sprite = defaultBackground;
        foreach (var button in partyButtons) button.SetActive(false);
        foreach (var health in partyHealth) health.gameObject.SetActive(false);
        equipmentText.text = "Current Weapon";
        usedItem = false;
        itemUsed = null;
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
        else if (item.text == "Potion")
        {
            partyButtons[0].SetActive(true);
            partyHealth[0].gameObject.SetActive(true);
            partyHealth[0].text = MainManager.playerHealth + "/100";

            if (MainManager.partyMembers.Contains("Steel"))
            {
                partyButtons[1].SetActive(true);
                partyHealth[1].gameObject.SetActive(true);
                partyHealth[1].text = MainManager.steelHealth + "/100";
            }
            if (MainManager.partyMembers.Contains("Gracy"))
            {
                partyButtons[2].SetActive(true);
                partyHealth[2].gameObject.SetActive(true);
                partyHealth[2].text = MainManager.gracyHealth + "/100";
            }
            if (MainManager.partyMembers.Contains("Stacy"))
            {
                partyButtons[3].SetActive(true);
                partyHealth[3].gameObject.SetActive(true);
                partyHealth[3].text = MainManager.stacyHealth + "/100";
            }

            itemUsed = item;
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
        image.sprite = defaultBackground;
    }

    public void UseOnParty(TMP_Text player)
    {
        if (player.text == "Meeri") MainManager.playerHealth += 10;
        if (player.text == "Steel") MainManager.steelHealth += 10;
        if (player.text == "Gracy") MainManager.gracyHealth += 10;
        if (player.text == "Stacy") MainManager.stacyHealth += 10;

        cork.Play();

        usedItem = true;
        RemoveItem(itemUsed, useImage);

        foreach (var button in partyButtons) button.SetActive(false);
        foreach (var health in partyHealth) health.gameObject.SetActive(false);
    }
}
