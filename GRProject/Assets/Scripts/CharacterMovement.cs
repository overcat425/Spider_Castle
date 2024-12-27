using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;                                // �̵��ӵ�
    private Vector2 moveForce;                             // �̵� ����

    private CharacterController characterController;    // �̵� ���� ����
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        characterController.Move(moveForce * Time.deltaTime);       // �ʴ� �̵�
    }
    
    public void MoveTo(Vector2 direction)           // �÷��̾� �̵�
    {
        direction = transform.rotation * new Vector2(direction.x, direction.y);
        
        moveForce = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }
}
