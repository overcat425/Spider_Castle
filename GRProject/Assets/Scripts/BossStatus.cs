using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStatus : MonoBehaviour
{
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

    private void Start()
    {
        //healthBar = GetComponent<Image>();
        bossHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        baseDamage = SaveManager.skill1LvInstance * 30;
        maceDamage = SaveManager.skill2LvInstance * 20;
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