using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class cookie_1 : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] int healAmount;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            if(HealthGauge.maxHealth > HealthGauge.health)
            {
                if(HealthGauge.health <= HealthGauge.maxHealth - healAmount*10f)
                {
                    HealthGauge.health += healAmount * 10f;
                }else if(HealthGauge.health > HealthGauge.maxHealth - healAmount * 10f)
                {
                    HealthGauge.health = HealthGauge.maxHealth;
                }
                Destroy (this.gameObject);
            }
        }
    }
}