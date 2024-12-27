using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private Image bossHealthBar;                        // ü�¹� �̹���
    public float bossMaxHealth;                 // ���� �ִ�ü�� (100)
    public float bossHealth;                        // ���� ���� ü��
    [SerializeField] public Text bossHealthText;    // ���� ü�� �ؽ�Ʈ���

    public float CurrentHP => bossHealth; // �ܺο����� ���� �ֵ��� property ����
    public float MaxHP => bossMaxHealth;
    private void Awake()
    {
        bossHealth = bossMaxHealth;                                 // �ʱ� ü�� = �ִ�ü��
    }
    void Start()
    {
        bossHealthBar = GetComponent<Image>();          // ü�¹� ����
    }

    void Update()
    {
        bossHealth = BossStatus.bossHealth;
        bossHealthBar.fillAmount = bossHealth / bossMaxHealth;
        bossHealthText.text = bossHealth.ToString();
    }
}
