using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    public float damage = 40;

    //wywoływane kiedy gracz wchodzi do triggera
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // warynek czy gracz wchodzi w trigger
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        // sprawdzenie czy gracz ma PlayerHealth
        PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
        if (health == null)
        {
            return;
        }

        health.TakeDamage(damage);
      
    }

}
