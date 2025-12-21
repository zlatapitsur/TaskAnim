using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float[] position; // pozycja gracza
    public float health; // zdrowie gracza
    public int coins; // liczba monet
    public string[] collectedIds; // lista id wziątych rzeczy
}