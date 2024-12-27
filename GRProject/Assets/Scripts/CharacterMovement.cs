using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;                                // 이동속도
    private Vector2 moveForce;                             // 이동 벡터

    private CharacterController characterController;    // 이동 제어 컴포
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        characterController.Move(moveForce * Time.deltaTime);       // 초당 이동
    }
    
    public void MoveTo(Vector2 direction)           // 플레이어 이동
    {
        direction = transform.rotation * new Vector2(direction.x, direction.y);
        
        moveForce = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }
}
