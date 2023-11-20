using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PartyAI : MonoBehaviour
{
    [Header("Party")]
    [SerializeField] string partyMember;
    [SerializeField] GameObject silver;
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    [SerializeField] float distance;
    [Header("Animation")]
    [SerializeField] Animator animator;

    bool team = false;
    bool faceRight = true;
    Vector2 vel = Vector2.zero;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Physics2D.IgnoreCollision(player.gameObject.GetComponent<Collider2D>(), GetComponent<CapsuleCollider2D>());
        team = false;
    }

    void Update()
    {
        if (team)
        {
            Vector2 dir = Vector2.zero;

            dir.x = Mathf.Sign(player.transform.position.x - transform.position.x);
            dir.y = Mathf.Sign(player.transform.position.y - transform.position.y);

            float x = Mathf.Abs(transform.position.x - player.transform.position.x);
            float y = Mathf.Abs(transform.position.y - player.transform.position.y);
        
            if (x < distance)
            {
                dir.x = 0;
            }
            if (y < distance)
            {
                dir.y = 0;
            }

            vel = dir * speed;

            if (vel.x < 0 && faceRight) FlipSprite();
            if (vel.x > 0 && !faceRight) FlipSprite();

            if (vel.x != 0 || vel.y != 0) animator.SetFloat("Speed", 1);
            else animator.SetFloat("Speed", 0);

            rb.velocity = vel;
        }
        if (MainManager.destroySilver == true) Destroy(silver);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !team)
        {
            if (partyMember == "Steel")
            {
                MainManager.dialogSystem.StartSystem("Steel", 0, 1);
                MainManager.pause = true;
            }

            if(partyMember == "Gracy")
            {
                MainManager.dialogSystem.StartSystem("Gracy", 0, 0);
                MainManager.pause = true;
            }

            if (partyMember == "Stacy")
            {
                MainManager.dialogSystem.StartSystem("Stacy", 0, 0);
                MainManager.pause = true;
            }
            JoinParty();
        }

        if(collision.tag == "Player")
        {
            if (partyMember == "Steel")
            {
                player.GetComponent<PlayerController>().talkingTo = "Steel";
            }

            if (partyMember == "Gracy")
            {
                player.GetComponent<PlayerController>().talkingTo = "Gracy";
            }

            if (partyMember == "Stacy")
            {
                player.GetComponent<PlayerController>().talkingTo = "Stacy";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.GetComponent<PlayerController>().talkingTo = "";
        }
    }

    private void FlipSprite()
    {
        faceRight = !faceRight;
        spriteRenderer.flipX = !faceRight;
    }

    private void JoinParty()
    {
        team = true;
        MainManager.partySize++;
        MainManager.partyMembers.Add(partyMember);
    }
}
