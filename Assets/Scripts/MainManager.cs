using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [SerializeField] GameObject invUI;
    [SerializeField] Inventory inv;
    [SerializeField] DialogSystem ds;
    [SerializeField] GameObject ps;

    public static Inventory inventory;
    public static GameObject inventoryUI;
    public static DialogSystem dialogSystem;
    public static GameObject pauseScreen;
    public static float gold = 100;
    public static float playerHealth = 100;
    public static float steelHealth = 100;
    public static float gracyHealth = 100;
    public static float stacyHealth = 100;
    public static bool pause = false;
    public static float weaponMod = 0;
    public static int partySize = 0;
    public static List<string> partyMembers = new List<string>();
    public static string[] enemyTypes = {"Zombie", "Ninja", "Sniper"};
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

        if(pauseScreen == null)
        {
            pauseScreen = ps;
        }

        if (dialogSystem == null) dialogSystem = ds;

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if (playerHealth > maxHealth) playerHealth = maxHealth;
        if (steelHealth > maxHealth) steelHealth = maxHealth;
        if (gracyHealth > maxHealth) gracyHealth = maxHealth;
        if (stacyHealth > maxHealth) stacyHealth = maxHealth;
    }

    public static void ExitInventory()
    {
        inventoryUI.SetActive(false);
        inventory.Clear();
    }

    public static void Pause()
    {
        pause = true;
        pauseScreen.SetActive(true);
    }

    public static void Resume()
    {
        pause = false;
        pauseScreen.SetActive(false);
    }

    public void Title()
    {
        pause = false;
        Destroy(gameObject);
        SceneManager.LoadSceneAsync("Title", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
