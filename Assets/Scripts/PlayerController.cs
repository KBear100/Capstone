using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;

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
            if (MainManager.inventoryUI.activeInHierarchy)
            {
                MainManager.ExitInventory();
            }
            else
            {
                MainManager.inventoryUI.SetActive(true);
                MainManager.inventory.Display();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainManager.ExitInventory();
        }
    }
}
