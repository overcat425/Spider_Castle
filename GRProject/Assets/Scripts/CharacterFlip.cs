using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlip : MonoBehaviour
{
    public Animator animator;                   // ���/�ȱ� �ִϸ�����
    Rigidbody2D rigidBody;
    private void Start()
    {
        animator = GetComponent<Animator>();        // �ִϸ����� ���� �ޱ�
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
        Vector3 CharacterFlip = Vector3.zero;                       // �¿� �̵���
        if (Input.GetAxisRaw("Horizontal") < 0)                     // ĳ���� �¿����
        {
            CharacterFlip = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("run", true);                // ����Ű �ϳ��� �Է¹�����
        }                                                         // ��⿡�� �ȱ� ������� ��ȯ
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
