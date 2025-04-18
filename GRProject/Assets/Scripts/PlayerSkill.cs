using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [Header("�Ź��� ö��")]
    public GameObject maceObject;
    public GameObject[] webMace;          // ö�� ��ġ������ ö�� ���ӿ�����Ʈ ��ġ
    [SerializeField]    private int webMaceCount;
    float circleR;                    // ö�� ������
    float degree;                           // ����
    [SerializeField] private float objSpeed;       // ȸ�� �ӵ�
    [SerializeField] private SpriteRenderer maceAttack;     // ö�� ������
    [SerializeField] private AudioClip maceSound;           // ö�� ȿ����
    public Transform[] mace;        //  �� ö���� ��ġ
    public Vector2 maceSize;        // 

    [SerializeField]
    public AudioClip clip;

    [Header("�Ź��� Ʈ��")]
    [SerializeField] private GameObject webCounter;     // ��ų ���� Ƚ���� �����ִ� UI ī����
    public GameObject[] webLv;
    public Transform player;                                // �÷��̾� ��ġ����
    [SerializeField] public float trapCoolDown;             // Ʈ�� ��Ÿ��
    public int webCount = 2;                                // ��ų �ִ� ����Ƚ��
    public Image webCount1;
    public Image webCount2;
    public AudioClip trapSound;                 // Ʈ�� ��ġ ȿ����

    [Header("�� ����")]
    public GameObject poisonUi;                     // �� UI
    public GameObject poisonHorizontal;
    public GameObject poisonVertical;
    public GameObject poisonDiagonal;
    public GameObject poisonDiagonal2;
    [SerializeField]    private AudioClip poisonSound;      // �� ���� ȿ����
    [SerializeField]    private Transform charDirection;        // �� �߻� ����(��ġ)
    private int poisonLabLv;
    private int poisonAttackRate;

    private void Awake()
    {
        circleR = 400f;                                 // ö�� ȸ�� ������
        webMaceCount = SaveManager.instance.skillLabLvStat[0] + 1;     // ������ ���� ö�� ����
        if (webMaceCount <= 4)                  // ö�� �ӵ� ����
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
        poisonLabLv = SaveManager.instance.skillLabLvStat[3];
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
        InvokeRepeating("MaceSound", 1, 1.5f);          // ö�� ȿ����
        if (SaveManager.skillEnableStat[2] == true)       // �� �߻� ��ų Ȱ��ȭ��
        {
            poisonUi.SetActive(true);                       // �� ��Ÿ�� �˷��ִ� UI ON
            StartCoroutine("SpitPoison");                   // �� ��ų �ڷ�ƾ����
        }
    }
    void Update()
    {
        trapCoolDown = 4 - (SaveManager.skillLvStat[4]);
        StartCoroutine("WebMace");
        if (SaveManager.skillEnableStat[1] == true)
        {
            if(HealthGauge.isDie == false)
            {
                StartCoroutine("MakeWeb");
            }
        }else if (SaveManager.skillEnableStat[1] == false)
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
    private void OnDrawGizmos()                 // ö�� ��ġ �׵θ�
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
        int skill3Lab = SaveManager.instance.skillLabLvStat[2];
        switch (skill3Lab)
        {
            case 0:
                webLv[0].transform.position = new Vector3(player.position.x, player.position.y, 0.1f);
                Instantiate(webLv[0]);
                break;
            case 1:
                webLv[1].transform.position = new Vector3(player.position.x, player.position.y, 0.1f);
                Instantiate(webLv[1]);
                break;
            case 2:
                webLv[2].transform.position = new Vector3(player.position.x, player.position.y, 0.1f);
                Instantiate(webLv[2]);
                break;
            case 3:
                webLv[3].transform.position = new Vector3(player.position.x, player.position.y, 0.1f);
                Instantiate(webLv[3]);
                break;
            case 4:
                webLv[4].transform.position = new Vector3(player.position.x, player.position.y, 0.1f);
                Instantiate(webLv[4]);
                break;
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
    private IEnumerator SpitPoison()            // �� �߻� �ڷ�ƾ�޼ҵ�
    {                                // �÷��̾ �ٶ󺸴� ���⿡ ���� �� �߻� ��ġ ����
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
