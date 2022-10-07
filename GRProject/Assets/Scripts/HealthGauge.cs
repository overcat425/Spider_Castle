using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthGauge : MonoBehaviour
{
    private Image healthBar;
    private float maxHealth = 100f;
    public static float health;
    void Start()
    {
        healthBar = GetComponent<Image>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / maxHealth;
        if(health == 0f)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("GameOver");
        }
    }
}
