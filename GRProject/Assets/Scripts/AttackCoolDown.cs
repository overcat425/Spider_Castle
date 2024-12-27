using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackCoolDown : MonoBehaviour
{
    private Image attackBar;                        // �⺻���� ��Ÿ�ӹ� �̹���
    private float attackCoolDown = 100f;      // �⺻���� ��ٿ������
    public static float coolDown;                   // �⺻���� ��Ÿ��
    private void Start()
    {
        attackBar = GetComponent<Image>();      // �̹��� ������Ʈ           
        StartCoroutine("CoolDown");
    }

    private void Update()
    {
        attackBar.fillAmount -= 1.0f/2*Time.deltaTime;// 2�ʵ��� ��Ÿ�Ӱ����� ����
    }
    private IEnumerator CoolDown()             // 2�ʸ��� �ݺ��ϴ� �ڷ�ƾ�޼ҵ�
    {
        attackBar.fillAmount = attackCoolDown;    // ��Ÿ�Ӱ����� Ǯ����
        yield return new WaitForSeconds(2);
        StartCoroutine("CoolDown");
    }
}
