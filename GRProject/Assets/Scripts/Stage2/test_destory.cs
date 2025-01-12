using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_destory : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthGauge.health += 20f;
            Destroy(this.gameObject);
        }
    }
}