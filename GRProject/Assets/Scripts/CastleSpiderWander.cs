using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleSpiderWander : MonoBehaviour
{
    [SerializeField] private Image image;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        rectTransform.SetSiblingIndex(6);           // 하이어라키 순서 설정
        StartCoroutine("FadeIn");
    }
    void Update()
    {
    }
    private IEnumerator FadeIn()            // 주민 거미 페이드인
    {
        float fadein = 0f;
        while (fadein <= 1.0f)
        {
            fadein += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(1, 1, 1, fadein);
        }
        if (fadein <= 3.0f)
        {
            StartCoroutine("FadeOut");
        }
    }
    private IEnumerator FadeOut()           // 주민 거미 페이드아웃
    {
        float fadeout = 1f;
        while (fadeout >= 0f)
        {
            fadeout -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(1, 1, 1, fadeout);
        }
        if (fadeout <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
