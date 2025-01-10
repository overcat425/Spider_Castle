using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkTextScript : MonoBehaviour        // 게임오버 깜빡이는 텍스트
{
    float blinkTime;
    void Update()
    {
        blinkTime += Time.deltaTime;
        if (blinkTime < 0.5f)
        {
            GetComponent<TextMeshPro>().color = new Color(1, 1, 1, 0);
        }
        else
        {
            GetComponent<TextMeshPro>().color = new Color(1, 1, 1, 1);
            if(blinkTime > 1f)
            {
                blinkTime = 0;
            }
        }
    }
}
