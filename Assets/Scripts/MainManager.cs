using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject invUI;
    [SerializeField] Inventory inv;

    public static Inventory inventory;
    public static GameObject inventoryUI;
    public static float gold = 0;
    public static float playerHealth = 100;
    public static float weaponMod = 0;
    public static MainManager instance;

    private float maxHealth = 100;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        if(inventoryUI == null)
        {
            inventoryUI = invUI;
        }

        if(inventory == null)
        {
            inventory = inv;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if (playerHealth > maxHealth) playerHealth = maxHealth;
    }

    public static void ExitInventory()
    {
        inventoryUI.SetActive(false);
        inventory.Clear();
    }
}
