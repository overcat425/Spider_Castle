using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiGetBigger : MonoBehaviour
{
    float time = 0;
    float time2 = 0;
    public float size = 5;
    public float upSizeTime = 3f;
    void Start()
    {
    }
    void Update()
    {
        if(time <= 0.5f)
        {
            transform.localScale = Vector3.one * ( time*2);
        }
        time += Time.deltaTime;
    }
}
