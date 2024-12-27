using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip getSound;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))     // 키가 플레이어와 닿으면 획득 후 키 카운트 +1
        {
            SoundManager.SoundEffect.SoundPlay("getSound", getSound);
            PlayerController.keysCount++;
            Destroy(gameObject);
        }
    }
}
