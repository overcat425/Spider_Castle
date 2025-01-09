using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoisonCoolDown : MonoBehaviour
{
    private Image poisonBar;                        // 기본공격 쿨타임바 이미지
    private float attackCoolDown = 100f;      // 기본공격 쿨다운게이지
    public static float coolDown;                   // 쿨타임
    private int poisonLabLv;                        // 독 레벨
    private int poisonAttackRate;                   // 독 공속

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
        poisonBar = GetComponent<Image>();      // 이미지 컴포넌트           
        StartCoroutine("CoolDown");
    }

    private void Update()
    {
        poisonBar.fillAmount -= 1.0f / poisonAttackRate * Time.deltaTime;// 2초동안 쿨타임게이지 감소
    }
    private IEnumerator CoolDown()             // 2초마다 반복하는 코루틴메소드
    {
        poisonBar.fillAmount = attackCoolDown;    // 쿨타임게이지 풀충전
        yield return new WaitForSeconds(poisonAttackRate);
        StartCoroutine("CoolDown");
    }
}
