using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthGauge : MonoBehaviour
{
    private Image healthBar;                        // ü�¹� �̹���
    public static float maxHealth;                 // �ִ�ü�� 100
    public static float health;
    public static bool canAutoSave;

    public float CurrentHP => health; // �ܺο����� ���� �ֵ��� property ����
    public float MaxHP => maxHealth;
    private void Awake()
    {
        maxHealth = 100 + (SaveManager.skill0LvInstance*20);
    }
    private void Start()
    {
        canAutoSave = true;
        healthBar = GetComponent<Image>();          // ü�¹� ����
        health = maxHealth;                                 // �ʱ� ü�� = �ִ�ü��
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
