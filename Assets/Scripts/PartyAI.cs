using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PartyAI : MonoBehaviour
{
    [SerializeField] string partyMember;
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    [SerializeField] float distance;

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

            rb.velocity = vel;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !team)
        {
            team = true;
            MainManager.partySize++;
            MainManager.partyMembers.Add(partyMember);
        }
    }

    private void FlipSprite()
    {
        faceRight = !faceRight;
        spriteRenderer.flipX = !faceRight;
    }
}
