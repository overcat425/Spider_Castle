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
        Vector3 CharacterFlip = Vector3.zero;                       // �¿� �̵���
        if (Input.GetAxisRaw("Horizontal") < 0)                     // ĳ���� �¿����
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
