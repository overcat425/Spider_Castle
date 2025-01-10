using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnhanceMenu : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]    private GameObject enhanceUi;           // ��ȭ UI
    [SerializeField]    private GameObject labTab;              // ������ ��
    [SerializeField]    private GameObject enhanceTab;       // ��ȭ ��
    [SerializeField]    private GameObject cobTab;              // �Ź̼��� ��
    [SerializeField]    private GameObject summary;         // ��ų���� ���
    [SerializeField]    private GameObject spiderInfo;          // �Ź� ����

    [Header("Lab�� ��ų")]
    [SerializeField]    private GameObject[] skillLabPane;      // Lv 2,3,4

    [SerializeField]    private GameObject[] skill1LabLvPane;   // Lv1234
    [SerializeField]    private GameObject[] skill2LabLvPane;
    [SerializeField]    private GameObject[] skill3LabLvPane;
    [SerializeField]    private GameObject[] skill4LabLvPane;

    [Header("Enhance�� ��ų")]
    [SerializeField]    private GameObject[] skills;

    [SerializeField]    private GameObject[] skillPane; // Lv 3,4,5

    [Header("Cob�� �Ź�����")]
    [SerializeField]    private GameObject[] spiders;   // 1234

    [SerializeField]    private GameObject spider2Pane;
    [SerializeField]    private GameObject spider3Pane;
    [SerializeField]    private GameObject spider4Pane;

    public bool skill3Enable;
    public bool skill4Enable;
    public bool skill5Enable;

    //public Transform buttonScale;
    //Vector3 defaultScale;
    private void Start()
    {
        StartCoroutine("SkillLabPaneUpdate");
    }
    private void Update()
    {
        skill3Enable = SaveManager.skillEnableStat[0];
        skill4Enable = SaveManager.skillEnableStat[1];
        skill5Enable = SaveManager.skillEnableStat[2];
        EnableSkills();
        EnableSpiders();
    }
    public void CallMenu()
    {
        enhanceUi.SetActive(true);
    }
    public void CloseMenu()
    {
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
        Skill1LabPane();
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
    IEnumerator SkillLabPaneUpdate()        // �ǽð� ���������� ������Ʈ
    {
        while (true)
        {
            Skill1LabPane();
            Skill2LabPane();
            Skill3LabPane();
            Skill4LabPane();
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void Skill1LabPane()                 // 2����ų ���� ī�װ�
    {
        int skill1LabLv = SaveManager.instance.skillLabLvStat[0];
        for (int i = 0; i < 4; i++)
        {
            skill1LabLvPane[i].SetActive(false);
        }
        switch (skill1LabLv)
        {
            case 1:
                skill1LabLvPane[0].SetActive(true);
                break;
            case 2:
                skill1LabLvPane[1].SetActive(true);
                break;
            case 3:
                skill1LabLvPane[2].SetActive(true);
                break;
            case 4:
                skill1LabLvPane[2].SetActive(true);
                skill1LabLvPane[3].SetActive(true);
                break;
        }
    }
    public void Skill2LabPane()                 // 3����ų ���� ī�װ�
    {
        int skill2LabLv = SaveManager.instance.skillLabLvStat[1];
        for (int i = 0; i < 4; i++)
        {
            skill2LabLvPane[i].SetActive(false);
        }
        switch (skill2LabLv)
        {
            case 1:
                skill2LabLvPane[0].SetActive(true);
                break;
            case 2:
                skill2LabLvPane[1].SetActive(true);
                break;
            case 3:
                skill2LabLvPane[2].SetActive(true);
                break;
            case 4:
                skill2LabLvPane[2].SetActive(true);
                skill2LabLvPane[3].SetActive(true);
                break;
        }
    }
    public void Skill3LabPane()                 // 4����ų ���� ī�װ�
    {
        int skill3LabLv = SaveManager.instance.skillLabLvStat[2];
        for (int i = 0; i < 4; i++)
        {
            skill3LabLvPane[i].SetActive(false);
        }
        switch (skill3LabLv)
        {
            case 1:
                skill3LabLvPane[0].SetActive(true);
                break;
            case 2:
                skill3LabLvPane[1].SetActive(true);
                break;
            case 3:
                skill3LabLvPane[2].SetActive(true);
                break;
            case 4:
                skill3LabLvPane[2].SetActive(true);
                skill3LabLvPane[3].SetActive(true);
                break;
        }
    }
    public void Skill4LabPane()                 // 5����ų ���� ī�װ�
    {
        int skill4LabLv = SaveManager.instance.skillLabLvStat[3];
        for (int i = 0; i < 4; i++)
        {
            skill4LabLvPane[i].SetActive(false);
        }
        switch (skill4LabLv)
        {
            case 1:
                skill4LabLvPane[0].SetActive(true);
                break;
            case 2:
                skill4LabLvPane[1].SetActive(true);
                break;
            case 3:
                skill4LabLvPane[2].SetActive(true);
                break;
            case 4:
                skill4LabLvPane[2].SetActive(true);
                skill4LabLvPane[3].SetActive(true);
                break;
        }
    }
    public void OnClickSkillIconFalse()
    {
        for (int i = 0; i < 6; i++)
        {
            skills[i].SetActive(false);
        }
    }
    public void OnClickSpiderIconFalse()
    {
        for (int i = 0; i < 4; i++)
        {
            spiders[i].SetActive(false);
        }
    }
    public void EnableSkills()
    {
        if (skill3Enable == true)
        {
            skillPane[0].SetActive(true);
            skillLabPane[0].SetActive(true);
        }
        else if (skill3Enable == false)
        {
            skillPane[0].SetActive(false);
            skillLabPane[0].SetActive(false);
        }
        if (skill4Enable == true)
        {
            skillPane[1].SetActive(true);
            skillLabPane[1].SetActive(true);
        }
        else if (skill4Enable == false)
        {
            skillPane[1].SetActive(false);
            skillLabPane[1].SetActive(false);
        }
        if (skill5Enable == true)
        {
            skillPane[2].SetActive(true);
            skillLabPane[2].SetActive(true);
        }
        else if (skill5Enable == false)
        {
            skillPane[2].SetActive(false);
            skillLabPane[2].SetActive(false);
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
    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    buttonScale.localScale = defaultScale * 1.1f;
    //}
    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    buttonScale.localScale = defaultScale;
    //}
}