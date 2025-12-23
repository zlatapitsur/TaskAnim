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

    // odnowienie zdrowia
    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + Mathf.RoundToInt(health).ToString();
        }
    }

    // getter
    public float GetHealth(){
        return health;
    }

    // setter
    public void SetHealth(float newHealthValue){
        health = Mathf.Clamp(newHealthValue, 0f, maxHealth);

        if (health > 0f)
        {
            isDead = false;
            if (anim != null)
            {
                anim.ResetTrigger("IsDead");
                anim.Play("Idle", 0, 0f);
            }
        }

        UpdateHealthUI();
    }
}
