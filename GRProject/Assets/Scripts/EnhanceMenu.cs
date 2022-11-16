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

    [Header("½ºÅ³")]
    [SerializeField] private GameObject Skill1;
    [SerializeField] private GameObject Skill2;
    [SerializeField] private GameObject Skill3;
    [SerializeField] private GameObject Skill4;
    [SerializeField] private GameObject Skill5;
    [SerializeField] private GameObject Skill6;

    public Transform buttonScale;
    Vector3 defaultScale;
    private void Start()
    {
        defaultScale = buttonScale.localScale;
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
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.1f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}