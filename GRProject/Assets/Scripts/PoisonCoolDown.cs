using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoisonCoolDown : MonoBehaviour
{
    private Image poisonBar;                        // �⺻���� ��Ÿ�ӹ� �̹���
    private float attackCoolDown = 100f;      // �⺻���� ��ٿ������
    public static float coolDown;                   // ��Ÿ��
    private int poisonLabLv;                        // �� ����
    private int poisonAttackRate;                   // �� ����

    private void Awake()
    {
        poisonLabLv = SaveManager.skillLabLvStat[3];
        if (poisonLabLv >= 3)
        {
            poisonLabLv = 2;
        }
        poisonAttackRate = 3 - poisonLabLv;
    }
    private void Start()
    {
        poisonBar = GetComponent<Image>();      // �̹��� ������Ʈ           
        StartCoroutine("CoolDown");
    }

    private void Update()
    {
        poisonBar.fillAmount -= 1.0f / poisonAttackRate * Time.deltaTime;// 2�ʵ��� ��Ÿ�Ӱ����� ����
    }
    private IEnumerator CoolDown()             // 2�ʸ��� �ݺ��ϴ� �ڷ�ƾ�޼ҵ�
    {
        poisonBar.fillAmount = attackCoolDown;    // ��Ÿ�Ӱ����� Ǯ����
        yield return new WaitForSeconds(poisonAttackRate);
        StartCoroutine("CoolDown");
    }
}
