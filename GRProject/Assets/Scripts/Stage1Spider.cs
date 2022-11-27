using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Spider : MonoBehaviour
{
    [SerializeField] private GameObject keyTip;
    void Start()
    {
    }

    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerController.keysCount == 3)
        {
            if (collision.CompareTag("Player"))
            {
                SaveManager.getSkill3EnableInstance = true;
                SaveManager.getSkill4EnableInstance = true;
                Destroy(gameObject);
            }
        }else if (PlayerController.keysCount < 3){
            keyTip.SetActive(true);
            Invoke("KeyToolTip", 2);
        }
    }
    public void KeyToolTip()
    {
        keyTip.SetActive(false);
    }
}
