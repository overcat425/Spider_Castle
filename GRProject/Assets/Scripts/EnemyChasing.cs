using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasing : MonoBehaviour
{
    Rigidbody2D enemyBody;
    Transform target;

    [Header("속도")]
    [SerializeField]
    private float moveSpeed = 5f;

    [Header("타격 거리")]
    [SerializeField]
    private float HitscanDistance = 1f;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        TargetChasing();
        MobFlip();
    }
    private void TargetChasing()                    // 플레이어 추적
    {
        if(Vector2.Distance(transform.position, target.position) > HitscanDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)          // 피격 시
    {
        HealthGauge.health -= 2f;
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
            transform.localScale = new Vector3(-100, 100, 1);
        }
        else if (target.position.x < transform.position.x)
        {
            CharacterFlip = Vector3.right;
            transform.localScale = new Vector3(100, 100, 1);
        }
    }
}
