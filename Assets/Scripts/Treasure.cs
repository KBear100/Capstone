using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Treasure : MonoBehaviour
{
    [SerializeField] AudioSource sound;
    float timer = 2;
    bool found = false;

    private void Update()
    {
        if(found)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                MainManager.dialogSystem.ExitDialog();
                MainManager.pause = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            sound.Play();
            MainManager.inventory.items.Add("Potion");
            MainManager.inventory.numItems++;
            MainManager.dialogSystem.ShowDialogWithoutName("You found a potion");
            MainManager.pause = true;
            found = true;
        }
    }
}
