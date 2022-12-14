using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearGeneScript : MonoBehaviour
{
    private bool clear;
    private bool isOpen;
    [SerializeField] private GameObject keyTip;
    [SerializeField] private GameObject wantKey;
    [SerializeField] private GameObject cageMoving;
    [SerializeField] private GameObject cageMoving2;
    [SerializeField] private GameObject cage;
    [SerializeField] private GameObject cageOpen;
    [SerializeField] private AudioClip doorOpenSound;
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
        if (PlayerController.keysCount == 3)
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
