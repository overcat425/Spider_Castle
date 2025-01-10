using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform buttonScale;           // ��ư ũ��
    private AudioSource audioSource;                // ��ư ȿ����
    Vector3 defaultScale;                       // ��ư ũ�� ����
    void Start()
    {
        defaultScale = buttonScale.localScale;                      // ��ư ������
    }
    public void OnClickQuit()
    {
        Debug.Log("����");
#if UNITY_EDITOR                    // �����Ϳ� ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;                   // ��ư ũ�� 1.2��
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;                           // 1.2�迡�� ���ƿ���
    }
}