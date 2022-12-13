using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossChasing : MonoBehaviour
{
    [SerializeField]    private Animator animator;
    Rigidbody2D enemyBody;
    Transform target;
    private float flipX;
    private HealthGauge healthGauge;

    [Header("속도")]
    [SerializeField] private float speed;
    [SerializeField] private float slowSpeed;
    [SerializeField] private float moveSpeed;

    [Header("타격 거리")]
    [SerializeField]
    private float hitscanDistance = 5f;

    [SerializeField] private AudioClip bossDashSound;
    private bool canDash;
    private bool isWander;
    private bool getTarget;
    private int wanderDice;
    private int dashDice;
    private Vector2 movingSpot;
    private Vector2 dashSpot;

    private void Awake()
    {
        flipX = -transform.localScale.x;
    }
    void Start()
    {
        animator = GetComponent<Animator>();        // 애니메이터 컴포 받기
        enemyBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        slowSpeed = speed * 0.2f;
        moveSpeed = speed;
        getTarget = false;
        Invoke("Wandering", 5);
        InvokeRepeating("SetTargetFalse", 0, 10);
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < 1000)   // 범위내로 들어오면
        {
            isWander = false;       // 배회모드 OFF
            MobFlip();
            if (canDash == false)       // 돌진 불가능할 때는
            {
                animator.SetBool("BossRun", true);
                StartCoroutine("TargetChasing");        // 일반 추적
            }else if (canDash == true)  // 돌진 가능할 때는
            {
                animator.SetBool("BossRun", true);
                StartCoroutine("Dashing");          // 돌진
            }
        }else{ isWander = true; }   // 범위 바깥이면 배회모드 ON
        if (isWander == true)       // 배회모드 ON일 때
        {
            if(wanderDice == 0)
            {
                animator.SetBool("BossRun", false);        //배회Dice가 0이면 정지
            }else if(wanderDice >= 1)   // 1 이상이면 무작위 movingSpot으로 이동
            {
                animator.SetBool("BossRun", true);
                transform.position = Vector2.MoveTowards(transform.position, movingSpot, moveSpeed * Time.deltaTime);
            }
        }
    }
    public IEnumerator TargetChasing()                    // 플레이어 추적
    {
        if (Vector2.Distance(transform.position, target.position) > hitscanDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        yield return null;
    }
    public void Wandering()                         // 배회
    {
        wanderDice = Random.Range(0, 3);        // 0,1,2 중 랜덤값
        animator.SetBool("BossRun", true);
        movingSpot = new Vector2(Random.Range(-3900, 3900), Random.Range(-2900, 2900));     // 맵의 랜덤좌표
        Invoke("Wandering", 5);     //반복
    }
    public IEnumerator Dashing()            // 돌진
    {
        if (getTarget == false)             // target정보가 없는 상태면
        {
            GetTarget();                // target정보를 받아옴
            SoundManager.SoundEffect.SoundPlay("bossDashSound", bossDashSound);
        }
        else if(getTarget == true)          // target정보를 받아오면
        {                                           // dashSpot으로 돌진
            transform.position = Vector2.MoveTowards(transform.position, dashSpot, moveSpeed * Time.deltaTime* 3);
            if((transform.position.x == dashSpot.x)&&(transform.position.y == dashSpot.y))
            {
                yield return new WaitForSeconds(0.2f);
                animator.SetBool("BossRun", false);
            }
        }
        yield return new WaitForSeconds(3f);
        animator.SetBool("BossRun", true);
        CanDashfalse();
    }
    public void GetTarget()                 // 플레이어 위치로 돌격
    {              // 보스 위치와 플레이어 위치를 비교해 조금 더 돌진하도록 설정
        dashSpot = new Vector2(target.position.x - ((transform.position.x - target.position.x)/2), target.position.y - ((transform.position.y - target.position.y) / 2));
        getTarget = true;
    }
    private void SetTargetFalse()
    {
        canDash = true;
        getTarget = false;
    }
    private void CanDashfalse()
    {
        canDash = false;
    }
    private void OnTriggerStay2D(Collider2D collision)          // 피격 시
    {
        if (collision.CompareTag("Player"))
        {
            HealthGauge.health -= 2f;
        }
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
}