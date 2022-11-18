using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnhanceMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]    private GameObject EnhanceUi;
    [SerializeField]    private GameObject LabTab;
    [SerializeField]    private GameObject EnhanceTab;
    [SerializeField]    private GameObject CobTab;
    [SerializeField]    private GameObject Summary;
    [SerializeField]    private GameObject SpiderInfo;

    [Header("스킬")]
    [SerializeField]    private GameObject Skill1;
    [SerializeField]    private GameObject Skill2;
    [SerializeField]    private GameObject Skill3;
    [SerializeField]    private GameObject Skill4;
    [SerializeField]    private GameObject Skill5;
    [SerializeField]    private GameObject Skill6;

    [Header("거미종류")]
    [SerializeField]    private GameObject Spider1;
    [SerializeField]    private GameObject Spider2;
    [SerializeField]    private GameObject Spider3;
    [SerializeField]    private GameObject Spider4;

    public Transform buttonScale;
    Vector3 defaultScale;
    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }
    private void Update()
    {
    }
    public void CallMenu()
    {
        //PlayerController.isPause = true;
        EnhanceUi.SetActive(true);
    }
    public void CloseMenu()
    {
        //PlayerController.isPause = false;
        EnhanceUi.SetActive(false);
    }
    public void ClickExit()
    {
        PlayerController.isPause = false;
        EnhanceUi.SetActive(false);
    }
    public void OnClickLabTab()
    {
        LabTab.SetActive(true);
        EnhanceTab.SetActive(false);
        CobTab.SetActive(false);

    }
    public void OnClickEnhanceTab()
    {
        LabTab.SetActive(false);
        EnhanceTab.SetActive(true);
        CobTab.SetActive(false);
        Summary.SetActive(false);
    }
    public void OnClickCobTab()
    {
        LabTab.SetActive(false);
        EnhanceTab.SetActive(false);
        CobTab.SetActive(true);
        SpiderInfo.SetActive(false);
    }
    public void OnClickSkill1Icon()
    {
        Summary.SetActive(true);
        Skill1.SetActive(true);
        Skill2.SetActive(false);
        Skill3.SetActive(false);
        Skill4.SetActive(false);
        Skill5.SetActive(false);
        Skill6.SetActive(false);
    }
    public void OnClickSkill2Icon()
    {
        Summary.SetActive(true);
        Skill2.SetActive(true);
        Skill1.SetActive(false);
        Skill3.SetActive(false);
        Skill4.SetActive(false);
        Skill5.SetActive(false);
        Skill6.SetActive(false);
    }
    public void OnClickSkill3Icon()
    {
        Summary.SetActive(true);
        Skill3.SetActive(true);
        Skill1.SetActive(false);
        Skill2.SetActive(false);
        Skill4.SetActive(false);
        Skill5.SetActive(false);
        Skill6.SetActive(false);
    }
    public void OnClickSkill4Icon()
    {
        Summary.SetActive(true);
        Skill4.SetActive(true);
        Skill1.SetActive(false);
        Skill2.SetActive(false);
        Skill3.SetActive(false);
        Skill5.SetActive(false);
        Skill6.SetActive(false);
    }
    public void OnClickSkill5Icon()
    {
        Summary.SetActive(true);
        Skill5.SetActive(true);
        Skill1.SetActive(false);
        Skill2.SetActive(false);
        Skill3.SetActive(false);
        Skill4.SetActive(false);
        Skill6.SetActive(false);
    }
    public void OnClickSkill6Icon()
    {
        Summary.SetActive(true);
        Skill6.SetActive(true);
        Skill1.SetActive(false);
        Skill2.SetActive(false);
        Skill3.SetActive(false);
        Skill4.SetActive(false);
        Skill5.SetActive(false);
    }
    public void OnClickSpider1Icon()
    {
        SpiderInfo.SetActive(true);
        Spider1.SetActive(true);
        Spider2.SetActive(false);
        Spider3.SetActive(false);
        Spider4.SetActive(false);
    }
    public void OnClickSpider2Icon()
    {
        SpiderInfo.SetActive(true);
        Spider2.SetActive(true);
        Spider1.SetActive(false);
        Spider3.SetActive(false);
        Spider4.SetActive(false);
    }
    public void OnClickSpider3Icon()
    {
        SpiderInfo.SetActive(true);
        Spider3.SetActive(true);
        Spider1.SetActive(false);
        Spider2.SetActive(false);
        Spider4.SetActive(false);
    }
    public void OnClickSpider4Icon()
    {
        SpiderInfo.SetActive(true);
        Spider4.SetActive(true);
        Spider1.SetActive(false);
        Spider2.SetActive(false);
        Spider3.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.1f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}