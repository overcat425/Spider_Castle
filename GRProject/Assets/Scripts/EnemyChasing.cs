using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasing : MonoBehaviour
{
    Rigidbody2D enemyBody;
    Transform target;

    [Header("�ӵ�")]
    [SerializeField]
    private float moveSpeed = 5f;

    [Header("Ÿ�� �Ÿ�")]
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
    private void TargetChasing()                    // �÷��̾� ����
    {
        if(Vector2.Distance(transform.position, target.position) > HitscanDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)          // �ǰ� ��
    {
        HealthGauge.health -= 2f;
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
            transform.localScale = new Vector3(-100, 100, 1);
        }
        else if (target.position.x < transform.position.x)
        {
            CharacterFlip = Vector3.right;
            transform.localScale = new Vector3(100, 100, 1);
        }
    }
}
