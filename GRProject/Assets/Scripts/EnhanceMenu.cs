using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnhanceMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]    private GameObject enhanceUi;
    [SerializeField]    private GameObject labTab;
    [SerializeField]    private GameObject enhanceTab;
    [SerializeField]    private GameObject cobTab;
    [SerializeField]    private GameObject summary;
    [SerializeField]    private GameObject spiderInfo;

    [Header("스킬")]
    [SerializeField]    private GameObject skill1;
    [SerializeField]    private GameObject skill2;
    [SerializeField]    private GameObject skill3;
    [SerializeField]    private GameObject skill4;
    [SerializeField]    private GameObject skill5;
    [SerializeField]    private GameObject skill6;
    [SerializeField]    private GameObject skill3Pane;
    [SerializeField]    private GameObject skill4Pane;

    [Header("거미종류")]
    [SerializeField]    private GameObject spider1;
    [SerializeField]    private GameObject spider2;
    [SerializeField]    private GameObject spider3;
    [SerializeField]    private GameObject spider4;

    public bool skill3Enable;
    public bool skill4Enable;

    public Transform buttonScale;
    Vector3 defaultScale;
    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }
    private void Update()
    {
        skill3Enable = SaveManager.skill3EnableInstance;
        skill4Enable = SaveManager.skill4EnableInstance;
    }
    public void CallMenu()
    {
        //PlayerController.isPause = true;
        enhanceUi.SetActive(true);
    }
    public void CloseMenu()
    {
        //PlayerController.isPause = false;
        enhanceUi.SetActive(false);
    }
    public void ClickExit()
    {
        PlayerController.isPause = false;
        enhanceUi.SetActive(false);
    }
    public void OnClickLabTab()
    {
        labTab.SetActive(true);
        enhanceTab.SetActive(false);
        cobTab.SetActive(false);

    }
    public void OnClickEnhanceTab()
    {
        Debug.Log(skill3Enable);
        labTab.SetActive(false);
        enhanceTab.SetActive(true);
        cobTab.SetActive(false);
        summary.SetActive(false);
        EnableSkill3();
        EnableSkill4();
    }
    public void OnClickCobTab()
    {
        labTab.SetActive(false);
        enhanceTab.SetActive(false);
        cobTab.SetActive(true);
        spiderInfo.SetActive(false);
    }
    public void OnClickSkill1Icon()
    {
        summary.SetActive(true);
        skill1.SetActive(true);
        skill2.SetActive(false);
        skill3.SetActive(false);
        skill4.SetActive(false);
        skill5.SetActive(false);
        skill6.SetActive(false);
    }
    public void OnClickSkill2Icon()
    {
        summary.SetActive(true);
        skill2.SetActive(true);
        skill1.SetActive(false);
        skill3.SetActive(false);
        skill4.SetActive(false);
        skill5.SetActive(false);
        skill6.SetActive(false);
    }
    public void OnClickSkill3Icon()
    {
        summary.SetActive(true);
        skill3.SetActive(true);
        skill1.SetActive(false);
        skill2.SetActive(false);
        skill4.SetActive(false);
        skill5.SetActive(false);
        skill6.SetActive(false);
    }
    public void OnClickSkill4Icon()
    {
        summary.SetActive(true);
        skill4.SetActive(true);
        skill1.SetActive(false);
        skill2.SetActive(false);
        skill3.SetActive(false);
        skill5.SetActive(false);
        skill6.SetActive(false);
    }
    public void OnClickSkill5Icon()
    {
        summary.SetActive(true);
        skill5.SetActive(true);
        skill1.SetActive(false);
        skill2.SetActive(false);
        skill3.SetActive(false);
        skill4.SetActive(false);
        skill6.SetActive(false);
    }
    public void OnClickSkill6Icon()
    {
        summary.SetActive(true);
        skill6.SetActive(true);
        skill1.SetActive(false);
        skill2.SetActive(false);
        skill3.SetActive(false);
        skill4.SetActive(false);
        skill5.SetActive(false);
    }
    public void OnClickSpider1Icon()
    {
        spiderInfo.SetActive(true);
        spider1.SetActive(true);
        spider2.SetActive(false);
        spider3.SetActive(false);
        spider4.SetActive(false);
    }
    public void OnClickSpider2Icon()
    {
        spiderInfo.SetActive(true);
        spider2.SetActive(true);
        spider1.SetActive(false);
        spider3.SetActive(false);
        spider4.SetActive(false);
    }
    public void OnClickSpider3Icon()
    {
        spiderInfo.SetActive(true);
        spider3.SetActive(true);
        spider1.SetActive(false);
        spider2.SetActive(false);
        spider4.SetActive(false);
    }
    public void OnClickSpider4Icon()
    {
        spiderInfo.SetActive(true);
        spider4.SetActive(true);
        spider1.SetActive(false);
        spider2.SetActive(false);
        spider3.SetActive(false);
    }
    public void EnableSkill3()
    {
        if (skill3Enable == true)
        {
            skill3Pane.SetActive(true);
        }
        else if (skill3Enable == false)
        {
            skill3Pane.SetActive(false);
        }
    }
    public void EnableSkill4()
    {
        if (skill4Enable == true)
        {
            skill4Pane.SetActive(true);
        }
        else if (skill4Enable == false)
        {
            skill4Pane.SetActive(false);
        }
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