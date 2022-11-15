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
    }
    public void OnClickCobTab()
    {
        LabTab.SetActive(false);
        EnhanceTab.SetActive(false);
        CobTab.SetActive(true);
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