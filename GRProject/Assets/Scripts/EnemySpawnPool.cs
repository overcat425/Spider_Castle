using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnPool : MonoBehaviour
{
    [Header("�� ������Ʈ ����")]
    [SerializeField]    private GameObject snail;
    [SerializeField]    private GameObject enemy;
    [SerializeField]    private GameObject enemy2;
    [SerializeField]    private GameObject enemy3;
    [SerializeField]    private GameObject boss;
    [SerializeField]    private GameObject bossGauge;
    [SerializeField]    private GameObject stageBgm;
    [SerializeField]    private GameObject bossBgm;
    [SerializeField]
    private int stage = 4;

    [Header("�� óġ")]
    public int EnemyKilledCount;            // �� ī����
    public static int enemyKilledCountInstance;
    [SerializeField]
    public Text EnemyKilled;                // ī���Ͱ��� �����ִ� �ؽ�Ʈ
    private static EnemySpawnPool instance;
    public static EnemySpawnPool count_instance     // ������Ʈ�� �ı��Ǵ�
    {                                  // �ܺ� ��ũ��Ʈ�� ���� ���� ī��Ʈ �ϴ� �ν��Ͻ�
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
        Invoke("BossSpawn", 300);
    }
    void Update()
    {
        EnemyKilled.text = EnemyKilledCount.ToString();
        enemyKilledCountInstance = EnemyKilledCount;
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
    private void BossSpawn()
    {
        stageBgm.SetActive(false);
        bossBgm.SetActive(true);
        bossGauge.SetActive(true);
        Instantiate(boss, new Vector3(Random.Range(-3800, 3800), Random.Range(-2900, 2900), 0), Quaternion.identity);
    }
    private void BossDie()
    {
        bossBgm.SetActive(false);
        stageBgm.SetActive(true);
        bossGauge.SetActive(false);
    }
}
