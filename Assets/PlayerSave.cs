using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSave : MonoBehaviour
{

    [SerializeField] private PlayerHealth playerHealth; //referencja do zdrowia
    [SerializeField] private ItemCounter itemCounter; //referencja do licznika

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void Save()
    {
        // zeby gracz nie mógł siebie zapisać kiedy zmarł
        if (playerHealth.isDead)
        {       
            Debug.Log("You can't Save yourself. YOU ARE DEAD");
            return;
        }

        // zapis ostatniego stanu
        SaveData data = new SaveData();
        data.health = playerHealth.GetHealth();
        data.coins = ItemCounter.Instance.GetItems();
        data.collectedIds = CollectibleManager.Instance.GetCollectedIds();

        data.position = new float[3]
        {
            transform.position.x,
            transform.position.y,
            transform.position.z
        };

        SaveSystem.Save(data);
        Debug.Log("Game Saved");
    }

    public void Load()
    {
        SaveData data = SaveSystem.Load();
        if (data == null) return;

        // odczyt ostatniego zapisanego stanu
        playerHealth.SetHealth(data.health);
        ItemCounter.Instance.SetItems(data.coins);
        CollectibleManager.Instance.ApplyCollectedIds(data.collectedIds);
        transform.position = new Vector3(
            data.position[0],
            data.position[1],
            data.position[2]
        );


        var pc = GetComponent<playerController>();
        // zeby playerController nie był null
        if (pc != null)
            pc.ResetAfterLoad();

        Debug.Log("Game Loaded");
    }
}
