using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneScript : MonoBehaviour
{
    private EnemyChasing enemyChasing;
    private SaveManager saveManager;
    Transform target;               // ���� Ÿ��

    [SerializeField]
    private AudioClip coinSound;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();      // Ÿ���� �÷��̾�� ����
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < 150)        // �÷��̾ �Ÿ� 150 ������ ������
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 5f);      // �÷��̾�� ���
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collected();
        }
    }
    public void Collected()    // �÷��̾ ��ȭ ȹ�� �� �ν��Ͻ��� ���θŴ�����ũ��Ʈ�� ��������
    {
        CoinManager.count_geneInstance.earnedGene++;
        SoundManager.SoundEffect.SoundPlay("coinSound", coinSound);
        Destroy(gameObject);
    }
}
