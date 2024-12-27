using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CastleMenu : MonoBehaviour
{
    public EnhanceMenu enhanceMenu;     // 스킬강화 메뉴
    Vector3 defaultScale;                       // 버튼 크기 벡터

    [SerializeField]    private GameObject castleSpider;        // 주민 거미들
    [SerializeField]    private GameObject castleSpiderRev;
    [SerializeField]    private GameObject castleSpider2;
    [SerializeField]    private GameObject castleSpider2Rev;
    [SerializeField]    private GameObject dungeonUi;

    [Header("도움말")]
    public bool hideHelp;
    [SerializeField]    private GameObject helpNotice;
    [SerializeField]    private GameObject cobTip;
    [SerializeField]    private GameObject labTip;
    [SerializeField]    private GameObject enhanceTip;
    [SerializeField]    private GameObject dungeonTip;
    private void Awake()
    {
        HealthGauge.canAutoSave = false;
        PlayerController.isClear = false;           // 던전 클리어에 필요한 조건 초기화
        PlayerController.keysCount = 0;         // 던전 클리어에 필요한 재화 초기화
        hideHelp = SaveManager.hideNoticeStat;      // 도움말 숨김 체크
        ShowTips();
    }
    void Start()
    {
        Time.timeScale = 1f;
        InvokeRepeating("SpawnCastleSpider", 0, 1);     // 주민 거미 배회 연출 1
        InvokeRepeating("SpawnCastleSpider2", 1, 2);   // 주민 거미 배회 연출 2
    }
    void Update()
    {        
    }
    public void OnClickDungeon()        // 던전메뉴 클릭
    {
        dungeonUi.SetActive(true);
    }
    public void OnClickLab()                // 연구실 메뉴 클릭
    {
        enhanceMenu.CallMenu();
        enhanceMenu.OnClickLabTab();
    }
    public void OnClickEnhancement()            // 강화 메뉴 클릭
    {
        enhanceMenu.CallMenu();
        enhanceMenu.OnClickEnhanceTab();
    }
    public void OnClickCobweb()             // 거미숙소 메뉴 클릭
    {
        enhanceMenu.CallMenu();
        enhanceMenu.OnClickCobTab();
    }
    public void OnClickExit()
    {
        enhanceMenu.CloseMenu();
    }
    private void ShowTips()                 // 도움말 ON/OFF
    {
        if (hideHelp == true)
        {
            helpNotice.SetActive(false);

        }else if (hideHelp == false)
        {
            OnClickShowCob();
        }
    }
    public void OnClickShowCob()        // 거미숙소 탭
    {
        helpNotice.SetActive(true);
        cobTip.SetActive(true);
        labTip.SetActive(false);
        enhanceTip.SetActive(false);
        dungeonTip.SetActive(false);
    }
    public void OnClickShowLab()        // 연구실 탭
    {
        cobTip.SetActive(false);
        labTip.SetActive(true);
        enhanceTip.SetActive(false);
        dungeonTip.SetActive(false);
    }
    public void OnClickShowEnhance()            // 스킬강화 탭
    {
        cobTip.SetActive(false);
        labTip.SetActive(false);
        enhanceTip.SetActive(true);
        dungeonTip.SetActive(false);
    }
    public void OnClickShowDungeon()            // 던전 탭
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
    public void SpawnCastleSpider()     // 주민거미 마을 배회 메소드 1
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
    public void SpawnCastleSpider2()    // 주민거미 마을 배회 메소드 2
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