using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class PauseMenu : MonoBehaviour                  // 일시 정지 메뉴
{
    [SerializeField]    private GameObject go_BaseUi;
    private void Update() // 클리어이벤트상태나 플레이어가 죽은 상태가 아닐때만 실행
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
    public void CallMenu()                      // 일시정지 메뉴 활성화
    {
        PlayerController.isPause = true;
        go_BaseUi.SetActive(true);
        Time.timeScale = 0f;
    }
    private void CloseMenu()                    // 일시정지 메뉴 비활성화
    {
        PlayerController.isPause = false;
        go_BaseUi.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ClickResume()                   // 게임으로 돌아가기 버튼
    {
        CloseMenu();
    }
    public void SaveData()
    {
    }
    public void OnClickVillage()                            // 마을로 가기 버튼
    {
        SceneManager.LoadScene("StartGame");
    }
    public void ClickQuit()                                     // 메인메뉴로 가기 버튼
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ClickExit()                                     // 게임 종료
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}
