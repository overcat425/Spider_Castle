using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnSizeController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform buttonScale;           // ��ư ũ��
    //private AudioSource audioSource;                // ��ư ȿ����
    Vector3 defaultScale;                       // ��ư ũ�� ����
    void Start()
    {
        defaultScale = buttonScale.localScale;
    }
    void Update()
    {
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.1f;                   // ��ư ũ�� 1.2��
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;                           // 1.2�迡�� ���ƿ���
    }
}