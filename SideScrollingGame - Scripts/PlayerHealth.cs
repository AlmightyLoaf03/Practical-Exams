using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public HealthUI healthUI;

    public static event Action OnPlayerDied;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
        healthUI.SetMaxHearts(maxHealth);

        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Trap trap = collision.GetComponent<Trap>(); 
        if (trap && trap.damage > 0)
        {
            TakeDamage(trap.damage);
            SoundEffectManager.Play("PlayerHit");

        }
    }

    void ResetHealth()
    {
        if (this == null) return; // Prevent issues if PlayerHealth is destroyed
        currentHealth = maxHealth;
        Debug.Log("Player health reset to max: " + maxHealth);
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);

        //Flash red when damaged
        StartCoroutine(FlashRed());

        if(currentHealth <= 0)
        {
            OnPlayerDied.Invoke();
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

}
