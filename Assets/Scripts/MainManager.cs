using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [SerializeField] GameObject invUI;
    [SerializeField] Inventory inv;
    [SerializeField] DialogSystem ds;
    [SerializeField] Settings settingsScript;
    [SerializeField] Tutorial tutorialScript;
    [SerializeField] GameObject ps;
    [SerializeField] GameObject settingsScreen;
    [SerializeField] AudioSource music1;

    public static Inventory inventory;
    public static GameObject inventoryUI;
    public static DialogSystem dialogSystem;
    public static Settings settings;
    public static GameObject pauseScreen;
    public static GameObject settingsUI;
    public static Tutorial tutorial;
    public static AudioSource forestMusic;
    public static float gold = 100;
    public static float playerHealth = 100;
    public static float steelHealth = 100;
    public static float gracyHealth = 100;
    public static float stacyHealth = 100;
    public static bool pause = false;
    public static bool destroySilver = false;
    public static bool destroyManager = false;
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

        if(inventoryUI == null) inventoryUI = invUI;

        if(inventory == null) inventory = inv;

        if(pauseScreen == null) pauseScreen = ps;

        if(forestMusic == null) forestMusic = music1;

        if (dialogSystem == null) dialogSystem = ds;

        if (settingsUI == null) settingsUI = settingsScreen;

        if (tutorial == null) tutorial = tutorialScript;
        
        destroyManager = false;
        pause = false;

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if (playerHealth > maxHealth) playerHealth = maxHealth;
        if (steelHealth > maxHealth) steelHealth = maxHealth;
        if (gracyHealth > maxHealth) gracyHealth = maxHealth;
        if (stacyHealth > maxHealth) stacyHealth = maxHealth;

        if (destroyManager) DestroyManager();
    }

    public static void ExitInventory()
    {
        inventoryUI.SetActive(false);
        inventory.Clear();
    }

    public void DestroyManager()
    {
        destroyManager = false;
        Destroy(gameObject);
    }

    public static void Pause()
    {
        pause = true;
        pauseScreen.SetActive(true);
        settingsUI.SetActive(false);
    }

    public static void Resume()
    {
        pause = false;
        pauseScreen.SetActive(false);
    }

    public void Settings()
    {
        if(!settingsUI.activeInHierarchy) settingsUI.SetActive(true);
        else settingsUI.SetActive(false);
    }

    public void Title()
    {
        Destroy(gameObject);
        SceneManager.LoadSceneAsync("Title", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
