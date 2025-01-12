using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnerHard : MonoBehaviour
{
    public GameObject[] enemyObject;
    List<GameObject>[] enemyPool;
    public Transform[] spawnPoint;
    float timer;
    float timeDelta;
    int level;
    public int kills;
    public Transform texts;

    public int EnemyKilledCount;            // 적 카운터
    public Text EnemyKilled;                // 카운터값을 보여주는 텍스트
    public static int enemyKilledCountStat;

    [Header("Boss")]
    public GameObject stageBgm;
    public GameObject bossBgm;
    public GameObject bossGauge;

    public static EnemySpawnerHard instance;
    public static EnemySpawnerHard count_instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<EnemySpawnerHard>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        enemyPool = new List<GameObject>[enemyObject.Length];
        for (int i = 0; i < enemyPool.Length; i++)
        {
            enemyPool[i] = new List<GameObject>();
        }
    }
    private void Start()
    {
        InvokeRepeating("BossSpawn", 300, 300);
        InvokeRepeating("Endemy", 900, 5);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        timeDelta += Time.deltaTime;
        level = (int)timeDelta / 60;
        if (level > 5) level = 5;
        if (timer > 0.5f)
        {
            SpawnEnemies();
            timer = 0f;
        }
        EnemyKilled.text = EnemyKilledCount.ToString();
        enemyKilledCountStat = EnemyKilledCount;
        if (BossStatus.bossHealth <= 0)
        {
            BossDie();
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i <= level; i++)            // 저렙 몹 생성
        {
            if (level > 2) level = 1;
            GameObject enemy = SpawnEnemy(i);
            enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        }
        if (timeDelta >= 120f && (int)timeDelta % 5 == 0)
        {
            for (int i = 2; i <= level; i++)
            {
                GameObject enemy = SpawnEnemy(i);
                enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
            }
        }
    }
    public GameObject SpawnEnemy(int i)
    {
        GameObject active = null;
        foreach (GameObject item in enemyPool[i])
        {
            if (item.activeSelf == false)
            {
                active = item;
                active.SetActive(true);
                break;
            }
        }
        if (active == false)
        {
            active = Instantiate(enemyObject[i], transform);
            enemyPool[i].Add(active);
        }
        return active;
    }
    private void BossSpawn()
    {
        stageBgm.SetActive(false);
        bossBgm.SetActive(true);
        bossGauge.SetActive(true);
        Instantiate(enemyObject[6], new Vector3(Random.Range(-3800, 3800), Random.Range(-2900, 2900), -0.1f), Quaternion.identity);
    }
    private void BossDie()
    {
        bossBgm.SetActive(false);
        stageBgm.SetActive(true);
        bossGauge.SetActive(false);
    }
    private void Endemy()
    {
        for (int i = 0; i <= 3; i++)
        {
            GameObject enemy = SpawnEnemy(7);
            enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        }
    }
}