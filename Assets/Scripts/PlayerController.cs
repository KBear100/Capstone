using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    [Header("Inventory")]
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject inventoryUI;

    Vector2 vel = Vector2.zero;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        vel.x = Input.GetAxis("Horizontal") * speed;
        vel.y = Input.GetAxis("Vertical") * speed;

        rb.velocity = vel;

        if(Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.activeInHierarchy)
            {
                inventoryUI.SetActive(false);
                inventory.Clear();
            }
            else
            {
                inventoryUI.SetActive(true);
                inventory.Display();
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)) inventoryUI.SetActive(false);
    }
}
