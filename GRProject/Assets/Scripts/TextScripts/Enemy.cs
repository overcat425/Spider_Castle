using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int health = 100;
    public float enemySpeed;

    public int Health
    {
        get { return health; }
    }
    void Start()
    {
        Debug.Log($"health°ª : {health}");
    }
    void TakeDamage(int value)
    {
        health -= value;
        if (health <= 0)
        {
            Die();
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(20);
            Debug.Log($"health°ª : {health}");
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
    void Die()
    {
        Destroy(this.gameObject);
    }
    public virtual void Move()
    {

    }
}
