using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    // public GameObject Enemy1Prefab;
    //public GameObject Enemy2Prefab;

    public Coordinate[] Coordinates =
    {
        new Coordinate(-3,1),
        new Coordinate(-3,-1),
        new Coordinate(3,-1),
        new Coordinate(3,1),
        new Coordinate(3, 3),
        new Coordinate(3,-3),
        new Coordinate(-3,3),
        new Coordinate(-3,-3),
    };

    void Start()
    {
        EnemyRandom();
        //SpawnEnemy(Enemy1Prefab, new Vector3(1, 2, 0));
        //SpawnEnemy(Enemy2Prefab, new Vector3(-1, 2, 0));
    }

    public void SpawnEnemy(GameObject prefab, Vector3 _position)
    {
        GameObject enemy = Instantiate(prefab);
        enemy.transform.position = _position;
        enemy.GetComponent<Enemy>().Move();
    }

    public void EnemyRandom()
    {
        GameObject enemyPrefab = EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)];
        Vector2 enemyPosition = Coordinates[Random.Range(0, Coordinates.Length)].GetPosition();
        SpawnEnemy(enemyPrefab, enemyPosition);
        Invoke("EnemyRandom", 0.5f);
    }

    public struct Coordinate
    {
        public int x;
        public int y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(x, y);
        }
    }
}
