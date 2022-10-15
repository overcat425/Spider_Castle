using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnPool : MonoBehaviour
{
    [Header("적 오브젝트 생성")]
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private Transform[] spawnEnemy;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private int stage = 4;

    [Header("적 처치")]
    public int EnemyKilledCount;            // 적 카운터
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
        InvokeRepeating("Spawn", 0, spawnTime);
    }
    void Update()
    {
        EnemyKilled.text = EnemyKilledCount.ToString();
    }
    private void Spawn()
    {
        Instantiate(enemy, new Vector3(Random.Range(4000, 5000), Random.Range(-4000, 4000), 0), Quaternion.identity);
        Instantiate(enemy, new Vector3(Random.Range(-5000, -4000), Random.Range(-4000, 4000), 0), Quaternion.identity);
        Instantiate(enemy, new Vector3(Random.Range(-5000, 5000), Random.Range(-4000, -3000), 0), Quaternion.identity);
        Instantiate(enemy, new Vector3(Random.Range(-5000, 5000), Random.Range(3000, 4000), 0), Quaternion.identity);
    }
}
