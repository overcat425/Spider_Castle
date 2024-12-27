using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneScript : MonoBehaviour
{
    private EnemyChasing enemyChasing;
    private SaveManager saveManager;
    Transform target;               // 추적 타겟

    [SerializeField]
    private AudioClip coinSound;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();      // 타겟을 플레이어로 설정
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < 150)        // 플레이어가 거리 150 안으로 들어오면
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 5f);      // 플레이어에게 흡수
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collected();
        }
    }
    public void Collected()    // 플레이어가 재화 획득 시 인스턴스로 코인매니저스크립트에 정보전달
    {
        CoinManager.count_geneInstance.earnedGene++;
        SoundManager.SoundEffect.SoundPlay("coinSound", coinSound);
        Destroy(gameObject);
    }
}
