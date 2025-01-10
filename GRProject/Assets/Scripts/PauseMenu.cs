using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class PauseMenu : MonoBehaviour                  // �Ͻ� ���� �޴�
{
    [SerializeField]    private GameObject go_BaseUi;
    private void Update() // Ŭ�����̺�Ʈ���³� �÷��̾ ���� ���°� �ƴҶ��� ����
    {
        if ((PlayerController.isClear == false) && (HealthGauge.isDie == false))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!PlayerController.isPause)
                    CallMenu();
                else
                    CloseMenu();
            }
        }
    }
    public void CallMenu()                      // �Ͻ����� �޴� Ȱ��ȭ
    {
        PlayerController.isPause = true;
        go_BaseUi.SetActive(true);
        Time.timeScale = 0f;
    }
    private void CloseMenu()                    // �Ͻ����� �޴� ��Ȱ��ȭ
    {
        PlayerController.isPause = false;
        go_BaseUi.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ClickResume()                   // �������� ���ư��� ��ư
    {
        CloseMenu();
    }
    public void SaveData()
    {
    }
    public void OnClickVillage()                            // ������ ���� ��ư
    {
        SceneManager.LoadScene("StartGame");
    }
    public void ClickQuit()                                     // ���θ޴��� ���� ��ư
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ClickExit()                                     // ���� ����
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}
