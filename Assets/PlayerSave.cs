using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSave : MonoBehaviour
{
    //public int health = 100;
    //public int coins = 0;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private ItemCounter itemCounter;


    //[SerializeField] private Text coinsText;

    //private void Start()
    //{
    //    UpdateText();
    //}

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

    //public void AddCoin()
    //{
    //    coins++;
    //    UpdateText();
    //}

    //private void UpdateText()
    //{
    //    if (coinsText != null)
    //        coinsText.text = "Items: " + coins;
    //}

    public void Save()
    {
        SaveData data = new SaveData();
        //data.health = health;
        //data.coins = coins;
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

        //health = data.health;
        //coins = data.coins;
        playerHealth.SetHealth(data.health);
        ItemCounter.Instance.SetItems(data.coins);
        CollectibleManager.Instance.ApplyCollectedIds(data.collectedIds);
        transform.position = new Vector3(
            data.position[0],
            data.position[1],
            data.position[2]
        );

        //UpdateText();
        Debug.Log("Game Loaded");
    }
}
