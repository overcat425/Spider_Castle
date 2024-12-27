using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private EnemyChasing enemyChasing;
    private SaveManager saveManager;
    Transform target;

    [SerializeField]
    private AudioClip coinSound;            // ���� ȹ�� ����
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();  // Ÿ�� == �÷��̾�� �ʱ�ȭ
    }
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) < 250) // �÷��̾ ���� �ȿ� ������
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 5f);  // �ڵ����� �÷��̾�� ���
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))     // �÷��̾�� ������
        {
            Collected();                    // ���� ȹ��
        }
    }
    public void Collected()
    {
        CoinManager.count_instance.earnedCoins++;       // ���� ī��Ʈ +1
        SoundManager.SoundEffect.SoundPlay("coinSound", coinSound); // ���� ȹ�� ���� ���
        Destroy(gameObject);
    }
}
