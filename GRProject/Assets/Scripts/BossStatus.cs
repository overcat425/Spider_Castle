using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStatus : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Color color;
    public GameObject hudDamageText;
    public GameObject gene;

    public Transform hudPos;
    Transform target;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    public static float bossHealth;
    public AudioClip enemyDestroySound;

    [SerializeField] public int baseDamage;
    [SerializeField] public int maceDamage;
    [SerializeField] public int poisonDamage;
    private int poisonLabLv;
    private int poisoningTime;

    private void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        color = sprite.color;
        poisonLabLv = SaveManager.skill4LabLvInstance;
    }
    private void Start()
    {
        bossHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (poisonLabLv <= 2)
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
        baseDamage = SaveManager.skill1LvInstance * 30;
        maceDamage = SaveManager.skill2LvInstance * 10;
        poisonDamage = SaveManager.skill5LvInstance * 2;
        if (bossHealth <= 0)
        {
            DestroyEnemy();
            EnemySpawnPool.count_instance.EnemyKilledCount++;
            SoundManager.SoundEffect.SoundPlay("EnemyDestroySound", enemyDestroySound);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Eraser"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Elec"))
        {
            bossHealth -= 10f;
        }
        if (collision.CompareTag("Poison"))
        {
            StartCoroutine("PoisonDamage");
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
        Invoke("EnemyCanDamage", 0.5f);
    }
    public IEnumerator PoisonDamage()
    {
        for (int i = 0; i < poisoningTime; i++)
        {
            sprite.color = new Color(1, 0, 1, 1);
            bossHealth -= poisonDamage;
            DamageText(poisonDamage);
            yield return new WaitForSeconds(1f);
        }
        sprite.color = color;
    }
    public void DestroyEnemy()
    {
        GeneDrop();
        Destroy(gameObject);
    }
    public void DamageText(int damageText)
    {
        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = hudPos.position;
        hudText.GetComponent<DamageText>().damage = damageText;
        Debug.Log(damageText);
    }
    public void GeneDrop()
    {
        GameObject Gene = Instantiate(gene);
        Gene.transform.position = hudPos.position;
    }
    public void EnemyDamaged()
    {
        gameObject.tag = "EnemyDamaged";
    }
    public void EnemyCanDamage()
    {
        gameObject.tag = "Boss";
    }
}