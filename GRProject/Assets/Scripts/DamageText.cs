using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public Renderer rend;
    public float speed = 10;
    public float alphaSpeed = 1;
    public float destroyTime = 1.5f;
    public int damage;
    TextMeshPro text;
    Color alpha;
    private void OnEnable()
    {
        alpha.a = 1f;
        Invoke("DestroyObject", destroyTime);
    }
    void Start()
    {
        rend = GetComponent<Renderer>();
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        text.text = damage.ToString();
        SetLayer();
    }
    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }
    private void SetLayer()
    {
        rend.sortingOrder = 5;
    }
    private void DestroyObject()
    {
        gameObject.SetActive(false);
    }
}
