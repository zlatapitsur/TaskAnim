using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCounter : MonoBehaviour
{
    public static ItemCounter Instance; //singlton

    [SerializeField] private TextMeshProUGUI itemsText;

    private int items = 0; //zmienna dla licznoci itemów

    //inicjalizacja
    private void Awake()
    {
        Instance = this;
        UpdateText();
    }

    //dodanie itemów
    public void AddItem()
    {
        items++;
        UpdateText();
    }

    //getter
    public int GetItems()
    {
        return items;
    }
    
    //setter
    public void SetItems(int newItemsValue)
    {
        items = newItemsValue;
        UpdateText();
    }

    //odnowienie tekstu na UI
    private void UpdateText()
    {
        if (itemsText != null)
        {
            itemsText.text = "Items: " + items; //licznik
        }
    }
}
