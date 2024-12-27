using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [Header("거미줄 철퇴")]
    public GameObject maceObject;
    public GameObject[] webMace;          // 철퇴 위치정보에 철퇴 게임오브젝트 배치
    [SerializeField]    private int webMaceCount;
    float circleR;                    // 철퇴 반지름
    float degree;                           // 각도
    [SerializeField] private float objSpeed;       // 회전 속도
    [SerializeField] private SpriteRenderer maceAttack;     // 철퇴 렌더러
    [SerializeField] private AudioClip maceSound;           // 철퇴 효과음
    public Transform[] mace;        //  각 철퇴의 위치
    public Vector2 maceSize;        // 

    [SerializeField]
    public AudioClip clip;

    [Header("거미줄 트랩")]
    [SerializeField] private GameObject webCounter;     // 스킬 남은 횟수를 보여주는 UI 카운터
    public GameObject webLv0;
    public GameObject webLv1;
    public GameObject webLv2;
    public GameObject webLv3;
    public GameObject webLv4;
    public Transform player;                                // 플레이어 위치정보
    [SerializeField] public float trapCoolDown;             // 트랩 쿨타임
    public int webCount = 2;                                // 스킬 최대 누적횟수
    public Image webCount1;
    public Image webCount2;
    public AudioClip trapSound;                 // 트랩 설치 효과음

    [Header("독 공격")]
    public GameObject poisonUi;                     // 독 UI
    public GameObject poisonHorizontal;
    public GameObject poisonVertical;
    public GameObject poisonDiagonal;
    public GameObject poisonDiagonal2;
    [SerializeField]    private AudioClip poisonSound;      // 독 공격 효과음
    [SerializeField]    private Transform charDirection;        // 독 발사 방향(위치)
    private int poisonLabLv;
    private int poisonAttackRate;

    private void Awake()
    {
        circleR = 400f;                                 // 철퇴 회전 반지름
        webMaceCount = SaveManager.skill1LabLvStat + 1;     // 레벨에 따른 철퇴 갯수
        if (webMaceCount <= 4)                  // 철퇴 속도 조절
        {
            objSpeed = 150f;
        }else if(webMaceCount > 4)
        {
            objSpeed = 200f;
        }
        if (webMaceCount > 4)
        {
            webMaceCount = 4;
        }
        poisonLabLv = SaveManager.skill4LabLvStat;
        if (poisonLabLv >= 3)
        {
            poisonLabLv = 2;
        }
        poisonAttackRate = 3 - poisonLabLv;
    }
    void Start()
    {
        StartCoroutine("WebCoolDown");
        MaceOn();
        InvokeRepeating("MaceSound", 1, 1.5f);          // 철퇴 효과음
        if (SaveManager.skill5EnableStat == true)       // 독 발사 스킬 활성화면
        {
            poisonUi.SetActive(true);                       // 독 쿨타임 알려주는 UI ON
            StartCoroutine("SpitPoison");                   // 독 스킬 코루틴실행
        }
    }
    void Update()
    {
        trapCoolDown = 4 - (SaveManager.skill4LvStat);
        StartCoroutine("WebMace");
        if (SaveManager.skill4EnableStat == true)
        {
            if(HealthGauge.isDie == false)
            {
                StartCoroutine("MakeWeb");
            }
        }else if(SaveManager.skill4EnableStat == false)
        {
            webCounter.SetActive(false);
        }
        if (HealthGauge.isDie == true)
        {
            webCount = 0;
            maceObject.SetActive(false);
            poisonUi.SetActive(false);
            StopCoroutine("WebCoolDown");
            CancelInvoke("MaceSound");
            StopCoroutine("SpitPoison");
        }
    }
    private void OnDrawGizmos()                 // 철퇴 위치 테두리
    {
        for (int i = 0; i < webMaceCount; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(mace[i].position, maceSize);
        }
    }
    private void MaceOn()
    {
        for(int i=0; i<webMaceCount; i++)
        {
            webMace[i].SetActive(true);
        }
    }
    private IEnumerator WebMace()
    {
        degree += Time.deltaTime * objSpeed;
        Collider2D[] maceColleder1 = Physics2D.OverlapBoxAll(mace[0].position, maceSize, 0);
        Collider2D[] maceColleder2 = Physics2D.OverlapBoxAll(mace[1].position, maceSize, 0);
        Collider2D[] maceColleder3 = Physics2D.OverlapBoxAll(mace[2].position, maceSize, 0);
        Collider2D[] maceColleder4 = Physics2D.OverlapBoxAll(mace[3].position, maceSize, 0);
        if (degree < 360)
        {
            for (int i = 0; i < webMaceCount; i++)
            {
                var rad = Mathf.Deg2Rad * (degree + (i * (360 / webMaceCount)));
                var x = circleR * Mathf.Sin(rad);
                var y = circleR * Mathf.Cos(rad);
                webMace[i].transform.position = transform.position + new Vector3(x, y);
                webMace[i].transform.rotation = Quaternion.Euler(0, 0, (degree + (i * (360 / webMaceCount))) * -1);
            }
        }else
        {
            degree = 0;
        }
        foreach (Collider2D collider in maceColleder1)
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
        foreach (Collider2D collider in maceColleder2)
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
        foreach (Collider2D collider in maceColleder3)
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
        foreach (Collider2D collider in maceColleder4)
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
    private void MaceSound()
    {
        SoundManager.SoundEffect.SoundPlay("maceSound", maceSound);
    }
    private IEnumerator MakeWeb()
    {
        if (webCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SpawnWeb();
                SoundManager.SoundEffect.SoundPlay("trapSound", trapSound);
                webCount -= 1;
                CountCheck();
            }
            yield return null;
        }
    }
    private void SpawnWeb()
    {
        int skill3Lab = SaveManager.skill3LabLvStat;
        if (skill3Lab == 0)
        {
            webLv0.transform.position = new Vector3(player.position.x, player.position.y, 0.1f);
            Instantiate(webLv0);
        }else if (skill3Lab == 1)
        {
            webLv1.transform.position = new Vector3(player.position.x, player.position.y, 0.1f);
            Instantiate(webLv1);
        }else if (skill3Lab == 2)
        {
            webLv2.transform.position = new Vector3(player.position.x, player.position.y, 0.1f);
            Instantiate(webLv2);
        }else if (skill3Lab == 3)
        {
            webLv3.transform.position = new Vector3(player.position.x, player.position.y, 0.1f);
            Instantiate(webLv3);
        }else if (skill3Lab == 4)
        {
            webLv4.transform.position = new Vector3(player.position.x, player.position.y, 0.1f);
            Instantiate(webLv4);
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
    private IEnumerator SpitPoison()            // 독 발사 코루틴메소드
    {                                // 플레이어가 바라보는 방향에 따라 독 발사 위치 변경
        SoundManager.SoundEffect.SoundPlay("poisonSound", poisonSound);
        if ((Input.GetAxisRaw("Horizontal") > 0) && (Input.GetAxisRaw("Vertical") > 0))
        {
            poisonDiagonal.transform.position = new Vector3(player.position.x + 250, player.position.y + 250, 0.1f);
            poisonDiagonal.transform.rotation = Quaternion.Euler(0, 0, 45);
            Instantiate(poisonDiagonal);
        }else if ((Input.GetAxisRaw("Horizontal") > 0) && (Input.GetAxisRaw("Vertical") < 0))
        {
            poisonDiagonal2.transform.position = new Vector3(player.position.x + 250, player.position.y - 250, 0.1f);
            poisonDiagonal2.transform.rotation = Quaternion.Euler(180, 0, 45);
            Instantiate(poisonDiagonal2);
        }else if ((Input.GetAxisRaw("Horizontal") < 0) && (Input.GetAxisRaw("Vertical") < 0))
        {
            poisonDiagonal2.transform.position = new Vector3(player.position.x - 250, player.position.y - 250, 0.1f);
            poisonDiagonal2.transform.rotation = Quaternion.Euler(180, 180, 45);
            Instantiate(poisonDiagonal2);
        }else if ((Input.GetAxisRaw("Horizontal") < 0) && (Input.GetAxisRaw("Vertical") > 0))
        {
            poisonDiagonal.transform.position = new Vector3(player.position.x - 250, player.position.y + 250, 0.1f);
            poisonDiagonal.transform.rotation = Quaternion.Euler(0, 180, 45);
            Instantiate(poisonDiagonal);
        }else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            poisonHorizontal.transform.position = new Vector3(player.position.x - 250, player.position.y, 0.1f);
            poisonHorizontal.transform.rotation = Quaternion.Euler(0, 180, 0);
            Instantiate(poisonHorizontal);
        }else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            poisonHorizontal.transform.position = new Vector3(player.position.x + 250, player.position.y, 0.1f);
            poisonHorizontal.transform.rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(poisonHorizontal);
        }else if (Input.GetAxisRaw("Vertical") > 0)
        {
            poisonVertical.transform.position = new Vector3(player.position.x, player.position.y + 250, 0.1f);
            poisonVertical.transform.rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(poisonVertical);
        }else if (Input.GetAxisRaw("Vertical") < 0)
        {
            poisonVertical.transform.position = new Vector3(player.position.x, player.position.y - 250, 0.1f);
            poisonVertical.transform.rotation = Quaternion.Euler(0, 0, 180);
            Instantiate(poisonVertical);
        }else
        {
            float charDir = charDirection.localScale.x;
            if ( charDir == 1f)
            {
                poisonHorizontal.transform.position = new Vector3(player.position.x + 250, player.position.y, 0.1f);
                poisonHorizontal.transform.rotation = Quaternion.Euler(0, 0, 0);
                Instantiate(poisonHorizontal);
            }else if( charDir == -1f)
            {
                poisonHorizontal.transform.position = new Vector3(player.position.x - 250, player.position.y, 0.1f);
                poisonHorizontal.transform.rotation = Quaternion.Euler(0, 180, 0);
                Instantiate(poisonHorizontal);
            }
        }
        yield return new WaitForSeconds(poisonAttackRate);
        StartCoroutine("SpitPoison");
    }
}
