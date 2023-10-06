using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] TMP_Text[] itemsText;
    [SerializeField] public int maxItems = 8;

    [Header("Stats")]
    public List<string> items = new List<string>();
    public int numItems = 0;

    void Start()
    {
        foreach (var item in itemsText) item.text = "";
    }

    void Update()
    {
        
    }

    public void Clear()
    {
        foreach (var item in itemsText) item.text = "";
    }

    public void Display()
    {
        for(int i = 0; i < numItems; i++)
        {
            itemsText[i].text = items[i];
        }
    }

    public void Use(TMP_Text item)
    {
        items.Remove(item.text);
        item.text = "";
        if(numItems > 0) numItems--;
    }
}
