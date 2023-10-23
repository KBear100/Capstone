using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    [Header("Anmation")]
    [SerializeField] Animator animator;

    Vector2 vel = Vector2.zero;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    bool faceRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        vel.x = Input.GetAxis("Horizontal") * speed;
        vel.y = Input.GetAxis("Vertical") * speed;

        if(vel.x < 0 && faceRight) FlipSprite();
        if(vel.x > 0 && !faceRight) FlipSprite();

        if (MainManager.freezePlayer) vel = Vector2.zero;

        if (vel.x > 0 || vel.y > 0) animator.SetFloat("Speed", 1);
        else animator.SetFloat("Speed", 0);
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

    private void FlipSprite()
    {
        faceRight = !faceRight;
        spriteRenderer.flipX = !faceRight;
    }

    public void TalkToSteel()
    {

    }
    public void TalkToGracy()
    {

    }
    public void TalkToStacy()
    {

    }
}
