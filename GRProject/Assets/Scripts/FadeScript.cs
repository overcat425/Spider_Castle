using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour                 // 게임오버 붉은화면 페이드
{
    public Image Panel;
    float time = 0f;
    float F_time = 1f;
    public void Awake()
    {
        StartCoroutine(FadeFlow());
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        yield return null;
    }
}
