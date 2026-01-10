using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health = 100;

    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void TakeDamage(int amount)
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(amount);
            return;
        }

        health -= amount;
        health = Mathf.Max(health, 0);
        Debug.Log("Player HP: " + health);
        if (health <= 0) Debug.Log("Player died");
    }
}
