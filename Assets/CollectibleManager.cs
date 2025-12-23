using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance; //Singleton

    private List<Collectible> all = new List<Collectible>(); //lista identyfikatorów itemów
    private HashSet<string> collectedIds = new HashSet<string>(); //lista zebranych identyfikatorów itemów

    //inicjalizacja Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    //zapisywanie itema do wszystkich w Managerze
    public void Register(Collectible item)
    {
        if (!all.Contains(item))
            all.Add(item);
    }

    // dodajemy item do zebrancyh
    public void MarkCollected(string id)
    {
        collectedIds.Add(id);
    }

    //zwracamy tablicę Id
    public string[] GetCollectedIds()
    {
        string[] ids = new string[collectedIds.Count];
        int index = 0;

        foreach (string id in collectedIds)
        {
            ids[index] = id;
            index++;
        }
        return ids;
    }

    // zwraca itemy podczas Load jak przed Save
    public void ApplyCollectedIds(string[] idsFromSave)
    {
        collectedIds.Clear();
        if (idsFromSave != null)
        {
            foreach (var id in idsFromSave)
                collectedIds.Add(id);
        }

        foreach (Collectible collectible in all)
        {
            if (collectible == null) continue;
            bool wasCollected = collectedIds.Contains(collectible.Id);
            collectible.gameObject.SetActive(!wasCollected);
        }
    }
}
