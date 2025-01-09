using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthGauge : MonoBehaviour
{
    private Image healthBar;                        // 체력바 이미지
    public static float maxHealth;                 // 최대체력 100
    public static float health;                     // 현재 체력
    public static bool canAutoSave;             // 자동 저장 가능 여부
    public static bool isDie;                       // 플레이어 사망 여부

    public float CurrentHP => health; // 외부에서도 볼수 있도록 property 정의
    public float MaxHP => maxHealth;
    private void Awake()
    {
        maxHealth = 100 + (SaveManager.skillLvStat[0] * 20);  // 특성에 따른 체력설정 (체력 강화)
    }
    private void Start()
    {
        isDie = false;
        canAutoSave = true;
        healthBar = GetComponent<Image>();          // 체력바 구현
        health = maxHealth;                                 // 초기 체력 = 최대체력
    }
    private void Update()
    {
        healthBar.fillAmount = health / maxHealth;

        StartCoroutine("PlayerDie");
    }
    private IEnumerator PlayerDie()              // 플레이어 사망시 코루틴메소드
    {
        if(health <= 0f)
        {
            isDie = true;
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("StartGame");
        }
    }
}
