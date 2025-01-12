using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStatus : MonoBehaviour
{
    EnemySpawner enemySpawner;
    private SpriteRenderer sprite;
    private Color color;
    public GameObject hudDamageText;        // 피격시 데미지텍스트
    public GameObject gene;                     // 보스 사망 시 드랍재화

    public Transform hudPos;                    // 보스정보 HUD 위치
    Transform target;
    [SerializeField]
    private float maxHealth;                        // 보스 최대 체력
    [SerializeField]
    public static float bossHealth;
    public AudioClip enemyDestroySound;

    [SerializeField] public int baseDamage;             // 기본공격 데미지
    [SerializeField] public int maceDamage;             // 철퇴공격 데미지
    [SerializeField] public int poisonDamage;           // 독 데미지
    private int poisonLabLv;                            // 독 스킬레벨
    private int poisoningTime;                          // 중독 상태이상 지속시간

    private void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        color = sprite.color;
        poisonLabLv = SaveManager.instance.skillLabLvStat[3];
    }
    private void Start()
    {
        bossHealth = maxHealth;                 // 보스 최대체력 초기화
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (poisonLabLv <= 2)                   // 독 지속시간 설정 (특성)
        {
            poisoningTime = 3;
        }
        else if (poisonLabLv <= 4)
        {
            poisoningTime = 3 + (poisonLabLv - 2);
        }
    }
    private void Update()
    {
        baseDamage = SaveManager.skillLvStat[1] * 50;             // 기본공격 계수
        maceDamage = SaveManager.skillLvStat[2] * 10;             // 철퇴공격 계수
        poisonDamage = SaveManager.skillLvStat[5] * 2;            // 독 공격 계수
        if (bossHealth <= 0)
        {                                                               // 보스 사망 시
            DestroyEnemy();                                       // 맵전체 몹들 초기화 후
            EnemySpawner.count_instance.EnemyKilledCount++;   // 전체 킬수에 +1 (보스)
            SoundManager.SoundEffect.SoundPlay("EnemyDestroySound", enemyDestroySound);     // 보스 사망사운드
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Eraser"))             // 오브젝트 지우개
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Elec"))               // 맵 전기 오브젝트 피격
        {
            bossHealth -= 10f;
        }
        if (collision.CompareTag("Poison"))             // 독 공격 피격 시
        {
            StartCoroutine("PoisonDamage");         // 독데미지 코루틴
        }
    }
    public void Skill1Damage()
    {
        bossHealth -= baseDamage;
        DamageText(baseDamage);
    }
    public void Skill2Damage()
    {
        bossHealth -= maceDamage;
        DamageText(maceDamage);
        EnemyDamaged();
    }
    public IEnumerator PoisonDamage()         // 중독 코루틴메소드
    {
        for (int i = 0; i < poisoningTime; i++)
        {
            sprite.color = new Color(1, 0, 1, 1);
            bossHealth -= poisonDamage;
            DamageText(poisonDamage);
            yield return new WaitForSeconds(1f);            // 1초마다 데미지
        }
        sprite.color = color;
    }
    public void DestroyEnemy()              // 보스 사망시 재화드랍
    {
        GeneDrop();
        Destroy(gameObject);
    }
    public void DamageText(int damageText)              // 몹 피격시 데미지 출력
    {
        GameObject hudText = Instantiate(hudDamageText, EnemySpawner.instance.texts);
        hudText.transform.position = hudPos.position;
        hudText.GetComponent<DamageText>().damage = damageText;
    }
    public void GeneDrop()              // 재화드랍 메소드
    {
        GameObject Gene = Instantiate(gene);
        Gene.transform.position = hudPos.position;
    }
    public void EnemyDamaged()              // 보스 피격 대기시간 처리용 1
    {
        gameObject.tag = "EnemyDamaged";
        Invoke("EnemyCanDamage", 0.5f);
    }
    public void EnemyCanDamage()        // 보스 피격 대기시간 처리용 2
    {
        gameObject.tag = "Boss";
    }
}