using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Manager : MonoBehaviour
{
    [Header("Å¬¸®¾î")]
    [SerializeField] private GameObject spawnPool;
    [SerializeField] private AudioClip clearSound;
    private float time = 0f;
    private bool isClear;
    [SerializeField]
    private GameObject clearUi;
    private GameObject killed;
    private GameObject coins;
    public Transform clearUiSize;
    public Transform killedUiSize;
    public Transform coinsUiSize;
    [SerializeField] private GameObject jumpUnlock;
    [SerializeField] private GameObject somethingUnlock;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
