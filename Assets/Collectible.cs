using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private string id;
    public string Id => id;
    private bool playerInRange = false;

    private void Awake()
    {
        CollectibleManager.Instance.Register(this);

        if (string.IsNullOrEmpty(id))
            id = gameObject.name; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ItemCounter.Instance.AddItem();
            CollectibleManager.Instance.MarkCollected(id);

            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
