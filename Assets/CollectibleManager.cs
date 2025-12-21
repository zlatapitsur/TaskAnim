using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    private List<Collectible> all = new List<Collectible>();
    private HashSet<string> collectedIds = new HashSet<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Register(Collectible c)
    {
        if (!all.Contains(c))
            all.Add(c);
    }

    public void MarkCollected(string id)
    {
        collectedIds.Add(id);
    }

    public string[] GetCollectedIds()
    {
        var arr = new string[collectedIds.Count];
        collectedIds.CopyTo(arr);
        return arr;
    }

    public void ApplyCollectedIds(string[] idsFromSave)
    {
        collectedIds.Clear();
        if (idsFromSave != null)
        {
            foreach (var id in idsFromSave)
                collectedIds.Add(id);
        }

        foreach (var c in all)
        {
            if (c == null) continue;
            bool shouldBeDisabled = collectedIds.Contains(c.Id);
            c.gameObject.SetActive(!shouldBeDisabled);
        }
    }
}
