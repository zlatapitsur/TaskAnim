using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private string id; //identyfikator kadego itema
    public string Id => id; //dla odczuty
    private bool playerInRange = false;

    private void Start()
    {
        if (string.IsNullOrEmpty(id))
            id = gameObject.name;

        if (CollectibleManager.Instance != null)
            CollectibleManager.Instance.Register(this);
        else
            Debug.LogError("CollectibleManager.Instance is NULL!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // warunek czy gracz jest u triggera przedmiotu
        if (collision.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // warunek czy gracz nie jest w zasięgu przedmiotu
        if (collision.CompareTag("Player"))
            playerInRange = false;
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            //zwiększamy licznik
            if (ItemCounter.Instance != null)
                ItemCounter.Instance.AddItem();
            else
                Debug.LogError("ItemCounter.Instance is NULL!");

            // zapis e zebraliśmy predmiot
            if (CollectibleManager.Instance != null)
                CollectibleManager.Instance.MarkCollected(id);
            else
                Debug.LogError("CollectibleManager.Instance is NULL!");

            //wyłączamy byśmy mogli zwrócić item po Load
            gameObject.SetActive(false);
        }
    }
}
