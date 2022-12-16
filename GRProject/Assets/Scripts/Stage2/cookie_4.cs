using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookie_4 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
     void Update()
    {
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            if (HealthGauge.maxHealth > HealthGauge.health)
            {
                if (HealthGauge.health <= HealthGauge.maxHealth - 10f)
                {
                    HealthGauge.health += 10f;
                }
                else if (HealthGauge.health > HealthGauge.maxHealth - 10f)
                {
                    HealthGauge.health = HealthGauge.maxHealth;
                }
                Destroy(this.gameObject);
            }
        }
    }
}
