using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [Header("거미줄 철퇴")]
    public GameObject webMace;
    float circleR = 300f;                    // 철퇴 반지름
    float degree;                           // 각도
    public float objSpeed = 200f;       // 회전 속도
    [SerializeField]
    private SpriteRenderer maceAttack;
    public Transform mace;
    public Vector2 maceSize;

    private bool maceOn = true;
    [SerializeField]
    public AudioClip clip;

    [Header("거미줄 트랩")]
    [SerializeField] private GameObject webCounter;
    public GameObject web;
    public Transform player;
    [SerializeField] public float trapCoolDown;
    public int webCount = 2;
    public Image webCount1;
    public Image webCount2;
    public AudioClip trapSound;
    void Start()
    {
        StartCoroutine("WebCoolDown");
    }

    void Update()
    {
        trapCoolDown = 4 - (SaveManager.skill4LvInstance);
        StartCoroutine("WebMace");
        if (SaveManager.skill4EnableInstance == true)
        {
            StartCoroutine("MakeWeb");
        }else if(SaveManager.skill4EnableInstance == false)
        {
            webCounter.SetActive(false);
        }
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
            //반지름 변경
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
            webMace.transform.rotation = Quaternion.Euler(0, 0, degree * -1); //가운데를 바라보게 각도 조절
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
            if (collider.tag == "Boss")
            {
                collider.GetComponent<BossStatus>().Skill2Damage();
            }
        }
        yield return null;
    }
    private IEnumerator MakeWeb()
    {
        if (webCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                web.transform.position = new Vector2(player.position.x, player.position.y);
                Instantiate(web);
                SoundManager.SoundEffect.SoundPlay("trapSound", trapSound);
                webCount -= 1;
                CountCheck();
            }
            yield return null;
        }
    }
    private IEnumerator WebCoolDown()
    {
        while (true)
        {
            if (webCount < 2)
            {
                yield return new WaitForSeconds(trapCoolDown);
                webCount += 1;
                CountCheck();
            }
            yield return null;
        }
    }
    private void CountCheck()
    {
        if (webCount == 2)
        {
            webCount1.color = Color.white;
            webCount2.color = Color.white;
        }
        else if (webCount == 1)
        {
            webCount1.color = Color.white;
            webCount2.color = Color.black;
        }
        else if (webCount == 0)
        {
            webCount1.color = Color.black;
            webCount2.color = Color.black;
        }
    }
    private IEnumerator SpitPoison()
    {
        if (webCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                web.transform.position = new Vector2(player.position.x, player.position.y);
                Instantiate(web);
                SoundManager.SoundEffect.SoundPlay("trapSound", trapSound);
                webCount -= 1;
                CountCheck();
            }
            yield return null;
        }
    }
}
