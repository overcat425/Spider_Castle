using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CastleMenu : MonoBehaviour
{
    public EnhanceMenu enhanceMenu;     // ��ų��ȭ �޴�
    Vector3 defaultScale;                       // ��ư ũ�� ����

    [SerializeField]    private GameObject castleSpider;        // �ֹ� �Ź̵�
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
        PlayerController.isClear = false;           // ���� Ŭ��� �ʿ��� ���� �ʱ�ȭ
        PlayerController.keysCount = 0;         // ���� Ŭ��� �ʿ��� ��ȭ �ʱ�ȭ
        hideHelp = SaveManager.hideNoticeStat;      // ���� ���� üũ
        ShowTips();
    }
    void Start()
    {
        Time.timeScale = 1f;
        InvokeRepeating("SpawnCastleSpider", 0, 1);     // �ֹ� �Ź� ��ȸ ���� 1
        InvokeRepeating("SpawnCastleSpider2", 1, 2);   // �ֹ� �Ź� ��ȸ ���� 2
    }
    void Update()
    {        
    }
    public void OnClickDungeon()        // �����޴� Ŭ��
    {
        dungeonUi.SetActive(true);
    }
    public void OnClickLab()                // ������ �޴� Ŭ��
    {
        enhanceMenu.CallMenu();
        enhanceMenu.OnClickLabTab();
    }
    public void OnClickEnhancement()            // ��ȭ �޴� Ŭ��
    {
        enhanceMenu.CallMenu();
        enhanceMenu.OnClickEnhanceTab();
    }
    public void OnClickCobweb()             // �Ź̼��� �޴� Ŭ��
    {
        enhanceMenu.CallMenu();
        enhanceMenu.OnClickCobTab();
    }
    public void OnClickExit()
    {
        enhanceMenu.CloseMenu();
    }
    private void ShowTips()                 // ���� ON/OFF
    {
        if (hideHelp == true)
        {
            helpNotice.SetActive(false);

        }else if (hideHelp == false)
        {
            OnClickShowCob();
        }
    }
    public void OnClickShowCob()        // �Ź̼��� ��
    {
        helpNotice.SetActive(true);
        cobTip.SetActive(true);
        labTip.SetActive(false);
        enhanceTip.SetActive(false);
        dungeonTip.SetActive(false);
    }
    public void OnClickShowLab()        // ������ ��
    {
        cobTip.SetActive(false);
        labTip.SetActive(true);
        enhanceTip.SetActive(false);
        dungeonTip.SetActive(false);
    }
    public void OnClickShowEnhance()            // ��ų��ȭ ��
    {
        cobTip.SetActive(false);
        labTip.SetActive(false);
        enhanceTip.SetActive(true);
        dungeonTip.SetActive(false);
    }
    public void OnClickShowDungeon()            // ���� ��
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
    public void SpawnCastleSpider()     // �ֹΰŹ� ���� ��ȸ �޼ҵ� 1
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
    public void SpawnCastleSpider2()    // �ֹΰŹ� ���� ��ȸ �޼ҵ� 2
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