using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] float wait;
    bool shown;
    float timer;

    private void Update()
    {
        if (shown)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                MainManager.dialogSystem.ExitDialog();
                shown = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            MainManager.dialogSystem.ShowDialog("I should keep going towards the colosseum");
            timer = wait;
            shown = true;
        }
    }
}
