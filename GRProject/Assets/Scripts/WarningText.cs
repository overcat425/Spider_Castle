using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WarningText : MonoBehaviour
{
    Text text;
    void Awake()
    {
        text = GetComponent<Text>();
    }
    private void Start()
    {
        StartCoroutine("FadeTextToZero");
    }
    private IEnumerator FadeTextToZero()  // 알파값 1에서 0으로 전환
    {
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime));
            yield return null;
        }
    }
}
