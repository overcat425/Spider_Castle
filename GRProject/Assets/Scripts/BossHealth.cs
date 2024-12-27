using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private Image bossHealthBar;                        // 체력바 이미지
    public float bossMaxHealth;                 // 보스 최대체력 (100)
    public float bossHealth;                        // 보스 현재 체력
    [SerializeField] public Text bossHealthText;    // 보스 체력 텍스트출력

    public float CurrentHP => bossHealth; // 외부에서도 볼수 있도록 property 정의
    public float MaxHP => bossMaxHealth;
    private void Awake()
    {
        bossHealth = bossMaxHealth;                                 // 초기 체력 = 최대체력
    }
    void Start()
    {
        bossHealthBar = GetComponent<Image>();          // 체력바 구현
    }

    void Update()
    {
        bossHealth = BossStatus.bossHealth;
        bossHealthBar.fillAmount = bossHealth / bossMaxHealth;
        bossHealthText.text = bossHealth.ToString();
    }
}
