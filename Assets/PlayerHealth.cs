using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health = 100;
    public Animator anim;

    public bool isDead = false;

    [Header("UI")]
    public TextMeshProUGUI healthText;

    public Canvas canvas;

    void Start()
    {
        anim = GetComponent<Animator>();
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        // Віднімаємо HP
        health -= damage;

        // Обмеження здоров'я
        if (health < 0) health = 0;
        if (health > maxHealth) health = maxHealth;

        UpdateHealthUI();

        // Смерть
        if (health == 0)
        {
            isDead = true;
            anim.SetTrigger("IsDead");
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + Mathf.RoundToInt(health).ToString();
        }
    }
}
