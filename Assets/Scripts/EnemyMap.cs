using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMap : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] GameObject[] waypoints;
    [SerializeField] float waitTimer;

    Vector2 vel = Vector2.zero;
    Rigidbody2D rb;
    float timer = 0f;
    Transform targetWaypoint = null;

    enum State
    {
        IDLE,
        PATROL,
        FIGHT
    }

    State state = State.IDLE;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = waitTimer;
        SetWayPoint();
    }

    void Update()
    {
        Vector2 dir = Vector2.zero;

        switch(state)
        {
            case State.IDLE:
                timer -= Time.deltaTime;
                if(timer <= 0f)
                {
                    timer = waitTimer;
                    SetWayPoint();
                    state = State.PATROL;
                }
                break;

            case State.PATROL:
                dir.x = Mathf.Sign(targetWaypoint.position.x - transform.position.x);
                dir.y = Mathf.Sign(targetWaypoint.position.y - transform.position.y);

                float x = Mathf.Abs(transform.position.x - targetWaypoint.position.x);
                float y = Mathf.Abs(transform.position.y - targetWaypoint.position.y);

                if (x < 0.25f && y < 0.25f)
                {
                    state = State.IDLE;
                }
                else if(x < 0.25f)
                {
                    dir.x = 0;
                }
                else if(y < 0.25f)
                {
                    dir.y = 0;
                }
                break;
            case State.FIGHT:
                Destroy(gameObject);
                break;
        }

        if (MainManager.pause) dir = Vector2.zero;

        vel = dir * speed;
        rb.velocity = vel;
    }

    void SetWayPoint()
    {
        Transform temp = null;
        while(temp == targetWaypoint || !temp)
        {
            int i = Random.Range(0, waypoints.Length);
            temp = waypoints[i].transform;
        }
        targetWaypoint = temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadSceneAsync("Fight1", LoadSceneMode.Additive);
            MainManager.pause = true;
            state = State.FIGHT;
        }
    }
}
