using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private Image bossHealthBar;                        // ü�¹� �̹���
    public float bossMaxHealth;                 // �ִ�ü�� 100
    public float bossHealth;

    public float CurrentHP => bossHealth; // �ܺο����� ���� �ֵ��� property ����
    public float MaxHP => bossMaxHealth;
    // Start is called before the first frame update
    private void Awake()
    {
        bossHealth = bossMaxHealth;                                 // �ʱ� ü�� = �ִ�ü��
    }
    void Start()
    {
        bossHealthBar = GetComponent<Image>();          // ü�¹� ����
    }

    // Update is called once per frame
    void Update()
    {
        bossHealth = BossStatus.bossHealth;
        bossHealthBar.fillAmount = bossHealth / bossMaxHealth;
    }
}