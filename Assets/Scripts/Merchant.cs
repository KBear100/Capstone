using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Merchant : MonoBehaviour
{
    [SerializeField] GameObject shopUI;
    [SerializeField] Inventory inventory;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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

    public void Buy(TMP_Text item)
    {
        if(inventory.items.Count == inventory.maxItems)
        {
            Debug.Log("Full");
            return;
        }
        inventory.numItems++;
        inventory.items.Add(item.text);
    }
}
