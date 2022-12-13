using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watertrriger : MonoBehaviour
{
private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

     void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){

          //Player.position = new Vector3(Random.Range(4000, 5000), Random.Range(-4000, 4000), 0), Quaternion.identity);
            
            }
        }
    }


