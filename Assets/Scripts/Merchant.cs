using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Merchant : MonoBehaviour
{
    [SerializeField] GameObject ShopUI;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShopUI.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ShopUI.gameObject.SetActive(false);
    }

    public void ExitShop()
    {
        ShopUI.gameObject.SetActive(false);
    }
}
