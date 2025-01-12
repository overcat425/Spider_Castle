using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossChasing : MonoBehaviour    // ������ �÷��̾� ���� ��ũ��Ʈ
{
    BossStatus bossStatus;
    [SerializeField]    private Animator animator;
    Rigidbody2D enemyBody;
    Transform target;
    private float flipX;
    private HealthGauge healthGauge;                // ���� �� ������

    [Header("�ӵ�")]
    [SerializeField] private float speed;                 // ��ȸ�� ���� �ӵ�
    [SerializeField] private float slowSpeed;
    [SerializeField] private float moveSpeed;          // ��ȸ�� ���� �ӵ�

    [Header("Ÿ�� �Ÿ�")]
    [SerializeField]
    private float hitscanDistance = 5f;

    [SerializeField] private AudioClip bossDashSound;
    private bool playerInvincible;                  // �÷��̾ ���������ΰ�
    private bool canDash;                           // �뽬(��ų)�� �� �� �ִ»���
    private bool isWander;                          // ��ȸ���� ����
    private bool getTarget;                         // ǥ�� �ν� ����
    private int wanderDice;                         
    private int dashDice;
    private Vector2 movingSpot;                 // ������ ��ȸ���� ��ǥ(����)
    private Vector2 dashSpot;                    // ������ �÷��̾� ����(�뽬)��ǥ

    private void Awake()
    {
        flipX = -transform.localScale.x;            // �÷��̾� ��ġ�� ���� �� �ٶ󺸴� ���� �ø�
    }
    void Start()
    {
        animator = GetComponent<Animator>();        // �ִϸ����� ���� �ޱ�
        enemyBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        slowSpeed = speed * 0.2f;
        moveSpeed = speed;
        getTarget = false;
        Invoke("Wandering", 5);                                 // ��ȸ �޼ҵ� 5�ʸ���
        InvokeRepeating("SetTargetFalse", 0, 10);           // ������ġ ���� ���� 
    }
    void Update()
    {
        playerInvincible = PlayerController.invincible;
        if (Vector2.Distance(transform.position, target.position) < 1000)   // �������� ������
        {
            isWander = false;       // ��ȸ��� OFF
            MobFlip();                  // �÷��̾ �ٶ󺸵��� �ø�
            if (canDash == false)       // ���� �Ұ����� ����
            {
                animator.SetBool("BossRun", true);
                StartCoroutine("TargetChasing");        // �Ϲ� ����
            }else if (canDash == true)  // ���� ������ ����
            {
                animator.SetBool("BossRun", true);
                StartCoroutine("Dashing");          // ����
            }
        }else{ isWander = true; }   // ���� �ٱ��̸� ��ȸ��� ON
        if (isWander == true)       // ��ȸ��� ON�� ��
        {
            if(wanderDice == 0)
            {
                animator.SetBool("BossRun", false);        //��ȸDice�� 0�̸� ����
            }else if(wanderDice >= 1)   // 1 �̻��̸� ������ movingSpot���� �̵�
            {
                animator.SetBool("BossRun", true);
                transform.position = Vector2.MoveTowards(transform.position, movingSpot, moveSpeed * Time.deltaTime);
            }
        }
    }
    public IEnumerator TargetChasing()                    // �÷��̾� ����
    {
        if (Vector2.Distance(transform.position, target.position) > hitscanDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        yield return null;
    }
    public void Wandering()                         // ��ȸ
    {
        wanderDice = Random.Range(0, 3);        // 0,1,2 �� ������
        animator.SetBool("BossRun", true);
        movingSpot = new Vector2(Random.Range(-3900, 3900), Random.Range(-2900, 2900));     // ���� ������ǥ
        Invoke("Wandering", 5);     //�ݺ�
    }
    public IEnumerator Dashing()            // ����
    {
        if (getTarget == false)             // target������ ���� ���¸�
        {
            GetTarget();                // target������ �޾ƿ�
            SoundManager.SoundEffect.SoundPlay("bossDashSound", bossDashSound);
        }
        else if(getTarget == true)          // target������ �޾ƿ���
        {                                           // dashSpot���� ����
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
    public void GetTarget()                 // �÷��̾� ��ġ�� ����
    {              // ���� ��ġ�� �÷��̾� ��ġ�� ���� ���� �� �����ϵ��� ����
        dashSpot = new Vector2(target.position.x - ((transform.position.x - target.position.x)/2), target.position.y - ((transform.position.y - target.position.y) / 2));
        getTarget = true;
    }
    private void SetTargetFalse()           // ���� Ÿ������ ������ ���� �ʱ�ȭ
    {
        canDash = true;
        getTarget = false;
    }
    private void CanDashfalse()
    {
        canDash = false;
    }
    private void OnTriggerStay2D(Collider2D collision)          // �ǰ� ��
    {
        if (collision.CompareTag("Player"))
        {
            if (playerInvincible == false)
            {
                HealthGauge.health -= 0.5f;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mace")){
            bossStatus.EnemyDamaged();
        }
    }
    public void MobFlip()
    {
        Vector3 CharacterFlip = Vector3.zero;
        if (target.position.x > transform.position.x)       // ���� �÷��̾� ĳ�������� �ٶ�
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