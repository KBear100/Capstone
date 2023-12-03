using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    [Header("UI")]
    public DialogObject steelDialog;
    public DialogObject gracyDialog;
    public DialogObject stacyDialog;
    public GameObject textBox;
    public TMP_Text text;
    public TMP_Text nameText;
    [Header("Timer")]
    [SerializeField] public float dialogTimer;

    [HideInInspector] public bool talking = false;
    [HideInInspector] public float timer;
    [HideInInspector] public string personTalking;
    [HideInInspector] public int dialogNum;
    [HideInInspector] public int dialogNumStop;

    void Start()
    {
        text.text = "";
        nameText.text = "";
        timer = dialogTimer;
    }

    void Update()
    {
        if(talking)
        {
            switch (personTalking)
            {
                case "Steel":
                    ShowDialog(steelDialog.dialog[dialogNum], personTalking);
                    break;
                case "Gracy":
                    ShowDialog(gracyDialog.dialog[dialogNum], personTalking);
                    break;
                case "Stacy":
                    ShowDialog(stacyDialog.dialog[dialogNum], personTalking);
                    break;
                case "":
                    Debug.Log("none");
                    break;
            }

            if (Input.GetMouseButtonDown(0))
            {
                dialogNum++;
                timer = dialogTimer;
                if(dialogNum > dialogNumStop)
                {
                    ExitDialog();
                }
            }
            
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                dialogNum++;
                timer = dialogTimer;
                if (dialogNum > dialogNumStop)
                {
                    ExitDialog();
                }
            }
        }
    }

    public void StartSystem(string name, int dialogNum, int dialogNumStop)
    {
        personTalking = name;
        this.dialogNum = dialogNum;
        this.dialogNumStop = dialogNumStop;

        talking = true;
    }

    public void ShowDialog(string dialog, string name)
    {
        textBox.SetActive(true);
        nameText.gameObject.SetActive(true);
        text.text = dialog;
        nameText.text = name;
    }

    public void ShowDialogWithoutName(string dialog)
    {
        textBox.SetActive(true);
        text.text = dialog;

        timer = dialogTimer;
    }

    public void ExitDialog()
    {
        if (personTalking == "Steel") MainManager.destroySilver = true;

        text.text = "";
        textBox.SetActive(false);
        nameText.gameObject.SetActive(false);
        personTalking = "";
        dialogNum = 0;
        dialogNumStop = 0;

        MainManager.pause = false;
        talking = false;
        timer = dialogTimer;
    }
}
