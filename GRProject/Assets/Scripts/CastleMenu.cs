using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CastleMenu : MonoBehaviour
{
    public EnhanceMenu enhanceMenu;
    Vector3 defaultScale;                       // ��ư ũ�� ����

    [SerializeField]    private GameObject castleSpider;
    [SerializeField]    private GameObject castleSpiderRev;
    [SerializeField]    private GameObject castleSpider2;
    [SerializeField]    private GameObject castleSpider2Rev;
    [SerializeField]    private GameObject dungeonUi;

    [Header("����")]
    public bool hideHelp;
    [SerializeField]    private GameObject helpNotice;
    [SerializeField]    private GameObject cobTip;
    [SerializeField]    private GameObject labTip;
    [SerializeField]    private GameObject enhanceTip;
    [SerializeField]    private GameObject dungeonTip;
    private void Awake()
    {
        HealthGauge.canAutoSave = false;
        PlayerController.isClear = false;
        PlayerController.keysCount = 0;
        hideHelp = SaveManager.hideNoticeInstance;
        ShowTips();
    }
    void Start()
    {
        InvokeRepeating("SpawnCastleSpider", 0, 1);
        InvokeRepeating("SpawnCastleSpider2", 1, 2);
    }
    void Update()
    {        
    }
    public void OnClickDungeon()
    {
        Debug.Log("����");
        dungeonUi.SetActive(true);
    }
    public void OnClickLab()
    {
        Debug.Log("������");
        enhanceMenu.CallMenu();
        enhanceMenu.OnClickLabTab();
    }
    public void OnClickEnhancement()
    {
        Debug.Log("��ȭ��");
        enhanceMenu.CallMenu();
        enhanceMenu.OnClickEnhanceTab();
    }
    public void OnClickCobweb()
    {
        Debug.Log("�Ź� ����");
        enhanceMenu.CallMenu();
        enhanceMenu.OnClickCobTab();
    }
    public void OnClickExit()
    {
        enhanceMenu.CloseMenu();
    }
    private void ShowTips()
    {
        if (hideHelp == true)
        {
            helpNotice.SetActive(false);
            Debug.Log("���� ����" + hideHelp);

        }else if (hideHelp == false)
        {
            OnClickShowCob();
            Debug.Log("���� ����" + hideHelp);
        }
    }
    public void OnClickShowCob()
    {
        helpNotice.SetActive(true);
        cobTip.SetActive(true);
        labTip.SetActive(false);
        enhanceTip.SetActive(false);
        dungeonTip.SetActive(false);
    }
    public void OnClickShowLab()
    {
        cobTip.SetActive(false);
        labTip.SetActive(true);
        enhanceTip.SetActive(false);
        dungeonTip.SetActive(false);
    }
    public void OnClickShowEnhance()
    {
        cobTip.SetActive(false);
        labTip.SetActive(false);
        enhanceTip.SetActive(true);
        dungeonTip.SetActive(false);
    }
    public void OnClickShowDungeon()
    {
        cobTip.SetActive(false);
        labTip.SetActive(false);
        enhanceTip.SetActive(false);
        dungeonTip.SetActive(true);
    }
    public void OnClickTipoff()
    {
        helpNotice.SetActive(false);
    }
    public void OnClickDungeonoff()
    {
        dungeonUi.SetActive(false);
    }
    public void SpawnCastleSpider()
    {
        int wander = Random.Range(0, 2);
        if(wander == 0)
        {
            Instantiate(castleSpider, new Vector3(Random.Range(0, 1920), Random.Range(0, 250), 0), Quaternion.identity, GameObject.Find("Castle").transform);
        }
        else if(wander == 1)
        {
            Instantiate(castleSpiderRev, new Vector3(Random.Range(0, 1920), Random.Range(0, 250), 0), Quaternion.identity, GameObject.Find("Castle").transform);
        }
    }
    public void SpawnCastleSpider2()
    {
        int wander2 = Random.Range(0, 2);
        if (wander2 == 0)
        {
            Instantiate(castleSpider2, new Vector3(Random.Range(0, 1920), Random.Range(320, 540), 0), Quaternion.identity, GameObject.Find("Castle").transform);
        }
        else if (wander2 == 1)
        {
            Instantiate(castleSpider2Rev, new Vector3(Random.Range(0, 1920), Random.Range(320, 540), 0), Quaternion.identity, GameObject.Find("Castle").transform);
        }
    }
}