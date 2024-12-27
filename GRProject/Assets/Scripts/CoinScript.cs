using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private EnemyChasing enemyChasing;
    private SaveManager saveManager;
    Transform target;

    [SerializeField]
    private AudioClip coinSound;            // 코인 획득 사운드
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();  // 타겟 == 플레이어로 초기화
    }
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) < 250) // 플레이어가 범위 안에 들어오면
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 5f);  // 자동으로 플레이어에게 흡수
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))     // 플레이어와 닿으면
        {
            Collected();                    // 코인 획득
        }
    }
    public void Collected()
    {
        CoinManager.count_instance.earnedCoins++;       // 코인 카운트 +1
        SoundManager.SoundEffect.SoundPlay("coinSound", coinSound); // 코인 획득 사운드 출력
        Destroy(gameObject);
    }
}
