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
        rectTransform.SetSiblingIndex(6);
        StartCoroutine("FadeIn");
    }
    // Update is called once per frame
    void Update()
    {
    }
    private IEnumerator FadeIn()
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
    private IEnumerator FadeOut()
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
