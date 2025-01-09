using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthGauge : MonoBehaviour
{
    private Image healthBar;                        // ü�¹� �̹���
    public static float maxHealth;                 // �ִ�ü�� 100
    public static float health;                     // ���� ü��
    public static bool canAutoSave;             // �ڵ� ���� ���� ����
    public static bool isDie;                       // �÷��̾� ��� ����

    public float CurrentHP => health; // �ܺο����� ���� �ֵ��� property ����
    public float MaxHP => maxHealth;
    private void Awake()
    {
        maxHealth = 100 + (SaveManager.skillLvStat[0] * 20);  // Ư���� ���� ü�¼��� (ü�� ��ȭ)
    }
    private void Start()
    {
        isDie = false;
        canAutoSave = true;
        healthBar = GetComponent<Image>();          // ü�¹� ����
        health = maxHealth;                                 // �ʱ� ü�� = �ִ�ü��
    }
    private void Update()
    {
        healthBar.fillAmount = health / maxHealth;

        StartCoroutine("PlayerDie");
    }
    private IEnumerator PlayerDie()              // �÷��̾� ����� �ڷ�ƾ�޼ҵ�
    {
        if(health <= 0f)
        {
            isDie = true;
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("StartGame");
        }
    }
}
