using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed;                         // 이동 속도
    private Vector3 moveDirection = Vector3.zero;      // 이동 벡터
    private CharacterMovement movement;       // 키보드로 플레이어 이동
    private SaveManager saveManager;
    private PlayerSkill playerSkill;
    private EnemyStatus enemyStatus;
    private static EnemyChasing enemyChasing;

    [SerializeField]
    private GameObject go_BaseUi;

    public AudioClip clip;
    public AudioClip enemyDestroySound;
    public AudioClip jumpSound;
    public AudioClip electricShock;
    public AudioClip healSound;

    private bool enable3;
    private bool enable4;
    private bool enable5;

    public static bool canPlayerMove = false;
    public static bool isPause = false;

    [Header("BloodScreen")]
    [SerializeField]
    private Image bloodScreen;
    [SerializeField]
    private AnimationCurve curveBloodScreen;

    [Header("기본공격")]
    [SerializeField]
    private SpriteRenderer baseAttackEffect;            // 기본공격 이펙트 스프라이트
    [SerializeField]
    private AnimationCurve curveAttackEffect;       // 기본공격 이펙트 커브모션
    public Transform pos;
    public Vector2 boxSize;

    [Header("점프")]
    [SerializeField]    private GameObject tpCounter;
    public float lerpTime = 1.0f;
    private bool canDash = true;
    private bool isDashing;
    private float dashingTime = 0.2f;
    private float dashingCoolDown = 0.25f;
    [SerializeField]    private TrailRenderer tr;
    [SerializeField]    public float jumpCoolDown;
    public int teleportationCount = 2;
    public float currentCoolDown;
    public float rechargeCoolDown = 5;
    public Image countImage1;
    public Image countImage2;

    [Header("열쇠")]
    [SerializeField]    private GameObject key;
    public static int keysCount;
    [SerializeField]    private Image keyUi1;
    [SerializeField]    private Image keyUi2;
    [SerializeField]    private Image keyUi3;

    [Header("클리어")]
    [SerializeField] private GameObject stage1Spidy;
    [SerializeField] private GameObject stage2Spidy;
    [SerializeField] private GameObject stage3Spidy;
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
        moveSpeed = 300 + (SaveManager.skill2LabLvInstance * 30);
        isPause = false;
        movement = GetComponent<CharacterMovement>();
        Time.timeScale = 1f;
        clearNum = 0;
        enable3 = SaveManager.skill3EnableInstance;
        enable4 = SaveManager.skill4EnableInstance;
        enable5 = SaveManager.skill5EnableInstance;
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
        jumpCoolDown = 6 - (SaveManager.skill3LvInstance);
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
        if (SaveManager.skill3EnableInstance == true)
        {
            if (isDashing)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {
                StartCoroutine("Dash");
            }
        }else if(SaveManager.skill3EnableInstance == false)
        {
            tpCounter.SetActive(false);
        }
        if (isClear)
        {
            canPlayerMove = false;
            Invoke("ClearEvent", 3.5f);
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
            StartCoroutine("OnBloodScreen");            // 피격시 빨간화면 코루틴 실행
        }
        if (collision.CompareTag("Elec"))
        {
            StartCoroutine("OnBloodScreen");
            HealthGauge.health -= 5f;
            SoundManager.SoundEffect.SoundPlay("electricShock", electricShock);
        }
        if (collision.CompareTag("HealItem"))
        {
            if (HealthGauge.maxHealth > HealthGauge.health)
            {
                SoundManager.SoundEffect.SoundPlay("healSound", healSound);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)          // 피격 시
    {
        if (collision.CompareTag("Enemy"))
        {
            //StartCoroutine("OnBloodScreen");            // 피격시 빨간화면 코루틴 실행
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
        float x = Input.GetAxisRaw("Horizontal");           // 좌우 이동
        float y = Input.GetAxisRaw("Vertical");              // 상하 이동
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
    private IEnumerator OnBloodScreen()          // 피격시 빨간화면 코루틴메소드
    {
        float percent = 0;              // 1초동안 회복
        while (percent < 1)
        {                                  // 빨간화면 알파값을 0에서 0.5(127)까지 변환
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
    private IEnumerator BaseAttack()                // 기본공격 코루틴메소드
    {
        yield return new WaitForSeconds(2);         // 2초 대기시간
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")                // Enemy를 때리면
            {
                collider.GetComponent<EnemyStatus>().Skill1Damage();
            }                                           // Skill1 수치의 데미지 부여
            if (collider.tag == "Boss")
            {
                collider.GetComponent<BossStatus>().Skill1Damage();
            }
        }
        StartCoroutine("BaseAttackEffect"); // 기본공격 이펙트 코루틴메소드 반복
        StartCoroutine("BaseAttack");       // 기본공격 코루틴메소드 반복
        SoundManager.SoundEffect.SoundPlay("BaseAttack",clip);  // 공격 사운드
    }
    private IEnumerator BaseAttackEffect()          // 기본공격 이펙트 코루틴메소드
    {
        float percent = 0;
        Color color = baseAttackEffect.color;
        while (percent < 1)                     // 이펙트 fadeout
        {
            percent += Time.deltaTime/0.5f;         // 1초동안 출력
            color.a = Mathf.Lerp(1f, 0, curveAttackEffect.Evaluate(percent));
            baseAttackEffect.color = color;
            yield return null;
        }
    }
    public void ColliderBox()         // 공격 판정 방향 박스
    {                                          // 콜라이더와 공격이펙트 방향/위치 조절
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
        if (teleportationCount > 0)        //스킬 사용 가능 확인
        {
            canDash = false;
            isDashing = true;
            teleportationCount -= 1;            //사용횟수 차감
            CountCheck();               //이동기 선행모션
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
            //순간이동 횟수가 최대 횟수인 2 보다 작을 때, 재충전시간을 통해 횟수를 충전한다. 
            if (teleportationCount < 2)
            {
                yield return new WaitForSeconds(jumpCoolDown);
                //위에서 저장한 현재시간 + 스킬 쿨타임보다 현재 시간이 클 경우, 
                teleportationCount += 1;
                CountCheck();
            }
            yield return null;
        }
    }
    private void CountCheck()       //순간이동 횟수UI 색상 변경
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
    private void SpiderSpawnRandom()
    {
        int spawnSpidy = Random.Range(0, 4);
        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            if (enable3 == false)
            {
                switch (spawnSpidy)
                {
                    case 0:
                        Instantiate(stage1Spidy, new Vector3(Random.Range(-3900, -2900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 1:
                        Instantiate(stage1Spidy, new Vector3(Random.Range(2900, 3900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 2:
                        Instantiate(stage1Spidy, new Vector3(Random.Range(-3900, 3900), Random.Range(-2950, -1950), -0.1f), Quaternion.identity); break;
                    case 3:
                        Instantiate(stage1Spidy, new Vector3(Random.Range(-3900, 3900), Random.Range(1950, 2950), -0.1f), Quaternion.identity); break;
                }
            }else if (enable3 == true)
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
        }else if (SceneManager.GetActiveScene().name == "Stage2")
        {
            if (enable4 == false)
            {
                switch (spawnSpidy)
                {
                    case 0:
                        Instantiate(stage2Spidy, new Vector3(Random.Range(-3900, -2900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 1:
                        Instantiate(stage2Spidy, new Vector3(Random.Range(2900, 3900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 2:
                        Instantiate(stage2Spidy, new Vector3(Random.Range(-3900, 3900), Random.Range(-2950, -1950), -0.1f), Quaternion.identity); break;
                    case 3:
                        Instantiate(stage2Spidy, new Vector3(Random.Range(-3900, 3900), Random.Range(1950, 2950), -0.1f), Quaternion.identity); break;
                }
            }else if (enable4 == true)
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
        }else if (SceneManager.GetActiveScene().name == "Stage3")
        {
            if (enable5 == false)
            {
                switch (spawnSpidy)
                {
                    case 0:
                        Instantiate(stage3Spidy, new Vector3(Random.Range(-3900, -2900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 1:
                        Instantiate(stage3Spidy, new Vector3(Random.Range(2900, 3900), Random.Range(-2950, 2950), -0.1f), Quaternion.identity); break;
                    case 2:
                        Instantiate(stage3Spidy, new Vector3(Random.Range(-3900, 3900), Random.Range(-2950, -1950), -0.1f), Quaternion.identity); break;
                    case 3:
                        Instantiate(stage3Spidy, new Vector3(Random.Range(-3900, 3900), Random.Range(1950, 2950), -0.1f), Quaternion.identity); break;
                }
            }else if (enable4 == true)
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
        if (keysCount == 3)
        {
            keyUi1.color = Color.white;
            keyUi2.color = Color.white;
            keyUi3.color = Color.white;
        }
        else if (keysCount == 2)
        {
            keyUi1.color = Color.white;
            keyUi2.color = Color.white;
            keyUi3.color = Color.black;
        }
        else if (keysCount == 1)
        {
            keyUi1.color = Color.white;
            keyUi2.color = Color.black;
            keyUi3.color = Color.black;
        }
        else if (keysCount == 0)
        {
            keyUi1.color = Color.black;
            keyUi2.color = Color.black;
            keyUi3.color = Color.black;
        }
    }
    private void ClearEvent()
    {
        killedText.text = EnemySpawnPool.enemyKilledCountInstance.ToString();
        coinsText.text = CoinManager.earnedCoinsInstance.ToString();
        geneText.text = CoinManager.earnedGeneInstance.ToString();
        Invoke("ClearStop", 3);
        eraser.SetActive(true);
        clearUi.SetActive(true);
        time += Time.deltaTime/1.5f;
        Debug.Log(time);
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
        else if(time > 1.5f)
        {
            //Time.timeScale = 0f;
        }
        //if (SaveManager.getSkill3EnableInstance == true)
        //{
        //    Debug.Log(SaveManager.getSkill3EnableInstance);
        //    jumpUnlock.SetActive(false);
        //    somethingUnlock.SetActive(true);
        //}
        //else if (SaveManager.getSkill3EnableInstance == false)
        //{
        //    Debug.Log(SaveManager.getSkill3EnableInstance);
        //    somethingUnlock.SetActive(false);
        //    jumpUnlock.SetActive(true);
        //}
    }
    private void ClearSound()
    {
        if(clearNum == 0)
        {
            SoundManager.SoundEffect.SoundPlay("clearSound", clearSound);
            SoundManager.SoundEffect.SoundPlay("clearSound", clearSound);
            SoundManager.SoundEffect.SoundPlay("clearSound", clearSound);
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
}