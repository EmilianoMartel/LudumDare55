using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    [ContextMenu("Get 20 point damage")]
    private void Get20Point()
    {
        ReceiveDagage(20f);
        currentHealth -= 20;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
    public void ReceiveDagage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth/maxHealth; 
    }
}
