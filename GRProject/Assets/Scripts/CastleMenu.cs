using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CastleMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public EnhanceMenu enhanceMenu;
    public Transform buttonScale;           // 버튼 크기
    //private AudioSource audioSource;                // 버튼 효과음
    //[SerializeField]
    //private AudioClip audioClip;            // 효과음 클립
    Vector3 defaultScale;                       // 버튼 크기 벡터
    [SerializeField]
    private GameObject helpNotice;
    public bool hideHelp;
    void Start()
    {
        HealthGauge.canAutoSave = false;
        defaultScale = buttonScale.localScale;
        hideHelp = SaveManager.hideNoticeInstance;
        if (hideHelp == true)
        {
            helpNotice.SetActive(false);
        }
    }
    void Update()
    {

    }
    public void OnClickDungeon()                            // 뉴게임씬 시작
    {
        Debug.Log("던전");
        SceneManager.LoadScene("Stage1");
        //audioSource.GetComponent<AudioSource>(); //audioSource.Play();
    }
    public void OnClickLab()
    {
        Debug.Log("연구소");
        enhanceMenu.CallMenu();
        enhanceMenu.OnClickLabTab();
    }
    public void OnClickEnhancement()
    {
        Debug.Log("강화소");
        enhanceMenu.CallMenu();
        enhanceMenu.OnClickEnhanceTab();
    }
    public void OnClickCobweb()
    {
        Debug.Log("거미 숙소");
        enhanceMenu.CallMenu();
        enhanceMenu.OnClickCobTab();
    }
    public void OnClickExit()
    {
        enhanceMenu.CloseMenu();
    }
    public void OnClickHideoff()
    {
        helpNotice.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;                   // 버튼 크기 1.2배
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;                           // 1.2배에서 돌아오기
    }
}