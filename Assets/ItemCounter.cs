using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class ItemCounter : MonoBehaviour
{
    public static ItemCounter Instance;

    [SerializeField] private TextMeshProUGUI itemsText;

    private int items = 0;

    private void Awake()
    {
        Instance = this;
        UpdateText();
    }

    public void AddItem()
    {
        items++;
        UpdateText();
    }

    public int GetItems()
    {
        return items;
    }
    
    public void SetItems(int newItemsValue)
    {
        items = newItemsValue;
        UpdateText();
    }

    private void UpdateText()
    {
        if (itemsText != null)
        {
            itemsText.text = "Items: " + items;
        }
    }
}
