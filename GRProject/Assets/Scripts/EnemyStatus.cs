using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    public GameObject hudDamageText;
    public Transform hudPos;
    Transform target;
    private float moveSpeed = 7.5f;
    private Image healthBar;                  // 적 체력바(필요시 추가)
    private float maxHealth = 100f;
    [SerializeField]
    public float enemyHealth;
    public AudioClip enemyDestroySound;

    [SerializeField]    public int baseDamage;
    [SerializeField]    public int maceDamage;

    private void Start()
    {
        //healthBar = GetComponent<Image>();
        enemyHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        baseDamage = SaveManager.skill1LvInstance * 10;
        maceDamage = SaveManager.skill2LvInstance * 20;
        //healthBar.fillAmount = enemyHealth / maxHealth;
        //if (enemyHealth <= 0f)
        //{
        //Destroy(gameObject);
        //}
        if (enemyHealth <= 0)
        {
            DestroyEnemy();
            EnemySpawnPool.count_instance.EnemyKilledCount++;
            SoundManager.SoundEffect.SoundPlay("EnemyDestroySound", enemyDestroySound);
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
        StartCoroutine("KnockBack");
        EnemyDamaged();
        Invoke("EnemyCanDamage", 0.5f);
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    public void DamageText(int damageText)
    {
        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = hudPos.position;
        hudText.GetComponent<DamageText>().damage = damageText;
        Debug.Log(damageText);
    }
    public IEnumerator KnockBack()
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
