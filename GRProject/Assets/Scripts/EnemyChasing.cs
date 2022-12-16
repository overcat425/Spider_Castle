using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyChasing : MonoBehaviour
{
    Rigidbody2D enemyBody;
    Transform target;
    private float flipX;
    private HealthGauge healthGauge;

    [Header("속도")]
    [SerializeField]    private float speed;
    [SerializeField]    private float slowSpeed;
    [SerializeField]    private float moveSpeed;

    [Header("타격 거리")]
    [SerializeField]
    private float hitscanDistance = 1f;

    private bool canSlow = true;
    private bool playerInvincible;

    private void Awake()
    {
        flipX = -transform.localScale.x;
    }
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        slowSpeed = speed * 0.2f;
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
    private void OnTriggerStay2D(Collider2D collision)          // 피격 시
    {
        if (collision.CompareTag("Player"))
        {
            if(playerInvincible == false)
            {
                HealthGauge.health -= 0.5f;
            }
        }
        if (collision.CompareTag("Web")||collision.CompareTag("WebLv4"))
        {
            StartCoroutine("Slow");
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
    private IEnumerator Slow()
    {
        if (canSlow)
        {
            canSlow = false;
            moveSpeed = slowSpeed;
            yield return new WaitForSeconds(4f);
            moveSpeed = speed;
        }
        canSlow = true;
    }
}