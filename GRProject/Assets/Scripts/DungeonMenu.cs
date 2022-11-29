using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DungeonMenu : MonoBehaviour
{
    [SerializeField] private GameObject dungeonUi;

    [Header("스테이지")]
    [SerializeField] private GameObject stage1info;
    [SerializeField] private GameObject stage2info;
    [SerializeField] private GameObject stage3info;
    void Start()
    {
        stage1info.SetActive(false);
        stage2info.SetActive(false);
        stage3info.SetActive(false);
    }
    void Update()
    {
        
    }
    public void OnClickStage1()
    {
        stage1info.SetActive(true);
        stage2info.SetActive(false);
        stage3info.SetActive(false);
    }
    public void OnClickStage2()
    {
        stage1info.SetActive(false);
        stage2info.SetActive(true);
        stage3info.SetActive(false);
    }
    public void OnClickStage3()
    {
        stage1info.SetActive(false);
        stage2info.SetActive(false);
        stage3info.SetActive(true);
    }
    public void StartStage1()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void StartStage2()
    {

    }
    public void StartStage3()
    {

    }
}
