using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] AudioSource footstep;
    [Header("Anmation")]
    [SerializeField] Animator animator;

    Vector2 vel = Vector2.zero;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    bool faceRight = true;
    bool firstInv = true;
    [HideInInspector] public string talkingTo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        vel.x = Input.GetAxis("Horizontal") * speed;
        vel.y = Input.GetAxis("Vertical") * speed;

        if (MainManager.pause) vel = Vector2.zero;

        if(vel.x < 0 && faceRight) FlipSprite();
        if(vel.x > 0 && !faceRight) FlipSprite();

        if (vel.x != 0 || vel.y != 0)
        {
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
            footstep.Play();
        }
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
                if(firstInv)
                {
                    MainManager.tutorial.tutorialTxt.text = "Click on the current weapon slot than select the weapon you wish to replace it with.";
                    MainManager.tutorial.active = true;
                    firstInv = false;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(MainManager.inventoryUI.activeInHierarchy) MainManager.ExitInventory();
            else if(!MainManager.pause) MainManager.Pause();
            else if(MainManager.pause) MainManager.Resume();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            switch(talkingTo)
            {
                case "Steel":
                    MainManager.dialogSystem.StartSystem(talkingTo, 2, 2);
                    break;
                case "Gracy":
                    MainManager.dialogSystem.StartSystem(talkingTo, 1, 1);
                    break;
                case "Stacy":
                    MainManager.dialogSystem.StartSystem(talkingTo, 1, 1);
                    break;
            }
        }
    }

    private void FlipSprite()
    {
        faceRight = !faceRight;
        spriteRenderer.flipX = !faceRight;
    }
}
