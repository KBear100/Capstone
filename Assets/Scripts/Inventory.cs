using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] TMP_Text itemsText;

    public List<string> items = new List<string>();
    public int numItems = 0;

    void Start()
    {
        itemsText.text = "";
    }

    void Update()
    {
        
    }

    public void Clear()
    {
        itemsText.text = "";
    }

    public void Display()
    {
        foreach(var item in items)
        {
            itemsText.text += item + " ";
        }
    }

    public void Use()
    {

    }
}
