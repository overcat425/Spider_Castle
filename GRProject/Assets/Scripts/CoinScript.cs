using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private EnemyChasing enemyChasing;
    private SaveManager saveManager;
    Transform target;

    [SerializeField]
    private AudioClip coinSound;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) < 250)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 5f);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.CompareTag("MagnetField")) // Update문의 조건으로 변경
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, target.position, 10f);
        //}
        if (collision.CompareTag("Player"))
        {
            Collected();
        }
    }
    public void Collected()
    {
        CoinManager.count_instance.earnedCoins++;
        SoundManager.SoundEffect.SoundPlay("coinSound", coinSound);
        Destroy(gameObject);
    }
}
