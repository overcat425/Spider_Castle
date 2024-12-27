using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackCoolDown : MonoBehaviour
{
    private Image attackBar;                        // 기본공격 쿨타임바 이미지
    private float attackCoolDown = 100f;      // 기본공격 쿨다운게이지
    public static float coolDown;                   // 기본공격 쿨타임
    private void Start()
    {
        attackBar = GetComponent<Image>();      // 이미지 컴포넌트           
        StartCoroutine("CoolDown");
    }

    private void Update()
    {
        attackBar.fillAmount -= 1.0f/2*Time.deltaTime;// 2초동안 쿨타임게이지 감소
    }
    private IEnumerator CoolDown()             // 2초마다 반복하는 코루틴메소드
    {
        attackBar.fillAmount = attackCoolDown;    // 쿨타임게이지 풀충전
        yield return new WaitForSeconds(2);
        StartCoroutine("CoolDown");
    }
}
