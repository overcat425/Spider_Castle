using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CastleMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform buttonScale;           // 버튼 크기
    //private AudioSource audioSource;                // 버튼 효과음
    //[SerializeField]
    //private AudioClip audioClip;            // 효과음 클립
    Vector3 defaultScale;                       // 버튼 크기 벡터
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;             // 커서 중앙에 고정
        Cursor.visible = true;                                         // 커서 안보이게
        defaultScale = buttonScale.localScale;
    }
    void Update()
    {
    }
    public void OnClickDungeon()                            // 뉴게임씬 시작
    {
        Debug.Log("던전");
        SceneManager.LoadScene("Stage1");
        //audioSource.GetComponent<AudioSource>();
        //audioSource.Play();
    }
    public void OnClickEnhancement()
    {
        Debug.Log("강화소");
    }
    public void OnClickLab()
    {
        Debug.Log("연구소");
    }
    public void OnClickCobweb()
    {
        Debug.Log("거미 숙소");
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