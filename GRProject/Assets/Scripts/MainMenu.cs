using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform buttonScale;           // 버튼 크기
    private AudioSource audioSource;                // 버튼 효과음
    Vector3 defaultScale;                       // 버튼 크기 벡터
    void Start()
    {
        defaultScale = buttonScale.localScale;                      // 버튼 스케일
    }
    public void OnClickQuit()
    {
        Debug.Log("종료");
#if UNITY_EDITOR                    // 에디터용 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
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