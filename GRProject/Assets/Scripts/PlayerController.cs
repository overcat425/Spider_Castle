using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;                         // �̵� �ӵ�
    private Vector3 moveDirection = Vector3.zero;      // �̵� ����
    private CharacterMovement movement;       // Ű����� �÷��̾� �̵�
    private SaveManager saveManager;
    private PlayerSkill playerSkill;
    private EnemyStatus enemyStatus;
    private static EnemyChasing enemyChasing;
    public EnemySpawner enemySpawner;

    [SerializeField]
    private GameObject go_BaseUi;

    public AudioClip clip;
    public AudioClip enemyDestroySound;
    public AudioClip jumpSound;
    public AudioClip electricShock;
    public AudioClip healSound;

    private bool[] enable = new bool[3];

    public static bool invincible = false;
    public static bool canPlayerMove = false;
    public static bool isPause = false;

    [Header("BloodScreen")]
    [SerializeField]
    private Image bloodScreen;
    [SerializeField]
    private AnimationCurve curveBloodScreen;

    [Header("�⺻����")]
    [SerializeField]
    private SpriteRenderer baseAttackEffect;            // �⺻���� ����Ʈ ��������Ʈ
    [SerializeField]
    private AnimationCurve curveAttackEffect;       // �⺻���� ����Ʈ Ŀ����
    public Transform pos;
    public Vector2 boxSize;

    [Header("����")]
    [SerializeField]    private GameObject tpCounter;
    public float lerpTime = 1.0f;
    private bool canDash = true;
    private bool isDashing;
    private float dashingTime = 0.2f;
    private float dashingCoolDown = 0.25f;
    [SerializeField]    private SpriteRenderer sr;
    [SerializeField]    private TrailRenderer tr;
    [SerializeField]    public float jumpCoolDown;
    public int teleportationCount = 2;
    public float currentCoolDown;
    public float rechargeCoolDown = 5;
    public Image countImage1;
    public Image countImage2;

    [Header("����")]
    [SerializeField]    private GameObject key;
    public static int keysCount;
    [SerializeField]    private Image[] keyUi;

    [Header("����")]
    private float screenTime;
    private bool dieSoundOnce = false;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private Image dieScreen;
    [SerializeField] private GameObject spiderImg;
    [SerializeField] private GameObject spiderDieImg;
    [SerializeField] private Transform spiderPos;
    [SerializeField] private GameObject dieHP;
    [SerializeField] private GameObject dieAttackGaugeBar;
    [SerializeField] private GameObject dieAttackGauge;


    [Header("Ŭ����")]
    [SerializeField] private GameObject[] stageSpidy;
    [SerializeField] private GameObject clearGene;
    [SerializeField] private GameObject eraser;
    [SerializeField] private GameObject spawnPool;
    [SerializeField] private AudioClip clearSound;
    private float time;
    public static bool isClear;
    private int clearNum;
    [SerializeField]
    private GameObject clearUi;
    private GameObject killed;
    private GameObject coins;
    private GameObject gene;
    [SerializeField] private Text killedText;
    [SerializeField] private Text coinsText;
    [SerializeField] private Text geneText;
    public Transform clearUiSize;
    public Transform killedUiSize;
    public Transform coinsUiSize;
    public Transform geneUiSize;
    [SerializeField] private GameObject jumpUnlock;
    [SerializeField] private GameObject somethingUnlock;
    private void Awake()
    {
        time = 0f;
        if (SaveManager.instance.skillLabLvStat[1] < 4)
        {
            moveSpeed = 300 + (SaveManager.instance.skillLabLvStat[1] * 30);
        }else if (SaveManager.instance.skillLabLvStat[1] == 4){
            moveSpeed = 390;
        }
        isPause = false;
        movement = GetComponent<CharacterMovement>();
        Time.timeScale = 1f;
        clearNum = 0;
        for (int i = 0; i < 3; i++)
        {
            enable[i] = SaveManager.skillEnableStat[i];
        }
        jumpCoolDown = 6 - (SaveManager.skillLvStat[3]);
    }
    private void Start()
    {
        KeySpawn();
        SpiderSpawnRandom();
        StartCoroutine("BaseAttack");
        StartCoroutine("CoolDown");
        //countImage1 = GameObject.Find("TpCounter").GetComponent<Image>();
        //countImage2 = GameObject.Find("TpCounter2").GetComponent<Image>();
    }

    private void Update()
    {
        if (canPlayerMove)
        {
            UpdateMove();
            ColliderBox();
            KeyCount();
        }
        if (isPause)
        {
            canPlayerMove = false;
        }
        else
        {
            canPlayerMove = true;
        }
        if (SaveManager.skillEnableStat[0] == true)
        {
            if (isDashing)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {
                if (HealthGauge.isDie == false)
                {
                    StartCoroutine("Dash");
                }
            }
        }else if(SaveManager.skillEnableStat[0] == false)
        {
            tpCounter.SetActive(false);
        }
        if (isClear)
        {
            canPlayerMove = false;
            eraser.SetActive(true);
            Invoke("ClearEvent", 3.5f);
        }
        if (HealthGauge.isDie == true)
        {
            canPlayerMove = false;
            eraser.SetActive(true);
            StartCoroutine("PlayerDie");
            StartCoroutine("DieEvent");
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if ((keysCount == 3)&&(collision.CompareTag("SpiderGuys")))
        {
            isClear = true;
        }
        if (collision.CompareTag("Enemy")|| collision.CompareTag("Boss"))
        {
            if(invincible == false)
            {
                StartCoroutine("OnBloodScreen");            // �ǰݽ� ����ȭ�� �ڷ�ƾ ����
            }
        }
        if (collision.CompareTag("Elec"))
        {
            if (invincible == false)
            {
                StartCoroutine("OnBloodScreen");
                HealthGauge.health -= 5f;
                SoundManager.SoundEffect.SoundPlay("electricShock", electricShock);
            }
        }
        if (collision.CompareTag("HealItem"))
        {
            if (HealthGauge.maxHealth > HealthGauge.health)
            {
                SoundManager.SoundEffect.SoundPlay("healSound", healSound);
            }
        }
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }
    public void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");           // �¿� �̵�
        float y = Input.GetAxisRaw("Vertical");              // ���� �̵�
        moveDirection = new Vector3(x, y, 0);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        if(transform.position.x > 3950)
        {
            transform.Translate(new Vector3(-5, 0, 0));
        }else if(transform.position.x < -3950)
        {
            transform.Translate(new Vector3(5, 0, 0));
        }
        if (transform.position.y > 2930)
        {
            transform.Translate(new Vector3(0, -5, 0));
        }
        else if (transform.position.y < -2930)
        {
            transform.Translate(new Vector3(0, 5, 0));
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
            yield return null;
        }
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
            if (collider.tag == "Enemy")                // Enemy�� ������
            {
                collider.GetComponent<EnemyStatus>().Skill1Damage();
            }                                           // Skill1 ��ġ�� ������ �ο�
            if (collider.tag == "Boss")
            {
                collider.GetComponent<BossStatus>().Skill1Damage();
            }
        }
        StartCoroutine("BaseAttackEffect"); // �⺻���� ����Ʈ �ڷ�ƾ�޼ҵ� �ݺ�
        StartCoroutine("BaseAttack");       // �⺻���� �ڷ�ƾ�޼ҵ� �ݺ�
        SoundManager.SoundEffect.SoundPlay("BaseAttack",clip);  // ���� ����
    }
    private IEnumerator BaseAttackEffect()          // �⺻���� ����Ʈ �ڷ�ƾ�޼ҵ�
    {
        float percent = 0;
        Color color = baseAttackEffect.color;
        while (percent < 1)                     // ����Ʈ fadeout
        {
            percent += Time.deltaTime/0.5f;         // 1�ʵ��� ���
            color.a = Mathf.Lerp(1f, 0, curveAttackEffect.Evaluate(percent));
            baseAttackEffect.color = color;
            yield return null;
        }
    }
    public void ColliderBox()         // ���� ���� ���� �ڽ�
    {                                          // �ݶ��̴��� ��������Ʈ ����/��ġ ����
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            pos.localPosition = new Vector3(2.5f, 0, 0);
            pos.localRotation = Quaternion.Euler(0, 0, 0);
            boxSize = new Vector2(150, 300);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            pos.localPosition = new Vector3(-2.5f, 0, 0);
            pos.localRotation = Quaternion.Euler(0, 180, 0);
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
    public void LerpMove(Vector3 current, Vector3 target, float time)
    {
        float elapsedTime = 0.0f;
        this.transform.position = current;
        while (elapsedTime < time)
        {
            elapsedTime += (Time.deltaTime);
            this.transform.position = Vector3.Lerp(current, target, elapsedTime / time);
        }
        transform.position = target;
    }
    private IEnumerator Dash()
    {
        //float originalGravity = rb.gravityScale;
        //rb.gravityScale = 0f;
        if (teleportationCount > 0)        //��ų ��� ���� Ȯ��
        {
            canDash = false;
            isDashing = true;
            teleportationCount -= 1;            //���Ƚ�� ����
            CountCheck();               //�̵��� ������
            if ((Input.GetAxisRaw("Horizontal") > 0) && (Input.GetAxisRaw("Vertical") > 0))
            { LerpMove(this.transform.position, this.transform.position + new Vector3(150, 150, 0), lerpTime); }
            else if ((Input.GetAxisRaw("Horizontal") > 0) && (Input.GetAxisRaw("Vertical") < 0))
            { LerpMove(this.transform.position, this.transform.position + new Vector3(150, -150, 0), lerpTime); }
            else if ((Input.GetAxisRaw("Horizontal") < 0) && (Input.GetAxisRaw("Vertical") < 0))
            { LerpMove(this.transform.position, this.transform.position + new Vector3(-150, -150, 0), lerpTime); }
            else if ((Input.GetAxisRaw("Horizontal") < 0) && (Input.GetAxisRaw("Vertical") > 0))
            { LerpMove(this.transform.position, this.transform.position + new Vector3(-150, 150, 0), lerpTime); }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            { LerpMove(this.transform.position, this.transform.position + new Vector3(-150, 0, 0), lerpTime); }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            { LerpMove(this.transform.position, this.transform.position + new Vector3(150, 0, 0), lerpTime); }
            else if (Input.GetAxisRaw("Vertical") > 0)
            { LerpMove(this.transform.position, this.transform.position + new Vector3(0, 150, 0), lerpTime); }
            else if (Input.GetAxisRaw("Vertical") < 0)
            { LerpMove(this.transform.position, this.transform.position + new Vector3(0, -150, 0), lerpTime); }
            //rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
            SoundManager.SoundEffect.SoundPlay("jumpSound", jumpSound);
            if (SaveManager.instance.skillLabLvStat[1] == 4)
            {
                InvincibleOn();
            }
            tr.emitting = true;
            yield return new WaitForSeconds(dashingTime);
            tr.emitting = false;
            ///rb.gravityScale = originalGravity;
            isDashing = false;
            yield return new WaitForSeconds(dashingCoolDown);
            canDash = true;
        }
    }
    private IEnumerator CoolDown()
    {
        while (true)
        {
            //�����̵� Ƚ���� �ִ� Ƚ���� 2 ���� ���� ��, �������ð��� ���� Ƚ���� �����Ѵ�. 
            if (teleportationCount < 2)
            {
                yield return new WaitForSeconds(jumpCoolDown);
                //������ ������ ����ð� + ��ų ��Ÿ�Ӻ��� ���� �ð��� Ŭ ���, 
                teleportationCount += 1;
                CountCheck();
            }
            yield return null;
        }
    }
    private void CountCheck()       //�����̵� Ƚ��UI ���� ����
    {
        if (teleportationCount == 2)
        {
            countImage1.color = Color.white;
            countImage2.color = Color.white;
        }
        else if (teleportationCount == 1)
        {
            countImage1.color = Color.white;
            countImage2.color = Color.black;
        }
        else if (teleportationCount == 0)
        {
            countImage1.color = Color.black;
            countImage2.color = Color.black;
        }
    }
    private void InvincibleOn()
    {
        invincible = true;
        sr.color = new Color(1,1,1,0.2f);
        Invoke("InvincibleOff", 1f);
    }
    private void InvincibleOff()
    {
        invincible = false;
        sr.color = new Color(1, 1, 1, 1);
    }
    private void SpiderSpawnRandom()
    {
        int spawnSpidy = Random.Range(0, 4);
        if ((SceneManager.GetActiveScene().name == "Stage1") || (SceneManager.GetActiveScene().name == "Stage1Hard"))
        {
            if (enable[0] == false)
            {
                switch (spawnSpidy)
                {
                    case 0:
                        Instantiate(stageSpidy[0], new Vector3(Random.Range(-3900, -2900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 1:
                        Instantiate(stageSpidy[0], new Vector3(Random.Range(2900, 3900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 2:
                        Instantiate(stageSpidy[0], new Vector3(Random.Range(-3900, 3900), Random.Range(-2950, -1950), -0.1f), Quaternion.identity); break;
                    case 3:
                        Instantiate(stageSpidy[0], new Vector3(Random.Range(-3900, 3900), Random.Range(1950, 2950), -0.1f), Quaternion.identity); break;
                }
            }else if (enable[0] == true)
            {
                switch (spawnSpidy)
                {
                    case 0:
                        Instantiate(clearGene, new Vector3(Random.Range(-3900, -2900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 1:
                        Instantiate(clearGene, new Vector3(Random.Range(2900, 3900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 2:
                        Instantiate(clearGene, new Vector3(Random.Range(-3900, 3900), Random.Range(-2950, -1950), -0.1f), Quaternion.identity); break;
                    case 3:
                        Instantiate(clearGene, new Vector3(Random.Range(-3900, 3900), Random.Range(1950, 2950), -0.1f), Quaternion.identity); break;
                }
            }
        }else if ((SceneManager.GetActiveScene().name == "Stage2") || (SceneManager.GetActiveScene().name == "Stage2Hard"))
        {
            if (enable[1] == false)
            {
                switch (spawnSpidy)
                {
                    case 0:
                        Instantiate(stageSpidy[1], new Vector3(Random.Range(-3900, -2900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 1:
                        Instantiate(stageSpidy[1], new Vector3(Random.Range(2900, 3900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 2:
                        Instantiate(stageSpidy[1], new Vector3(Random.Range(-3900, 3900), Random.Range(-2950, -1950), -0.1f), Quaternion.identity); break;
                    case 3:
                        Instantiate(stageSpidy[1], new Vector3(Random.Range(-3900, 3900), Random.Range(1950, 2950), -0.1f), Quaternion.identity); break;
                }
            }else if (enable[1] == true)
            {
                switch (spawnSpidy)
                {
                    case 0:
                        Instantiate(clearGene, new Vector3(Random.Range(-3900, -2900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 1:
                        Instantiate(clearGene, new Vector3(Random.Range(2900, 3900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 2:
                        Instantiate(clearGene, new Vector3(Random.Range(-3900, 3900), Random.Range(-2950, -1950), -0.1f), Quaternion.identity); break;
                    case 3:
                        Instantiate(clearGene, new Vector3(Random.Range(-3900, 3900), Random.Range(1950, 2950), -0.1f), Quaternion.identity); break;
                }
            }
        }else if ((SceneManager.GetActiveScene().name == "Stage3") || (SceneManager.GetActiveScene().name == "Stage3Hard"))
        {
            if (enable[2] == false)
            {
                switch (spawnSpidy)
                {
                    case 0:
                        Instantiate(stageSpidy[2], new Vector3(Random.Range(-3900, -2900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 1:
                        Instantiate(stageSpidy[2], new Vector3(Random.Range(2900, 3900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 2:
                        Instantiate(stageSpidy[2], new Vector3(Random.Range(-3900, 3900), Random.Range(-2950, -1950), -0.1f), Quaternion.identity); break;
                    case 3:
                        Instantiate(stageSpidy[2], new Vector3(Random.Range(-3900, 3900), Random.Range(1950, 2950), -0.1f), Quaternion.identity); break;
                }
            }else if (enable[1] == true)
            {
                switch (spawnSpidy)
                {
                    case 0:
                        Instantiate(clearGene, new Vector3(Random.Range(-3900, -2900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 1:
                        Instantiate(clearGene, new Vector3(Random.Range(2900, 3900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 2:
                        Instantiate(clearGene, new Vector3(Random.Range(-3900, 3900), Random.Range(-2950, -1950), -0.1f), Quaternion.identity); break;
                    case 3:
                        Instantiate(clearGene, new Vector3(Random.Range(-3900, 3900), Random.Range(1950, 2950), -0.1f), Quaternion.identity); break;
                }
            }
        }
    }
    private void KeySpawn()
    {
        Instantiate(key, new Vector3(Random.Range(-3950, 3950), Random.Range(-2950, 2950), 0.1f), Quaternion.identity);
        Instantiate(key, new Vector3(Random.Range(-3950, 3950), Random.Range(-2950, 2950), 0.1f), Quaternion.identity);
        Instantiate(key, new Vector3(Random.Range(-3950, 3950), Random.Range(-2950, 2950), 0.1f), Quaternion.identity);
    }
    private void KeyCount()
    {
        switch (keysCount)
        {
            case 0:
                for (int i = 0; i < 3; i++)
                {
                    keyUi[i].color = Color.black;
                }
                break;
            case 1:
                keyUi[0].color = Color.white;
                for (int i = 1; i < 3; i++)
                {
                    keyUi[i].color = Color.black;
                }
                break;
            case 2:
                for (int i = 0; i < 2; i++)
                {
                    keyUi[i].color = Color.white;
                }keyUi[2].color = Color.black;
                break;
            case 3:
                for (int i = 0; i < 3; i++)
                {
                    keyUi[i].color = Color.white;
                }
                break;
        }
    }
    private void ClearEvent()
    {
        killedText.text = EnemySpawner.enemyKilledCountStat.ToString();
        coinsText.text = CoinManager.earnedCoinsStat.ToString();
        geneText.text = CoinManager.earnedGeneStat.ToString();
        Invoke("ClearStop", 3);
        eraser.SetActive(true);
        clearUi.SetActive(true);
        time += Time.deltaTime/1.5f;
        if (time < 0.5f)
        {
            clearUiSize.localScale = Vector3.one * (time * 2f);
        } else if (time <= 1.0f)
        {
            killedUiSize.localScale = Vector3.one * ((time - 0.5f) * 2f);
        } else if (time <= 1.5f)
        {
            ClearSound();
            coinsUiSize.localScale = Vector3.one * ((time - 1.0f) * 2f);
            geneUiSize.localScale = Vector3.one * ((time - 1.0f) * 2f);
        }
    }
    private void ClearSound()
    {
        if(clearNum == 0)
        {
            for(int i = 0; i < 3; i++)
            {
                SoundManager.SoundEffect.SoundPlay("clearSound", clearSound);
            }
            clearNum++;
        }
    }
    public void ClearSave()
    {
        HealthGauge.health = 369;
        SceneManager.LoadScene("StartGame");
    }
    public void ClearStop()
    {
        Time.timeScale = 0f;
    }
    public IEnumerator PlayerDie()
    {
        canPlayerMove = false;
        spiderImg.SetActive(false);
        spiderDieImg.SetActive(true);
        dieHP.SetActive(false);
        dieAttackGaugeBar.SetActive(false);
        dieAttackGauge.SetActive(false);
        StopCoroutine("BaseAttack");
        StopCoroutine("CoolDown");
        yield return new WaitForSeconds(0.5f);
        while (time <= 1f)
        {
            time += Time.deltaTime / 20f;
            spiderPos.position = new Vector2(-time * 1.5f, -time);
            spiderPos.localPosition = new Vector2(spiderPos.position.x, spiderPos.position.y);
            spiderPos.localRotation = Quaternion.Euler(0, 0, time*90);
            yield return null;
        }
        if(time >= 1f)
        {
            if(dieSoundOnce == false)
            {
                SoundManager.SoundEffect.SoundPlay("dieSound", dieSound);
                dieSoundOnce = true;
            }
        }
    }
    public IEnumerator DieEvent()
    {
        yield return new WaitForSeconds(2.5f);
        canPlayerMove = false;
        dieScreen.gameObject.SetActive(true);
        Color alpha = dieScreen.color;
        while (alpha.a < 1f)
        {
            screenTime += Time.deltaTime / 200f;
            alpha.a = Mathf.Lerp(0, 1, screenTime);
            dieScreen.color = alpha;
            yield return null;
        }
    }
}