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
    }
    private void Update()
    {
        //transform.Translate(new Vector3(0, Time.deltaTime* 5, 0));
        //StartCoroutine("FadeTextToZero");
    }
    private IEnumerator FadeTextToZero()  // ���İ� 1���� 0���� ��ȯ
    {
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime));
            yield return null;
        }
    }
    //public IEnumerator FadeTextToFullAlpha() // ���İ� 0���� 1�� ��ȯ
    //{
    //    text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    //    while (text.color.a < 1.0f)
    //    {
    //        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 2.0f));
    //        yield return null;
    //    }
    //}
}
