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
    [SerializeField] float dialogTimer;
    [Header("Animation")]
    [SerializeField] Animator animator;

    bool team = false;
    bool faceRight = true;
    bool steelIntro = false;
    bool gracyIntro = false;
    bool stacyIntro = false;
    Vector2 vel = Vector2.zero;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Physics2D.IgnoreCollision(player.gameObject.GetComponent<Collider2D>(), GetComponent<CapsuleCollider2D>());
        team = false;
        timer = dialogTimer;
    }

    void Update()
    {
        if(steelIntro) SteelIntro();
        if(gracyIntro) GracyIntro();
        if(stacyIntro) StacyIntro();
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !team)
        {
            if (partyMember == "Steel")
            {
                steelIntro = true;
            }

            if(partyMember == "Gracy")
            {
                gracyIntro = true;
            }

            if (partyMember == "Stacy")
            {
                stacyIntro = true;
            }
        }

        if(collision.tag == "Player")
        {
            if (partyMember == "Steel")
            {
                player.GetComponent<PlayerController>().steelTalk = true;
            }

            if (partyMember == "Gracy")
            {
                player.GetComponent<PlayerController>().gracyTalk = true;
            }

            if (partyMember == "Stacy")
            {
                player.GetComponent<PlayerController>().stacyTalk = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (partyMember == "Steel")
            {
                player.GetComponent<PlayerController>().steelTalk = false;
            }

            if (partyMember == "Gracy")
            {
                player.GetComponent<PlayerController>().gracyTalk = false;
            }

            if (partyMember == "Stacy")
            {
                player.GetComponent<PlayerController>().stacyTalk = false;
            }
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

    private void SteelIntro()
    {
        MainManager.freezePlayer = true;
        if(MainManager.dialogSystem.text.text == "") MainManager.dialogSystem.ShowDialog(MainManager.dialogSystem.steelDialog.dialog[0]);
        timer -= Time.deltaTime;
        if (timer <= 0 && MainManager.dialogSystem.text.text == MainManager.dialogSystem.steelDialog.dialog[1])
        {
            MainManager.dialogSystem.ExitDialog();
            Destroy(silver);
            JoinParty();
            MainManager.freezePlayer = false;
            timer = dialogTimer;
            steelIntro = false;
        }
        else if (timer <= 0)
        {
            timer = dialogTimer;
            MainManager.dialogSystem.ShowDialog(MainManager.dialogSystem.steelDialog.dialog[1]);
        }
    }

    private void GracyIntro()
    {
        MainManager.freezePlayer = true;
        if (MainManager.dialogSystem.text.text == "") MainManager.dialogSystem.ShowDialog(MainManager.dialogSystem.gracyDialog.dialog[0]);
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            MainManager.dialogSystem.ExitDialog();
            JoinParty();
            MainManager.freezePlayer = false;
            gracyIntro = false;
            timer = dialogTimer;
        }
    }

    private void StacyIntro()
    {
        MainManager.freezePlayer = true;
        if (MainManager.dialogSystem.text.text == "") MainManager.dialogSystem.ShowDialog(MainManager.dialogSystem.stacyDialog.dialog[0]);
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            MainManager.dialogSystem.ExitDialog();
            JoinParty();
            MainManager.freezePlayer = false;
            stacyIntro = false;
            timer = dialogTimer;
        }
    }
}
