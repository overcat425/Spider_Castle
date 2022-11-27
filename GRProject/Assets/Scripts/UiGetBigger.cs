using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiGetBigger : MonoBehaviour
{
    float time = 0;
    float time2 = 0;
    public float size = 5;
    public float upSizeTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time <= 0.5f)
        {
            transform.localScale = Vector3.one * ( time*2);
        }
        //else if(time <= 1f)
        //{
        //    time2 += Time.deltaTime*2;
        //    transform.localScale = Vector3.one * (time*2 - time2);
        //}
        time += Time.deltaTime;
    }
}
