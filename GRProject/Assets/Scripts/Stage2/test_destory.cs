using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_destory : MonoBehaviour
{
     private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

     void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            HealthGauge.health += 20f;
            Destroy (this.gameObject);
        }
    }
    // 플레이어와 충돌시 삭제
}


