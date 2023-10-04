using System.Collections;
using System.Collections.Generic;
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

    public void Buy(string item)
    {
        inventory.numItems++;
        inventory.items.Add(item);
    }
}
