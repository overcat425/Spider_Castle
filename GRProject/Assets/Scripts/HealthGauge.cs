using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthGauge : MonoBehaviour
{
    private Image healthBar;                        // 체력바 이미지
    private float maxHealth = 100f;                 // 최대체력 100
    public static float health;
    public static bool canAutoSave;

    public float CurrentHP => health; // 외부에서도 볼수 있도록 property 정의
    public float MaxHP => maxHealth;

    private void Start()
    {
        canAutoSave = true;
        healthBar = GetComponent<Image>();          // 체력바 구현
        health = maxHealth;                                 // 초기 체력 = 최대체력

    }
    private void Update()
    {
        healthBar.fillAmount = health / maxHealth;

        if(health <= 0f)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("GameOver");
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Destroy(collision.gameObject);
    //}
}
