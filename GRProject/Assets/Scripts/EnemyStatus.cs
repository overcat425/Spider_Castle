using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyStatus : MonoBehaviour
{
    private SpriteRenderer sprite;                  // �� ��������Ʈ
    private Color color;
    public GameObject hudDamageText;        // �ǰݽ� �������ؽ�Ʈ
    public GameObject coins;                        // ����
    public Transform hudPos;
    Transform target;                                   // ���� Ÿ������
    private float moveSpeed = 7.5f;
    private Image healthBar;                  // �� ü�¹�(�ʿ�� �߰�)
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    public float enemyHealth;
    public AudioClip enemyDestroySound;

    [SerializeField]    private int baseDamage;         // �⺻���� ������
    [SerializeField]    private int maceDamage;         // ö����� ������
    [SerializeField]    private int poisonDamage;       // �� ���� ������
    private int poisonLabLv;                                // �� ����
    private int poisoningTime;                              // �ߵ� �����̻� ���ӽð�

    private void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        color = sprite.color;
        poisonLabLv = SaveManager.instance.skillLabLvStat[3];
    }
    private void Start()
    {
        //healthBar = GetComponent<Image>();
        enemyHealth = maxHealth;                // �� �ִ�ü�� �ʱ�ȭ
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();  // Ÿ���� �÷��̾�� ����
        if (poisonLabLv <= 2)                       // �� ���ӽð� ����(Ư��)
        {
            poisoningTime = 3;
        }else if(poisonLabLv <= 4)
        {
            poisoningTime = 3 + (poisonLabLv-2);
        }
    }
    private void Update()
    {
        baseDamage = SaveManager.skillLvStat[1] * 50;
        maceDamage = SaveManager.skillLvStat[2] * 10;
        poisonDamage = SaveManager.skillLvStat[5] * 2;
        if (enemyHealth <= 0)           // �� ����� ���̵��� ���� �� ����Ǯ�� ų�� ī��Ʈ
        {
            DestroyEnemy();
            if ((SceneManager.GetActiveScene().name == "Stage1") || (SceneManager.GetActiveScene().name == "Stage2") || (SceneManager.GetActiveScene().name == "Stage3"))
            {
                EnemySpawnPool.count_instance.EnemyKilledCount++;
            }else if ((SceneManager.GetActiveScene().name == "Stage1Hard") || (SceneManager.GetActiveScene().name == "Stage2Hard") || (SceneManager.GetActiveScene().name == "Stage3Hard"))
            {
                EnemySpawnPoolHard.count_instance.EnemyKilledCount++;
            }
            SoundManager.SoundEffect.SoundPlay("EnemyDestroySound", enemyDestroySound);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Eraser"))             // ������Ʈ ���찳
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Elec"))               // �� ���� ������Ʈ �ǰ�
        {
            enemyHealth -= 10f;
        }
        if (collision.CompareTag("Poison"))             // �� ���� �ǰ� ��
        {
            StartCoroutine("PoisonDamage");         // �� ������ �ڷ�ƾ
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("WebLv4"))         // �Ź��� Ʈ���� �ǰ� �� ü�°���
        {
            enemyHealth -= 0.5f;
        }
    }
    public void Skill1Damage()
    {
        enemyHealth -= baseDamage;
        DamageText(baseDamage);
        StartCoroutine("KnockBack");
    }
    public void Skill2Damage()
    {
        enemyHealth -= maceDamage;
        DamageText(maceDamage);
        EnemyDamaged();
        Invoke("EnemyCanDamage", 0.5f);             // �ǰ� ���ð�
    }
    public IEnumerator PoisonDamage()           // �� ������ �ڷ�ƾ�޼ҵ�
    {
        for (int i = 0; i < poisoningTime; i++)
        {
            sprite.color = new Color(1, 0, 1, 1);
            enemyHealth -= poisonDamage;
            DamageText(poisonDamage);
            yield return new WaitForSeconds(1f);
        }
        sprite.color = color;
    }
    public void DestroyEnemy()              // ��� ��� �� Ȯ���� ��ȭ���
    {
        if (Random.Range(0, 5) == 0)
        {
            CoinDrop();
        }
        Destroy(gameObject);
    }
    public void DamageText(int damageText)          // �� �ǰݽ� �������ؽ�Ʈ ���
    {
        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = hudPos.position;
        hudText.GetComponent<DamageText>().damage = damageText;
    }
    public void CoinDrop()
    {
        GameObject Coin = Instantiate(coins);
        Coin.transform.position = hudPos.position;
    }
    public IEnumerator KnockBack()              // �� �ǰ� �� �˹� �ڷ�ƾ�޼ҵ�
    {
        float directionX = transform.position.x - target.position.x;
        float directionY = transform.position.y - target.position.y;
        if (directionX < 0) { directionX = 1; }
        else { directionX = -1; }
        if (directionY < 0) { directionY = -1; }
        else { directionY = 1; }
        float kb = 0;
        while (kb < 0.2f)
        {
            if (transform.rotation.y == 0)
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime * directionX * 50f);
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime * directionY * 50f);
            }
            else
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime * directionX * -50f);
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime * directionY * -50f);
            }
            kb += Time.deltaTime;
            yield return null;
        }
    }
    public void EnemyDamaged()
    {
        gameObject.tag = "EnemyDamaged";
    }
    public void EnemyCanDamage()
    {
        gameObject.tag = "Enemy";
    }
}