using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        InvokeRepeating("Spawn", 0, spawnTime);
    }
    void Update()
    {

    }
    private void Spawn()
    {
        Instantiate(enemy, new Vector3(Random.Range(4000, 5000), Random.Range(-4000, 4000), 0), Quaternion.identity);
        Instantiate(enemy, new Vector3(Random.Range(-5000, -4000), Random.Range(-4000, 4000), 0), Quaternion.identity);
        Instantiate(enemy, new Vector3(Random.Range(-5000, 5000), Random.Range(-4000, -3000), 0), Quaternion.identity);
        Instantiate(enemy, new Vector3(Random.Range(-5000, 5000), Random.Range(3000, 4000), 0), Quaternion.identity);
    }
}
