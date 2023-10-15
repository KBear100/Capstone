using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PartyAI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    [SerializeField] float distance;

    bool team = false;
    Vector2 vel = Vector2.zero;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            rb.velocity = vel;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !team)
        {
            team = true;
            MainManager.partySize++;
        }
    }
}
