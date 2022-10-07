using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 100.0f;                         // 이동 속도
    private Vector3 moveDirection = Vector3.zero;      // 이동 벡터
    private CharacterMovement movement;       // 키보드로 플레이어 이동

    [SerializeField]
    private GameObject go_BaseUi;
    public static bool canPlayerMove = false;
    public static bool isPause = false;

    private void Awake()
    {
//        Cursor.visible = false;
//        Cursor.lockState = CursorLockMode.Locked;
        isPause = false;
        movement = GetComponent<CharacterMovement>();
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (canPlayerMove)
        {
            UpdateMove();
        }
        if (isPause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            canPlayerMove = false;
        }
        else
        {
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            canPlayerMove = true;
        }
    }
    public void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");           // 좌우 이동
        float y = Input.GetAxisRaw("Vertical");              // 상하 이동

        moveDirection = new Vector3(x, y, 0);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
