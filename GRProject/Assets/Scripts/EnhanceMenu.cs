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

    [Header("Lab≈« Ω∫≈≥")]
    [SerializeField]    private GameObject skill2LabPane;
    [SerializeField]    private GameObject skill3LabPane;
    [SerializeField]    private GameObject skill4LabPane;

    [SerializeField]    private GameObject skill1LabLv1Pane;
    [SerializeField]    private GameObject skill1LabLv2Pane;
    [SerializeField]    private GameObject skill1LabLv3Pane;
    [SerializeField]    private GameObject skill1LabLv4Pane;

    [SerializeField]    private GameObject skill2LabLv1Pane;
    [SerializeField]    private GameObject skill2LabLv2Pane;
    [SerializeField]    private GameObject skill2LabLv3Pane;
    [SerializeField]    private GameObject skill2LabLv4Pane;

    [SerializeField]    private GameObject skill3LabLv1Pane;
    [SerializeField]    private GameObject skill3LabLv2Pane;
    [SerializeField]    private GameObject skill3LabLv3Pane;
    [SerializeField]    private GameObject skill3LabLv4Pane;

    [Header("Enhance≈« Ω∫≈≥")]
    [SerializeField]    private GameObject skill0;
    [SerializeField]    private GameObject skill1;
    [SerializeField]    private GameObject skill2;
    [SerializeField]    private GameObject skill3;
    [SerializeField]    private GameObject skill4;
    [SerializeField]    private GameObject skill5;

    [SerializeField]    private GameObject skill3Pane;
    [SerializeField]    private GameObject skill4Pane;
    [SerializeField]    private GameObject skill5Pane;

    [Header("Cob≈« ∞≈πÃ¡æ∑˘")]
    [SerializeField]    private GameObject spider1;
    [SerializeField]    private GameObject spider2;
    [SerializeField]    private GameObject spider3;
    [SerializeField]    private GameObject spider4;

    [SerializeField]    private GameObject spider2Pane;
    [SerializeField]    private GameObject spider3Pane;
    [SerializeField]    private GameObject spider4Pane;

    public bool skill3Enable;
    public bool skill4Enable;
    public bool skill5Enable;

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
        skill5Enable = SaveManager.skill5EnableInstance;
        EnableSkills();
        EnableSpiders();
        Skill1LabPane();
        Skill2LabPane();
        Skill3LabPane();
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
        labTab.SetActive(false);
        enhanceTab.SetActive(true);
        cobTab.SetActive(false);
        summary.SetActive(false);
    }
    public void OnClickCobTab()
    {
        labTab.SetActive(false);
        enhanceTab.SetActive(false);
        cobTab.SetActive(true);
        spiderInfo.SetActive(false);
    }
    public void Skill1LabPane()
    {
        int skill1LabLv = SaveManager.skill1LabLvInstance;
        if (skill1LabLv == 0)
        {
            skill1LabLv1Pane.SetActive(false);
            skill1LabLv2Pane.SetActive(false);
            skill1LabLv3Pane.SetActive(false);
            skill1LabLv4Pane.SetActive(false);
        }
        else if (skill1LabLv == 1)
        {
            skill1LabLv1Pane.SetActive(true);
            skill1LabLv2Pane.SetActive(false);
            skill1LabLv3Pane.SetActive(false);
            skill1LabLv4Pane.SetActive(false);
        }
        else if (skill1LabLv == 2)
        {
            skill1LabLv1Pane.SetActive(true);
            skill1LabLv2Pane.SetActive(true);
            skill1LabLv3Pane.SetActive(false);
            skill1LabLv4Pane.SetActive(false);
        }
        else if (skill1LabLv == 3)
        {
            skill1LabLv1Pane.SetActive(true);
            skill1LabLv2Pane.SetActive(true);
            skill1LabLv3Pane.SetActive(true);
            skill1LabLv4Pane.SetActive(false);
        }
        else if (skill1LabLv == 4)
        {
            skill1LabLv1Pane.SetActive(true);
            skill1LabLv2Pane.SetActive(true);
            skill1LabLv3Pane.SetActive(true);
            skill1LabLv4Pane.SetActive(true);
        }
    }
    public void Skill2LabPane()
    {
        int skill2LabLv = SaveManager.skill2LabLvInstance;
        if (skill2LabLv == 0)
        {
            skill2LabLv1Pane.SetActive(false);
            skill2LabLv2Pane.SetActive(false);
            skill2LabLv3Pane.SetActive(false);
            skill2LabLv4Pane.SetActive(false);
        }
        else if (skill2LabLv == 1)
        {
            skill2LabLv1Pane.SetActive(true);
            skill2LabLv2Pane.SetActive(false);
            skill2LabLv3Pane.SetActive(false);
            skill2LabLv4Pane.SetActive(false);
        }
        else if (skill2LabLv == 2)
        {
            skill2LabLv1Pane.SetActive(true);
            skill2LabLv2Pane.SetActive(true);
            skill2LabLv3Pane.SetActive(false);
            skill2LabLv4Pane.SetActive(false);
        }
        else if (skill2LabLv == 3)
        {
            skill2LabLv1Pane.SetActive(true);
            skill2LabLv2Pane.SetActive(true);
            skill2LabLv3Pane.SetActive(true);
            skill2LabLv4Pane.SetActive(false);
        }
        else if (skill2LabLv == 4)
        {
            skill2LabLv1Pane.SetActive(true);
            skill2LabLv2Pane.SetActive(true);
            skill2LabLv3Pane.SetActive(true);
            skill2LabLv4Pane.SetActive(true);
        }
    }
    public void Skill3LabPane()
    {
        int skill3LabLv = SaveManager.skill3LabLvInstance;
        if (skill3LabLv == 0)
        {
            skill3LabLv1Pane.SetActive(false);
            skill3LabLv2Pane.SetActive(false);
            skill3LabLv3Pane.SetActive(false);
            skill3LabLv4Pane.SetActive(false);
        }else if (skill3LabLv == 1)
        {
            skill3LabLv1Pane.SetActive(true);
            skill3LabLv2Pane.SetActive(false);
            skill3LabLv3Pane.SetActive(false);
            skill3LabLv4Pane.SetActive(false);
        }
        else if (skill3LabLv == 2)
        {
            skill3LabLv1Pane.SetActive(true);
            skill3LabLv2Pane.SetActive(true);
            skill3LabLv3Pane.SetActive(false);
            skill3LabLv4Pane.SetActive(false);
        }
        else if (skill3LabLv == 3)
        {
            skill3LabLv1Pane.SetActive(true);
            skill3LabLv2Pane.SetActive(true);
            skill3LabLv3Pane.SetActive(true);
            skill3LabLv4Pane.SetActive(false);
        }
        else if (skill3LabLv == 4)
        {
            skill3LabLv1Pane.SetActive(true);
            skill3LabLv2Pane.SetActive(true);
            skill3LabLv3Pane.SetActive(true);
            skill3LabLv4Pane.SetActive(true);
        }
    }
    public void OnClickSkill0Icon()
    {
        summary.SetActive(true);
        skill0.SetActive(true);
        skill1.SetActive(false);
        skill2.SetActive(false);
        skill3.SetActive(false);
        skill4.SetActive(false);
        skill5.SetActive(false);
    }
    public void OnClickSkill1Icon()
    {
        summary.SetActive(true);
        skill0.SetActive(false);
        skill1.SetActive(true);
        skill2.SetActive(false);
        skill3.SetActive(false);
        skill4.SetActive(false);
        skill5.SetActive(false);

    }
    public void OnClickSkill2Icon()
    {
        summary.SetActive(true);
        skill2.SetActive(true);
        skill0.SetActive(false);
        skill1.SetActive(false);
        skill3.SetActive(false);
        skill4.SetActive(false);
        skill5.SetActive(false);
    }
    public void OnClickSkill3Icon()
    {
        summary.SetActive(true);
        skill3.SetActive(true);
        skill0.SetActive(false);
        skill1.SetActive(false);
        skill2.SetActive(false);
        skill4.SetActive(false);
        skill5.SetActive(false);

    }
    public void OnClickSkill4Icon()
    {
        summary.SetActive(true);
        skill4.SetActive(true);
        skill0.SetActive(false);
        skill1.SetActive(false);
        skill2.SetActive(false);
        skill3.SetActive(false);
        skill5.SetActive(false);
    }
    public void OnClickSkill5Icon()
    {
        summary.SetActive(true);
        skill5.SetActive(true);
        skill0.SetActive(false);
        skill1.SetActive(false);
        skill2.SetActive(false);
        skill3.SetActive(false);
        skill4.SetActive(false);
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
    public void EnableSkills()
    {
        if (skill3Enable == true)
        {
            skill3Pane.SetActive(true);
            skill2LabPane.SetActive(true);
        }
        else if (skill3Enable == false)
        {
            skill3Pane.SetActive(false);
            skill2LabPane.SetActive(false);
        }
        if (skill4Enable == true)
        {
            skill4Pane.SetActive(true);
            skill3LabPane.SetActive(true);
        }
        else if (skill4Enable == false)
        {
            skill4Pane.SetActive(false);
            skill3LabPane.SetActive(false);
        }
        if (skill5Enable == true)
        {
            skill5Pane.SetActive(true);
            skill4LabPane.SetActive(true);
        }
        else if (skill5Enable == false)
        {
            skill5Pane.SetActive(false);
            skill4LabPane.SetActive(false);
        }
    }
    public void EnableSpiders()
    {
        if (skill3Enable == true)
        {
            spider2Pane.SetActive(true);
        }
        else if (skill3Enable == false)
        {
            spider2Pane.SetActive(false);
        }
        if (skill4Enable == true)
        {
            spider3Pane.SetActive(true);
        }
        else if (skill4Enable == false)
        {
            spider3Pane.SetActive(false);
        }
        if (skill5Enable == true)
        {
            spider4Pane.SetActive(true);
        }
        else if (skill5Enable == false)
        {
            spider4Pane.SetActive(false);
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