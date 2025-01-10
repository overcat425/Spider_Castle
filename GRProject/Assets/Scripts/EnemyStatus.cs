using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyStatus : MonoBehaviour
{
    private SpriteRenderer sprite;                  // 적 스프라이트
    private Color color;
    public GameObject hudDamageText;        // 피격시 데미지텍스트
    public GameObject coins;                        // 코인
    public Transform hudPos;
    Transform target;                                   // 추적 타겟정보
    private float moveSpeed = 7.5f;
    private Image healthBar;                  // 적 체력바(필요시 추가)
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    public float enemyHealth;
    public AudioClip enemyDestroySound;

    [SerializeField]    private int baseDamage;         // 기본공격 데미지
    [SerializeField]    private int maceDamage;         // 철퇴공격 데미지
    [SerializeField]    private int poisonDamage;       // 독 공격 데미지
    private int poisonLabLv;                                // 독 레벨
    private int poisoningTime;                              // 중독 상태이상 지속시간

    private void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        color = sprite.color;
        poisonLabLv = SaveManager.instance.skillLabLvStat[3];
    }
    private void Start()
    {
        //healthBar = GetComponent<Image>();
        enemyHealth = maxHealth;                // 몹 최대체력 초기화
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();  // 타겟을 플레이어로 설정
        if (poisonLabLv <= 2)                       // 독 지속시간 설정(특성)
        {
            poisoningTime = 3;
        }else if(poisonLabLv <= 4)
        {
            poisoningTime = 3 + (poisonLabLv-2);
        }
    }
    private void Update()
    {
        baseDamage = SaveManager.skillLvStat[1] * 50;
        maceDamage = SaveManager.skillLvStat[2] * 10;
        poisonDamage = SaveManager.skillLvStat[5] * 2;
        if (enemyHealth <= 0)           // 몹 사망시 난이도에 따른 적 스폰풀에 킬수 카운트
        {
            DestroyEnemy();
            if ((SceneManager.GetActiveScene().name == "Stage1") || (SceneManager.GetActiveScene().name == "Stage2") || (SceneManager.GetActiveScene().name == "Stage3"))
            {
                EnemySpawnPool.count_instance.EnemyKilledCount++;
            }else if ((SceneManager.GetActiveScene().name == "Stage1Hard") || (SceneManager.GetActiveScene().name == "Stage2Hard") || (SceneManager.GetActiveScene().name == "Stage3Hard"))
            {
                EnemySpawnPoolHard.count_instance.EnemyKilledCount++;
            }
            SoundManager.SoundEffect.SoundPlay("EnemyDestroySound", enemyDestroySound);
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
            enemyHealth -= 10f;
        }
        if (collision.CompareTag("Poison"))             // 독 공격 피격 시
        {
            StartCoroutine("PoisonDamage");         // 독 데미지 코루틴
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("WebLv4"))         // 거미줄 트랩에 피격 시 체력감소
        {
            enemyHealth -= 0.5f;
        }
    }
    public void Skill1Damage()
    {
        enemyHealth -= baseDamage;
        DamageText(baseDamage);
        StartCoroutine("KnockBack");
    }
    public void Skill2Damage()
    {
        enemyHealth -= maceDamage;
        DamageText(maceDamage);
        EnemyDamaged();
        Invoke("EnemyCanDamage", 0.5f);             // 피격 대기시간
    }
    public IEnumerator PoisonDamage()           // 독 데미지 코루틴메소드
    {
        for (int i = 0; i < poisoningTime; i++)
        {
            sprite.color = new Color(1, 0, 1, 1);
            enemyHealth -= poisonDamage;
            DamageText(poisonDamage);
            yield return new WaitForSeconds(1f);
        }
        sprite.color = color;
    }
    public void DestroyEnemy()              // 잡몹 사망 시 확률로 재화드랍
    {
        if (Random.Range(0, 5) == 0)
        {
            CoinDrop();
        }
        Destroy(gameObject);
    }
    public void DamageText(int damageText)          // 몹 피격시 데미지텍스트 출력
    {
        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = hudPos.position;
        hudText.GetComponent<DamageText>().damage = damageText;
    }
    public void CoinDrop()
    {
        GameObject Coin = Instantiate(coins);
        Coin.transform.position = hudPos.position;
    }
    public IEnumerator KnockBack()              // 몹 피격 시 넉백 코루틴메소드
    {
        float directionX = transform.position.x - target.position.x;
        float directionY = transform.position.y - target.position.y;
        if (directionX < 0) { directionX = 1; }
        else { directionX = -1; }
        if (directionY < 0) { directionY = -1; }
        else { directionY = 1; }
        float kb = 0;
        while (kb < 0.2f)
        {
            if (transform.rotation.y == 0)
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime * directionX * 50f);
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime * directionY * 50f);
            }
            else
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime * directionX * -50f);
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime * directionY * -50f);
            }
            kb += Time.deltaTime;
            yield return null;
        }
    }
    public void EnemyDamaged()
    {
        gameObject.tag = "EnemyDamaged";
    }
    public void EnemyCanDamage()
    {
        gameObject.tag = "Enemy";
    }
}