using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    private GameObject enemy;
    private Image healthBar;                  // �� ü�¹�(�ʿ�� �߰�)
    private float maxHealth = 10f;
    public static float enemyHealth;

    private EnemyMemoryPool enemyMemoryPool;
    private void Start()
    {
        healthBar = GetComponent<Image>();
        enemyHealth = maxHealth;
    }
    private void Update()
    {
        //healthBar.fillAmount = enemyHealth / maxHealth;
        //if (enemyHealth <= 0f)
        //{
            //Destroy(gameObject);
            //enemyMemoryPool.InactivateEnemy(gameObject);
        //}
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
