using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearGeneScript : MonoBehaviour
{
    private bool clear;
    private bool isOpen;
    [SerializeField] private GameObject keyTip;                 // 열쇠 도움말
    [SerializeField] private GameObject wantKey;                // 키 요구 연출
    [SerializeField] private GameObject cageMoving;           // 철창 움직임 1
    [SerializeField] private GameObject cageMoving2;         // 철창 움직임 2
    [SerializeField] private GameObject cage;                    // 철창
    [SerializeField] private GameObject cageOpen;             // 철창 개방 애니메이션
    [SerializeField] private AudioClip doorOpenSound;       // 철창 개방 사운드
    void Start()
    {
        isOpen = false;
    }
    void Update()
    {
        clear = PlayerController.isClear;
        if (clear == true)
        {
            if (isOpen == false)
            {
                SoundManager.SoundEffect.SoundPlay("doorOpenSound", doorOpenSound);
                isOpen = true;
            }
            wantKey.SetActive(false);
            cageMoving.SetActive(false);
            cageMoving2.SetActive(false);
            cage.SetActive(false);
            cageOpen.SetActive(true);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerController.keysCount == 3)        // 키 카운트(3개까지)
        {
            if (collision.CompareTag("Player"))
            {
                CoinManager.count_geneInstance.earnedGene++;
            }
        }
        else if (PlayerController.keysCount < 3)
        {
            keyTip.SetActive(true);
            Invoke("KeyToolTip", 2);
        }
    }
    public void KeyToolTip()
    {
        keyTip.SetActive(false);
    }
}
