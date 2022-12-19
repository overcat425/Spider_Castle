using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnPool : MonoBehaviour
{
    [Header("적 오브젝트 생성")]
    [SerializeField]    private GameObject snail;
    [SerializeField]    private GameObject enemy;
    [SerializeField]    private GameObject enemy2;
    [SerializeField]    private GameObject enemy3;
    [SerializeField]    private GameObject enemy4;
    [SerializeField]    private GameObject enemy5;
    [SerializeField]    private GameObject boss;
    [SerializeField]    private GameObject endemy;
    [SerializeField]    private GameObject bossGauge;
    [SerializeField]    private GameObject stageBgm;
    [SerializeField]    private GameObject bossBgm;
    [SerializeField]
    private int stage = 4;

    [Header("적 처치")]
    public int EnemyKilledCount;            // 적 카운터
    public static int enemyKilledCountStat;
    [SerializeField]
    public Text EnemyKilled;                // 카운터값을 보여주는 텍스트
    private static EnemySpawnPool instance;
    public static EnemySpawnPool count_instance     // 오브젝트가 파괴되는
    {                                  // 외부 스크립트에 가서 직접 카운트 하는 인스턴스
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<EnemySpawnPool>();
            }
            return instance;
        }
    }
    void Start()
    {
        InvokeRepeating("Snail", 0, 2);
        InvokeRepeating("Spawn", 60, 4);
        InvokeRepeating("Spawn2", 120, 10);
        InvokeRepeating("Spawn3", 180, 20);
        InvokeRepeating("Spawn4", 240, 25);
        InvokeRepeating("Spawn5", 300, 30);
        Invoke("BossSpawn", 600);
        InvokeRepeating("Endemy", 900, 5);
    }
    void Update()
    {
        EnemyKilled.text = EnemyKilledCount.ToString();
        enemyKilledCountStat = EnemyKilledCount;
        if (BossStatus.bossHealth <= 0)
        {
            BossDie();
        }
    }
    private void Snail()
    {
        Instantiate(snail, new Vector3(Random.Range(4000, 5000), Random.Range(-4000, 4000), 0), Quaternion.identity);
        Instantiate(snail, new Vector3(Random.Range(-5000, -4000), Random.Range(-4000, 4000), 0), Quaternion.identity);
        Instantiate(snail, new Vector3(Random.Range(-5000, 5000), Random.Range(-4000, -3000), 0), Quaternion.identity);
        Instantiate(snail, new Vector3(Random.Range(-5000, 5000), Random.Range(3000, 4000), 0), Quaternion.identity);
    }
    private void Spawn()
    {
        Instantiate(enemy, new Vector3(Random.Range(4000, 5000), Random.Range(-4000, 4000), 0), Quaternion.identity);
        Instantiate(enemy, new Vector3(Random.Range(-5000, -4000), Random.Range(-4000, 4000), 0), Quaternion.identity);
        Instantiate(enemy, new Vector3(Random.Range(-5000, 5000), Random.Range(-4000, -3000), 0), Quaternion.identity);
        Instantiate(enemy, new Vector3(Random.Range(-5000, 5000), Random.Range(3000, 4000), 0), Quaternion.identity);
    }
    private void Spawn2()
    {
        Instantiate(enemy2, new Vector3(Random.Range(4000, 5000), Random.Range(-4000, 4000), 0), Quaternion.identity);
        Instantiate(enemy2, new Vector3(Random.Range(-5000, -4000), Random.Range(-4000, 4000), 0), Quaternion.identity);
        Instantiate(enemy2, new Vector3(Random.Range(-5000, 5000), Random.Range(-4000, -3000), 0), Quaternion.identity);
        Instantiate(enemy2, new Vector3(Random.Range(-5000, 5000), Random.Range(3000, 4000), 0), Quaternion.identity);
    }
    private void Spawn3()
    {
        Instantiate(enemy3, new Vector3(Random.Range(4000, 5000), Random.Range(-4000, 4000), 0), Quaternion.identity);
        Instantiate(enemy3, new Vector3(Random.Range(-5000, -4000), Random.Range(-4000, 4000), 0), Quaternion.identity);
        Instantiate(enemy3, new Vector3(Random.Range(-5000, 5000), Random.Range(-4000, -3000), 0), Quaternion.identity);
        Instantiate(enemy3, new Vector3(Random.Range(-5000, 5000), Random.Range(3000, 4000), 0), Quaternion.identity);
    }
    private void Spawn4()
    {
        Instantiate(enemy4, new Vector3(Random.Range(4000, 5000), Random.Range(-4000, 4000), 0), Quaternion.identity);
        Instantiate(enemy4, new Vector3(Random.Range(-5000, -4000), Random.Range(-4000, 4000), 0), Quaternion.identity);
        Instantiate(enemy4, new Vector3(Random.Range(-5000, 5000), Random.Range(-4000, -3000), 0), Quaternion.identity);
        Instantiate(enemy4, new Vector3(Random.Range(-5000, 5000), Random.Range(3000, 4000), 0), Quaternion.identity);
    }
    private void Spawn5()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
        case 0:
            Instantiate(enemy5, new Vector3(Random.Range(4000, 5000), Random.Range(-4000, 4000), 0), Quaternion.identity); break;
        case 1:
            Instantiate(enemy5, new Vector3(Random.Range(-5000, -4000), Random.Range(-4000, 4000), 0), Quaternion.identity); break;
        case 2:
            Instantiate(enemy5, new Vector3(Random.Range(-5000, 5000), Random.Range(-4000, -3000), 0), Quaternion.identity); break;
        case 3:
            Instantiate(enemy5, new Vector3(Random.Range(-5000, 5000), Random.Range(3000, 4000), 0), Quaternion.identity); break;
        }
    }
    private void BossSpawn()
    {
        stageBgm.SetActive(false);
        bossBgm.SetActive(true);
        bossGauge.SetActive(true);
        Instantiate(boss, new Vector3(Random.Range(-3800, 3800), Random.Range(-2900, 2900), -0.1f), Quaternion.identity);
    }
    private void BossDie()
    {
        bossBgm.SetActive(false);
        stageBgm.SetActive(true);
        bossGauge.SetActive(false);
    }
    private void Endemy()
    {
        Instantiate(endemy, new Vector3(Random.Range(4000, 5000), Random.Range(-4000, 4000), 0), Quaternion.identity);
        Instantiate(endemy, new Vector3(Random.Range(-5000, -4000), Random.Range(-4000, 4000), 0), Quaternion.identity);
        Instantiate(endemy, new Vector3(Random.Range(-5000, 5000), Random.Range(-4000, -3000), 0), Quaternion.identity);
        Instantiate(endemy, new Vector3(Random.Range(-5000, 5000), Random.Range(3000, 4000), 0), Quaternion.identity);
    }
}
