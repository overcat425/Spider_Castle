using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMemoryPool : MonoBehaviour
{
    [SerializeField]
    private Transform target;                           // 적의 타겟 (플레이어)
    [SerializeField]
    private GameObject enemySpawnPointPrefab;
    [SerializeField]
    private GameObject enemyPrefab;             // 적 프리팹
    [SerializeField]
    private float enemySpawnTime = 1;           // 적 생성 주기
    [SerializeField]
    private float enemySpawnLatency = 1;        // 적 생성 시간간격

    private MemoryPool spawnPointMemoryPool;
    private MemoryPool enemyMemoryPool;     // 적 생성 관리

    private int numOfEnemiesSpawnedAtOnce = 5;                 // 동시에 생성되는 적 수
    private void Awake()
    {
        spawnPointMemoryPool = new MemoryPool(enemySpawnPointPrefab);
        enemyMemoryPool = new MemoryPool(enemyPrefab);

        StartCoroutine("SpawnTile");
    }

    private IEnumerator SpawnTile()
    {
        int currentNum = 0;
        int maxNum = 50;
        while (true)
        {
            for (int i = 0; i < numOfEnemiesSpawnedAtOnce; ++i)
            {
                GameObject item1 = spawnPointMemoryPool.ActivatePoolItem();
                GameObject item2 = spawnPointMemoryPool.ActivatePoolItem();
                GameObject item3 = spawnPointMemoryPool.ActivatePoolItem();
                GameObject item4 = spawnPointMemoryPool.ActivatePoolItem();
                // 기존 메모리풀 방식
                //item.transform.position = new Vector3(Random.Range(-spawnPool1.x * 0.49f, spawnPool1.x * 0.49f), Random.Range(-spawnPool1.y * 0.49f, spawnPool1.y * 0.49f), 10);
                item1.transform.position = new Vector3(Random.Range(4000, 5000), Random.Range(-4000, 4000), -100);
                item2.transform.position = new Vector3(Random.Range(-5000, -4000), Random.Range(-4000, 4000), -100);
                item3.transform.position = new Vector3(Random.Range(-5000, 5000), Random.Range(-4000, -3000), -100);
                item4.transform.position = new Vector3(Random.Range(-5000, 5000), Random.Range(3000, 4000), -100);
                StartCoroutine("SpawnEnemy", item1);
                StartCoroutine("SpawnEnemy", item2);
                StartCoroutine("SpawnEnemy", item3);
                StartCoroutine("SpawnEnemy", item4);
            }
            currentNum++;
            if (currentNum >= maxNum)
            {
                currentNum = 0;
                numOfEnemiesSpawnedAtOnce++;
            }
            yield return new WaitForSeconds(enemySpawnTime);
        }
    }
    private IEnumerator SpawnEnemy(GameObject point)
    {
        yield return new WaitForSeconds(enemySpawnLatency);

        GameObject item = enemyMemoryPool.ActivatePoolItem();
        item.transform.position = point.transform.position;
        item.GetComponent<EnemyAttack>().Setup(target, this);

        spawnPointMemoryPool.InactivatePoolItem(point);
    }
    public void InactivateEnemy(GameObject enemy)
    {
        enemyMemoryPool.InactivatePoolItem(enemy);
    }
}
