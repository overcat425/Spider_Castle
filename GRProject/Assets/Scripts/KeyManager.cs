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
        if (collision.CompareTag("Player"))     // Ű�� �÷��̾�� ������ ȹ�� �� Ű ī��Ʈ +1
        {
            SoundManager.SoundEffect.SoundPlay("getSound", getSound);
            PlayerController.keysCount++;
            Destroy(gameObject);
        }
    }
}
