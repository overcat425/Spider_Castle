using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyChasing : MonoBehaviour
{
    Rigidbody2D enemyBody;
    Transform target;
    private float flipX;                            // �� �÷��̾� �ٶ󺸱� �ø�
    private HealthGauge healthGauge;

    [Header("�ӵ�")]
    [SerializeField]    private float speed;                // ���� �̵��ӵ�
    [SerializeField]    private float slowSpeed;        // ���ҵ� �̵��ӵ� ��ġ
    [SerializeField]    private float moveSpeed;        // ��� �̵��ӵ� ��ġ

    [Header("Ÿ�� �Ÿ�")]
    [SerializeField]
    private float hitscanDistance = 1f;

    private bool canSlow = true;            // �� �̵��ӵ� ���� ���ɿ��� �ʱ�ȭ
    private bool playerInvincible;            // �÷��̾� �ǰ� ���ð� �ο�

    private void Awake()
    {
        flipX = -transform.localScale.x;
    }
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();  // Ÿ���� �÷��̾�� �ʱ�ȭ
        slowSpeed = speed * 0.2f;           // ���ҵ� �̵��ӵ��� ����� 0.2��� ����
        moveSpeed = speed;
    }
    void Update()
    {
        TargetChasing();
        MobFlip();
        playerInvincible = PlayerController.invincible;
    }
    public void TargetChasing()                    // �÷��̾� ����
    {
        if(Vector2.Distance(transform.position, target.position) > hitscanDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)          // �÷��̾ �ǰ� ��
    {
        if (collision.CompareTag("Player"))
        {
            if(playerInvincible == false)
            {
                HealthGauge.health -= 0.5f;
            }
        }
        if (collision.CompareTag("Web")||collision.CompareTag("WebLv4"))    // �Ź��� Ʈ���� �ǰ� ��
        {
            StartCoroutine("Slow");         // �̵��ӵ� ���� �ڷ�ƾ�޼ҵ� ����
        }
    }
    private void OnTriggerExit2D(Collider2D collision)          // �ǰ� ���� X
    {
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
    private IEnumerator Slow()          // �� �̵��ӵ� ���� �ڷ�ƾ�޼ҵ�
    {
        if (canSlow)            // �̵��ӵ� ���� ���ɻ��¸� �̵��ӵ� ����
        {
            canSlow = false;
            moveSpeed = slowSpeed;
            yield return new WaitForSeconds(4f);        // 4�� ����
            moveSpeed = speed;
        }
        canSlow = true;
    }
}