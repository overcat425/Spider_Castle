using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnPool : MonoBehaviour
{
    [Header("�� ������Ʈ ����")]
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private Transform[] spawnEnemy;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private int stage = 4;

    [Header("�� óġ")]
    public int EnemyKilledCount;            // �� ī����
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
