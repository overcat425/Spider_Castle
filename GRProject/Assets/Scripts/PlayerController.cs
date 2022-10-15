using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 200.0f;                         // �̵� �ӵ�
    private Vector3 moveDirection = Vector3.zero;      // �̵� ����
    private CharacterMovement movement;       // Ű����� �÷��̾� �̵�
    private EnemyStatus enemyStatus;
    [SerializeField]
    private GameObject go_BaseUi;

    public static bool canPlayerMove = false;
    public static bool isPause = false;

    [SerializeField]
    private SpriteRenderer baseAttackEffect;            // �⺻���� ����Ʈ ��������Ʈ
    [SerializeField]
    private AnimationCurve curveAttackEffect;       // �⺻���� ����Ʈ Ŀ����
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
        float x = Input.GetAxisRaw("Horizontal");           // �¿� �̵�
        float y = Input.GetAxisRaw("Vertical");              // ���� �̵�

        moveDirection = new Vector3(x, y, 0);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    private IEnumerator BaseAttack()                // �⺻���� �ڷ�ƾ�޼ҵ�
    {
        yield return new WaitForSeconds(2);         // 2�� ���ð�
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<EnemyStatus>().DestroyEnemy();    // �� �ı�
            }
        }
        StartCoroutine("BaseAttackEffect"); // �⺻���� ����Ʈ �ڷ�ƾ�޼ҵ� �ݺ�
        StartCoroutine("BaseAttack");       // �⺻���� �ڷ�ƾ�޼ҵ� �ݺ�
    }
    private IEnumerator BaseAttackEffect()          // �⺻���� ����Ʈ �ڷ�ƾ�޼ҵ�
    {
        float percent = 0;
        while (percent < 1)                     // ����Ʈ fadeout
        {
            percent += Time.deltaTime/0.5f;         // 2�ʵ��� ���
            Color color = baseAttackEffect.color;
            color.a = Mathf.Lerp(1f, 0, curveAttackEffect.Evaluate(percent));
            baseAttackEffect.color = color;
            InfiniteLoopDetector.Run();
            yield return null;
        }
    }
    public void ColliderBox()         // ���� ���� ���� �ڽ� (�̰͸� 2�� �ɸ� �Ѥ�)
    {                                          // �ݶ��̴��� ��������Ʈ ����/��ġ ����
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