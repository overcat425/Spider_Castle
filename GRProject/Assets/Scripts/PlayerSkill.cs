using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Header("�Ź��� ö��")]
    public GameObject webMace;
    float circleR = 300f;                    // ö�� ������
    float degree;                           // ����
    public float objSpeed = 200f;       // ȸ�� �ӵ�
    [SerializeField]
    private SpriteRenderer maceAttack;
    public Transform mace;
    public Vector2 maceSize;

    private bool maceOn = true;
    [SerializeField]
    public AudioClip clip;
    void Start()
    {
    }

    void Update()
    {
        StartCoroutine("WebMace");
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    if (maceOn == false)
        //    {
        //        webMace.SetActive(true);
        //        maceOn = true;
        //    }else if (maceOn == true)
        //    {
        //        webMace.SetActive(false);
        //        maceOn = false;
        //    }
        //}
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(mace.position, maceSize);
    }
    private IEnumerator WebMace()
    {
        degree += Time.deltaTime * objSpeed;
        if (degree < 360)
        {
            //������ ����
            /*if (circleR < 300f && state == 0)
                circleR += 1.5f;
            else if (circleR >= 300f)
                state = 1;
            if (circleR > 50 && state == 1)
                circleR -= 1.5f;
            else if (circleR <= 50)
                state = 0;*/

            //if (circleR < 300f)
            //    circleR += 1.5f;

            var rad = Mathf.Deg2Rad * (degree);
            var x = circleR * Mathf.Sin(rad);
            var y = circleR * Mathf.Cos(rad);
            webMace.transform.position = transform.position + new Vector3(x, y);
            webMace.transform.rotation = Quaternion.Euler(0, 0, degree * -1); //����� �ٶ󺸰� ���� ����
        }
        else
        {
            degree = 0;
        }
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(mace.position, maceSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<EnemyStatus>().Skill2Damage();
            }
        }
        yield return null;
    }
}
