using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour               // 장애물 반투명 스크립트
{
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)     // 플레이어와 겹칠 시 반투명
    {
        if (collision.CompareTag("Player"))
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            spriteRenderer.color = new Color(1, 1, 1, 1f);
    }
}