using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlip : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        CharFlip();
    }
    public void CharFlip()
    {
        Vector3 CharacterFlip = Vector3.zero;                       // 좌우 이동시
        if (Input.GetAxisRaw("Horizontal") < 0)                     // 캐릭터 좌우반전
        {
            CharacterFlip = Vector3.left;
            transform.localScale = new Vector3(1, -1, 1);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            CharacterFlip = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
