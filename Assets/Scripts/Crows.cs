using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crows : MonoBehaviour
{
    [SerializeField] GameObject crow;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float wait;
    [SerializeField] float speed;

    float timer;
    bool moving = false;

    void Start()
    {
        timer = wait;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {

            if (!moving)
            {
                crow.transform.position = spawnPoint.position;
                moving = true;
            }
            else
            {
                crow.transform.position += Vector3.right * speed;
                moving = true;
            }

            if(crow.transform.position.x >= 100)
            {
                timer = wait;
                moving = false;
            }
        }
    }
}
