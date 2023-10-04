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
    List<GameObject> inventory;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        vel.x = Input.GetAxis("Horizontal") * speed;
        vel.y = Input.GetAxis("Vertical") * speed;

        rb.velocity = vel;
    }
}
