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
    [Header("Dialog")]
    [SerializeField] float dialogTimer;
    [Header("Anmation")]
    [SerializeField] Animator animator;
    
    [HideInInspector]public bool steelTalk = false;
    [HideInInspector]public bool gracyTalk = false;
    [HideInInspector]public bool stacyTalk = false;

    Vector2 vel = Vector2.zero;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    bool faceRight = true;
    bool talking = false;
    float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = dialogTimer;
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
            if(steelTalk)
            {
                talking = true;
                MainManager.dialogSystem.ShowDialog(MainManager.dialogSystem.steelDialog.dialog[2]);
            }

            if (gracyTalk)
            {
                talking = true;
                MainManager.dialogSystem.ShowDialog(MainManager.dialogSystem.gracyDialog.dialog[1]);
            }

            if (stacyTalk)
            {
                talking = true;
                MainManager.dialogSystem.ShowDialog(MainManager.dialogSystem.stacyDialog.dialog[1]);
            }
        }

        if(talking)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                MainManager.dialogSystem.ExitDialog();
                timer = dialogTimer;
                talking = false;
            }
        }
    }

    private void FlipSprite()
    {
        faceRight = !faceRight;
        spriteRenderer.flipX = !faceRight;
    }
}
