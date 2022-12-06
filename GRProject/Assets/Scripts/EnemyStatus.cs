using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    public GameObject hudDamageText;
    public GameObject coins;
    public Transform hudPos;
    Transform target;
    private float moveSpeed = 7.5f;
    private Image healthBar;                  // 적 체력바(필요시 추가)
    [SerializeField]
    private float maxHealth;
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
        baseDamage = SaveManager.skill1LvInstance * 30;
        maceDamage = SaveManager.skill2LvInstance * 20;
        if (enemyHealth <= 0)
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
            enemyHealth -= 10f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("WebLv4"))
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
        StartCoroutine("KnockBack");
        EnemyDamaged();
        Invoke("EnemyCanDamage", 0.5f);
    }
    public void DestroyEnemy()
    {
        if (Random.Range(0, 5) == 0)
        {
            CoinDrop();
        }
        Destroy(gameObject);
    }
    public void DamageText(int damageText)
    {
        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = hudPos.position;
        hudText.GetComponent<DamageText>().damage = damageText;
        Debug.Log(damageText);
    }
    public void CoinDrop()
    {
        GameObject Coin = Instantiate(coins);
        Coin.transform.position = hudPos.position;
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