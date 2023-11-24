using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] float wait;
    public TMP_Text tutorialTxt;

    float timer;
    [HideInInspector] public bool active = true;

    void Start()
    {
        timer = wait;
        tutorialTxt.text = "WASD - Move\r\nI - Inventory\r\nE - Talk\r\nEsc - Pause";
        active = true;
    }

    void Update()
    {
        if (active)
        {
            tutorialTxt.gameObject.SetActive(true);
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                tutorialTxt.gameObject.SetActive(false);
                timer = wait;
                active = false;
            }
        }
    }
}
