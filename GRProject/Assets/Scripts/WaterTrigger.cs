using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaterTrigger : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private AudioClip waterSound;
    private SpriteRenderer spriteRenderer;
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

            playerPos.position = new Vector3(Random.Range(-3800, 3800), Random.Range(-2800, 2800), 0);
            SoundManager.SoundEffect.SoundPlay("waterSound", waterSound);
        }
    }
}