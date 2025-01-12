using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStatus : MonoBehaviour
{
    EnemySpawner enemySpawner;
    private SpriteRenderer sprite;
    private Color color;
    public GameObject hudDamageText;        // �ǰݽ� �������ؽ�Ʈ
    public GameObject gene;                     // ���� ��� �� �����ȭ

    public Transform hudPos;                    // �������� HUD ��ġ
    Transform target;
    [SerializeField]
    private float maxHealth;                        // ���� �ִ� ü��
    [SerializeField]
    public static float bossHealth;
    public AudioClip enemyDestroySound;

    [SerializeField] public int baseDamage;             // �⺻���� ������
    [SerializeField] public int maceDamage;             // ö����� ������
    [SerializeField] public int poisonDamage;           // �� ������
    private int poisonLabLv;                            // �� ��ų����
    private int poisoningTime;                          // �ߵ� �����̻� ���ӽð�

    private void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        color = sprite.color;
        poisonLabLv = SaveManager.instance.skillLabLvStat[3];
    }
    private void Start()
    {
        bossHealth = maxHealth;                 // ���� �ִ�ü�� �ʱ�ȭ
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (poisonLabLv <= 2)                   // �� ���ӽð� ���� (Ư��)
        {
            poisoningTime = 3;
        }
        else if (poisonLabLv <= 4)
        {
            poisoningTime = 3 + (poisonLabLv - 2);
        }
    }
    private void Update()
    {
        baseDamage = SaveManager.skillLvStat[1] * 50;             // �⺻���� ���
        maceDamage = SaveManager.skillLvStat[2] * 10;             // ö����� ���
        poisonDamage = SaveManager.skillLvStat[5] * 2;            // �� ���� ���
        if (bossHealth <= 0)
        {                                                               // ���� ��� ��
            DestroyEnemy();                                       // ����ü ���� �ʱ�ȭ ��
            EnemySpawner.count_instance.EnemyKilledCount++;   // ��ü ų���� +1 (����)
            SoundManager.SoundEffect.SoundPlay("EnemyDestroySound", enemyDestroySound);     // ���� �������
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
            bossHealth -= 10f;
        }
        if (collision.CompareTag("Poison"))             // �� ���� �ǰ� ��
        {
            StartCoroutine("PoisonDamage");         // �������� �ڷ�ƾ
        }
    }
    public void Skill1Damage()
    {
        bossHealth -= baseDamage;
        DamageText(baseDamage);
    }
    public void Skill2Damage()
    {
        bossHealth -= maceDamage;
        DamageText(maceDamage);
        EnemyDamaged();
    }
    public IEnumerator PoisonDamage()         // �ߵ� �ڷ�ƾ�޼ҵ�
    {
        for (int i = 0; i < poisoningTime; i++)
        {
            sprite.color = new Color(1, 0, 1, 1);
            bossHealth -= poisonDamage;
            DamageText(poisonDamage);
            yield return new WaitForSeconds(1f);            // 1�ʸ��� ������
        }
        sprite.color = color;
    }
    public void DestroyEnemy()              // ���� ����� ��ȭ���
    {
        GeneDrop();
        Destroy(gameObject);
    }
    public void DamageText(int damageText)              // �� �ǰݽ� ������ ���
    {
        GameObject hudText = Instantiate(hudDamageText, EnemySpawner.instance.texts);
        hudText.transform.position = hudPos.position;
        hudText.GetComponent<DamageText>().damage = damageText;
    }
    public void GeneDrop()              // ��ȭ��� �޼ҵ�
    {
        GameObject Gene = Instantiate(gene);
        Gene.transform.position = hudPos.position;
    }
    public void EnemyDamaged()              // ���� �ǰ� ���ð� ó���� 1
    {
        gameObject.tag = "EnemyDamaged";
        Invoke("EnemyCanDamage", 0.5f);
    }
    public void EnemyCanDamage()        // ���� �ǰ� ���ð� ó���� 2
    {
        gameObject.tag = "Boss";
    }
}