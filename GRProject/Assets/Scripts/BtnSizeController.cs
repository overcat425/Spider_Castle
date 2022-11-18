using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnSizeController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform buttonScale;           // 버튼 크기
    //private AudioSource audioSource;                // 버튼 효과음
    Vector3 defaultScale;                       // 버튼 크기 벡터
    void Start()
    {
        defaultScale = buttonScale.localScale;
    }
    void Update()
    {
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.1f;                   // 버튼 크기 1.2배
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;                           // 1.2배에서 돌아오기
    }
}