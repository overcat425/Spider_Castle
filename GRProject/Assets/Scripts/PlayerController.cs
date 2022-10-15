using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 200.0f;                         // 이동 속도
    private Vector3 moveDirection = Vector3.zero;      // 이동 벡터
    private CharacterMovement movement;       // 키보드로 플레이어 이동
    private EnemyStatus enemyStatus;
    [SerializeField]
    private GameObject go_BaseUi;

    public static bool canPlayerMove = false;
    public static bool isPause = false;

    [SerializeField]
    private SpriteRenderer baseAttackEffect;            // 기본공격 이펙트 스프라이트
    [SerializeField]
    private AnimationCurve curveAttackEffect;       // 기본공격 이펙트 커브모션
    public Transform pos;
    public Vector2 boxSize;

    private void Start()
    {
        StartCoroutine("BaseAttack");
    }
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
            ColliderBox();
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    private IEnumerator BaseAttack()                // 기본공격 코루틴메소드
    {
        yield return new WaitForSeconds(2);         // 2초 대기시간
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<EnemyStatus>().DestroyEnemy();    // 적 파괴
            }
        }
        StartCoroutine("BaseAttackEffect"); // 기본공격 이펙트 코루틴메소드 반복
        StartCoroutine("BaseAttack");       // 기본공격 코루틴메소드 반복
    }
    private IEnumerator BaseAttackEffect()          // 기본공격 이펙트 코루틴메소드
    {
        float percent = 0;
        while (percent < 1)                     // 이펙트 fadeout
        {
            percent += Time.deltaTime/0.5f;         // 2초동안 출력
            Color color = baseAttackEffect.color;
            color.a = Mathf.Lerp(1f, 0, curveAttackEffect.Evaluate(percent));
            baseAttackEffect.color = color;
            InfiniteLoopDetector.Run();
            yield return null;
        }
    }
    public void ColliderBox()         // 공격 판정 방향 박스 (이것만 2일 걸림 ㅡㅡ)
    {                                          // 콜라이더와 공격이펙트 방향/위치 조절
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            pos.localPosition = new Vector3(2.5f, 0, 0);
            pos.localRotation = Quaternion.Euler(0, 0, 0);
            boxSize = new Vector2(150, 300);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            pos.localPosition = new Vector3(2.5f, 0, 0);
            pos.localRotation = Quaternion.Euler(0, 0, 0);
            boxSize = new Vector2(150, 300);
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            pos.localPosition = new Vector3(0, 2.5f, 0);
            pos.localRotation = Quaternion.Euler(0, 0, 90);
            boxSize = new Vector2(300, 150);
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            pos.localPosition = new Vector3(0, -2.5f, 0);
            pos.localRotation = Quaternion.Euler(0, 0, -90);
            boxSize = new Vector2(300, 150);
        }
    }
}