using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlip : MonoBehaviour
{
    public Animator animator;                   // 대기/걷기 애니메이터
    Rigidbody2D rigidBody;
    private void Start()
    {
        animator = GetComponent<Animator>();        // 애니메이터 컴포 받기
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        CharFlip();
        //if (Input.GetKey(KeyCode.Z))
        //{
            //Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(point, size, angle);
        //}
    }
    public void CharFlip()
    {
        Vector3 CharacterFlip = Vector3.zero;                       // 좌우 이동시
        if (Input.GetAxisRaw("Horizontal") < 0)                     // 캐릭터 좌우반전
        {
            CharacterFlip = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("run", true);                // 방향키 하나라도 입력받으면
        }                                                         // 대기에서 걷기 모션으로 변환
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            CharacterFlip = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("run", true);
        }else if(Input.GetAxisRaw("Vertical") != 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
    }
}
