using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Spider : MonoBehaviour
{
    private bool clear;
    private bool isOpen;
    private bool isLike;
    [SerializeField] private GameObject keyTip;
    [SerializeField] private GameObject wantKey;
    [SerializeField] private GameObject cageMoving;
    [SerializeField] private GameObject cageMoving2;
    [SerializeField] private GameObject cage;
    [SerializeField] private GameObject cageOpen;
    [SerializeField] private GameObject liked;
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip likeSound;
    void Start()
    {
        isLike = false;
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
            Invoke("DoorOpen", 2f);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerController.keysCount == 3)
        {
            if (collision.CompareTag("Player"))
            {
                SaveManager.getSkillEnableStat[2] = true;
                //Destroy(gameObject);
            }
        }
        else if (PlayerController.keysCount < 3)
        {
            keyTip.SetActive(true);
            Invoke("KeyToolTip", 2);
        }
    }
    private void DoorOpen()
    {
        if (isLike == false)
        {
            liked.SetActive(true);
            SoundManager.SoundEffect.SoundPlay("likeSound", likeSound);
            isLike = true;
        }
    }
    public void KeyToolTip()
    {
        keyTip.SetActive(false);
    }
}
