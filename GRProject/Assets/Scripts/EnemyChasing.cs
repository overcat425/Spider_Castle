using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyChasing : MonoBehaviour
{
    Rigidbody2D enemyBody;
    Transform target;
    private HealthGauge healthGauge;

    [Header("�ӵ�")]
    [SerializeField]
    private float moveSpeed = 7.5f;

    [Header("Ÿ�� �Ÿ�")]
    [SerializeField]
    private float hitscanDistance = 1f;

    [Header("BloodScreen")]
    [SerializeField]
    private Image bloodScreen;
    [SerializeField]
    private AnimationCurve curveBloodScreen;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        TargetChasing();
        MobFlip();
    }
    private void TargetChasing()                    // �÷��̾� ����
    {
        if(Vector2.Distance(transform.position, target.position) > hitscanDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)          // �ǰ� ��
    {
        if (collision.CompareTag("Player"))
        {
            HealthGauge.health -= 1f;
            //EnemyStatus.enemyHealth -= 2f;
            //StopCoroutine("OnBloodScreen");             // �ǰ� �޴� ���� ����
            StartCoroutine("OnBloodScreen");            // �ǰݽ� ����ȭ�� �ڷ�ƾ ����
        }
    }
    private void OnTriggerExit2D(Collider2D collision)          // �ǰ� ���� X
    {
    }
    public void MobFlip()
    {
        Vector3 CharacterFlip = Vector3.zero;
        if (target.position.x > transform.position.x)       // ���� �÷��̾� ĳ�������� �ٶ�
        {
            CharacterFlip = Vector3.left;
            transform.localScale = new Vector3(-100, 100, 1);
        }
        else if (target.position.x < transform.position.x)
        {
            CharacterFlip = Vector3.right;
            transform.localScale = new Vector3(100, 100, 1);
        }
    }
    private IEnumerator OnBloodScreen()          // �ǰݽ� ����ȭ�� �ڷ�ƾ�޼ҵ�
    {
        float percent = 0;              // 1�ʵ��� ȸ��
        while (percent < 1)
        {                                  // ����ȭ�� ���İ��� 0���� 0.5(127)���� ��ȯ
            percent += Time.deltaTime;
            Color color = bloodScreen.color;
            color.a = Mathf.Lerp(0.5f, 0, curveBloodScreen.Evaluate(percent));
            bloodScreen.color = color;
            InfiniteLoopDetector.Run();
            yield return null;
        }
    }
}
