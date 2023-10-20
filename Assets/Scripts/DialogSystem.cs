using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public DialogObject steelDialog;
    public DialogObject gracyDialog;
    public DialogObject stacyDialog;
    public GameObject textBox;
    public TMP_Text text;

    void Start()
    {
        text.text = "";
    }

    void Update()
    {
        
    }

    public void ShowDialog(string dialog)
    {
        textBox.SetActive(true);
        text.text = dialog;
    }

    public void ExitDialog()
    {
        text.text = "";
        textBox.SetActive(false);
    }
}
