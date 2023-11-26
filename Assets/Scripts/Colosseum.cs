using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Colosseum : MonoBehaviour
{
    [SerializeField] float wait;
    [SerializeField] float endDelay;
    [SerializeField] GameObject background;

    float timer;
    bool complete = false;

    private void Start()
    {
        timer = wait;
        complete = false;
    }

    private void Update()
    {
        if(complete)
        {
            MainManager.forestMusic.Stop();
            MainManager.pause = true;
            timer -= Time.deltaTime;
            if (timer <= 0 && MainManager.dialogSystem.text.text == "Finally, the colosseum! Me and Silver will do great here!")
            {
                MainManager.dialogSystem.ShowDialog("Steel, is Silver even real? He hasn't been around at all.", "Meeri");
                timer = wait;
            }
            else if (timer <= 0)
            {
                MainManager.dialogSystem.ShowDialog("Of course he is! ... of course he is ...", "Steel");
                endDelay -= Time.deltaTime;
            }

            if(endDelay <= 0)
            {
                SceneManager.LoadSceneAsync("Title", LoadSceneMode.Single);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(MainManager.partySize == 3)
            {
               MainManager.dialogSystem.ShowDialog("Finally, the colosseum! Me and Silver will do great here!", "Steel");
                background.SetActive(true);
               complete = true;
            }
            else
            {
                MainManager.dialogSystem.ShowDialog("I should gather party members first.", "Meeri");
                MainManager.dialogSystem.talking = true;
            }
        }
    }
}
