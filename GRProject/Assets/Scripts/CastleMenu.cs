using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CastleMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public EnhanceMenu enhanceMenu;
    public Transform buttonScale;           // ��ư ũ��
    //private AudioSource audioSource;                // ��ư ȿ����
    //[SerializeField]
    //private AudioClip audioClip;            // ȿ���� Ŭ��
    Vector3 defaultScale;                       // ��ư ũ�� ����
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
    public void OnClickDungeon()                            // �����Ӿ� ����
    {
        Debug.Log("����");
        SceneManager.LoadScene("Stage1");
        //audioSource.GetComponent<AudioSource>(); //audioSource.Play();
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
    public void OnClickHideoff()
    {
        helpNotice.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;                   // ��ư ũ�� 1.2��
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;                           // 1.2�迡�� ���ƿ���
    }
}