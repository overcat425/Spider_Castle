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
    //[SerializeField]
    //private AudioClip audioClip;            // ȿ���� Ŭ��
    Vector3 defaultScale;                       // ��ư ũ�� ����
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;             // Ŀ�� �߾ӿ� ����
        Cursor.visible = true;                                         // Ŀ�� �Ⱥ��̰�
        defaultScale = buttonScale.localScale;
    }
    void Update()
    {    
    }
    public void OnClickNewGame()                            // �����Ӿ� ����
    {
        Debug.Log("���ӽ���");
        //audioSource.GetComponent<AudioSource>();
        //audioSource.Play();
    }
    public void OnClickLoadGame()
    {
        Debug.Log("���� �̾��ϱ�");
    }
    public void OnClickQuit()
    {
        Debug.Log("����");
#if UNITY_EDITOR
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