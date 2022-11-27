using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private float moveSpeed = 300.0f;                         // �̵� �ӵ�
    private Vector3 moveDirection = Vector3.zero;      // �̵� ����
    private CharacterMovement movement;       // Ű����� �÷��̾� �̵�
    private SaveManager saveManager;
    private PlayerSkill playerSkill;
    private EnemyStatus enemyStatus;
    private static EnemyChasing enemyChasing;

    [SerializeField]
    private GameObject go_BaseUi;

    public AudioClip clip;
    public AudioClip enemyDestroySound;
    public AudioClip jumpSound;
    public GameObject maceSound;

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
    [SerializeField]    private GameObject tpCounter1;
    [SerializeField]    private GameObject tpCounter2;
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

    [Header("����")]
    public static int keysCount;
    [SerializeField]    private Image keyUi1;
    [SerializeField]    private Image keyUi2;
    [SerializeField]    private Image keyUi3;

    [Header("Ŭ����")]
    [SerializeField] private GameObject eraser;
    [SerializeField] private GameObject spawnPool;
    [SerializeField] private AudioClip clearSound;
    private float time = 0f;
    public static bool isClear;
    private int clearNum;
    [SerializeField]
    private GameObject clearUi;
    private GameObject killed;
    private GameObject coins;
    [SerializeField] private Text killedText;
    [SerializeField] private Text coinsText;
    public Transform clearUiSize;
    public Transform killedUiSize;
    public Transform coinsUiSize;
    [SerializeField] private GameObject jumpUnlock;
    [SerializeField] private GameObject somethingUnlock;
    private void Start()
    {
        //log = GameObject.FindGameObjectWithTag("Log").GetComponent<Transform>();
        StartCoroutine("BaseAttack");
        StartCoroutine("CoolDown");
        countImage1 = GameObject.Find("Count1").GetComponent<Image>();
        countImage2 = GameObject.Find("Count2").GetComponent<Image>();
    }
    private void Awake()
    {
//        Cursor.visible = false;
//        Cursor.lockState = CursorLockMode.Locked;
        isPause = false;
        movement = GetComponent<CharacterMovement>();
        Time.timeScale = 1f;
        clearNum = 0;
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
            tpCounter1.SetActive(false);
            tpCounter2.SetActive(false);
        }
        if (isClear)
        {
            canPlayerMove = false;
            ClearEvent();
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if ((keysCount == 3)&&(collision.CompareTag("SpiderGuys")))
        {
            isClear = true;
        }
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine("OnBloodScreen");            // �ǰݽ� ����ȭ�� �ڷ�ƾ ����
        }
    }
    private void OnTriggerStay2D(Collider2D collision)          // �ǰ� ��
    {
        if (collision.CompareTag("Enemy"))
        {
            //StartCoroutine("OnBloodScreen");            // �ǰݽ� ����ȭ�� �ڷ�ƾ ����
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
        Invoke("ClearStop", 3);
        maceSound.SetActive(false);
        eraser.SetActive(true);
        clearUi.SetActive(true);
        time += Time.deltaTime;
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
        }else if(time > 1.5f)
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