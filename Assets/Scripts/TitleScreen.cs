using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] float wait;
    [SerializeField] GameObject background;
    [SerializeField] Image black;
    [SerializeField] Image backgroundImage;
    [SerializeField] TMP_Text story;

    float fade = 2f;
    float timer;
    bool started;
    bool open;

    private void Start()
    {
        background.SetActive(true);
        story.gameObject.SetActive(false);
        black.gameObject.SetActive(false);
        timer = wait;
    }

    private void Update()
    {
        if (started)
        {
            black.gameObject.SetActive(true);
            Color fadeColor = black.color;

            fadeColor.a += 0.5f * Time.deltaTime;
            black.color = fadeColor;
            
            fade -= Time.deltaTime;
            if (fade <= 0)
            {
                story.text = "A brave adventurer named Meeri left her home town to try and fight the strange being tormenting the land.";
                background.SetActive(false);
                story.gameObject.SetActive(true);

                timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    story.text = "You must follow her on her adventure in the great land known as Kear.";
                    story.CrossFadeAlpha(0, wait, true);
                    open = true;
                    timer = wait;
                }
            }
        }
        
        if(open)
        {
            started = false;

            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                open = false;
                OpenGame();
            }
        }
    }

    public void StartGame()
    {
        started = true;
        //SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Single);
    }

    private void OpenGame()
    {
        SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
