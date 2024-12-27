using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyChasing : MonoBehaviour
{
    Rigidbody2D enemyBody;
    Transform target;
    private float flipX;                            // 몹 플레이어 바라보기 플립
    private HealthGauge healthGauge;

    [Header("속도")]
    [SerializeField]    private float speed;                // 기준 이동속도
    [SerializeField]    private float slowSpeed;        // 감소된 이동속도 수치
    [SerializeField]    private float moveSpeed;        // 평시 이동속도 수치

    [Header("타격 거리")]
    [SerializeField]
    private float hitscanDistance = 1f;

    private bool canSlow = true;            // 몹 이동속도 감소 가능여부 초기화
    private bool playerInvincible;            // 플레이어 피격 대기시간 부여

    private void Awake()
    {
        flipX = -transform.localScale.x;
    }
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();  // 타겟을 플레이어로 초기화
        slowSpeed = speed * 0.2f;           // 감소된 이동속도는 평시의 0.2배로 설정
        moveSpeed = speed;
    }
    void Update()
    {
        TargetChasing();
        MobFlip();
        playerInvincible = PlayerController.invincible;
    }
    public void TargetChasing()                    // 플레이어 추적
    {
        if(Vector2.Distance(transform.position, target.position) > hitscanDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)          // 플레이어가 피격 시
    {
        if (collision.CompareTag("Player"))
        {
            if(playerInvincible == false)
            {
                HealthGauge.health -= 0.5f;
            }
        }
        if (collision.CompareTag("Web")||collision.CompareTag("WebLv4"))    // 거미줄 트랩에 피격 시
        {
            StartCoroutine("Slow");         // 이동속도 감소 코루틴메소드 실행
        }
    }
    private void OnTriggerExit2D(Collider2D collision)          // 피격 상태 X
    {
    }
    public void MobFlip()
    {
        Vector3 CharacterFlip = Vector3.zero;
        if (target.position.x > transform.position.x)       // 몹이 플레이어 캐릭터쪽을 바라봄
        {
            CharacterFlip = Vector3.left;
            transform.localScale = new Vector3(flipX, transform.localScale.y, 1);
        }
        else if (target.position.x < transform.position.x)
        {
            CharacterFlip = Vector3.right;
            transform.localScale = new Vector3(-flipX, transform.localScale.y, 1);
        }
    }
    private IEnumerator Slow()          // 몹 이동속도 감소 코루틴메소드
    {
        if (canSlow)            // 이동속도 감소 가능상태면 이동속도 감소
        {
            canSlow = false;
            moveSpeed = slowSpeed;
            yield return new WaitForSeconds(4f);        // 4초 지속
            moveSpeed = speed;
        }
        canSlow = true;
    }
}