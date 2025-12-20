using System.IO;
using UnityEngine;

public class SaveSystem
{
    private static string fileName = "savegame.json";

    // Zwraca ?cie?k? do pliku zapisu gry
    private static string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    // Zapisuje dane gry do pliku
    public static void Save(SaveData data)
    {
        // Konwertuje dane do formatu JSON
        string json = JsonUtility.ToJson(data);
        // Zapisuje JSON do pliku
        File.WriteAllText(GetFilePath(), json);
        Debug.Log("Game saved to " + GetFilePath());
    }

    // Wczytuje dane gry z pliku
    public static SaveData Load()
    {
        string path = GetFilePath();
        // Jeśli plik nie istnieje, zwraca null
        if (!File.Exists(path))
        {
            Debug.LogWarning("Save file not found in " + path);
            return null;
        }
        // Odczytuje JSON z pliku
        string json = File.ReadAllText(path);
        // Konwertuje JSON na obiekt SaveData
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        Debug.Log("Game loaded from " + path);
        return data;

    }
}