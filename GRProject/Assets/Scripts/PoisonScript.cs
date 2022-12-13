using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonScript : MonoBehaviour
{
    [SerializeField] private GameObject spitPoison;
    [SerializeField] private GameObject poison;
    void Start()
    {
        Invoke("DestroySpit", 0.5f);
        Invoke("SetTruePoison", 0.5f);
        Invoke("DestroyObject", 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void DestroySpit()
    {
        Destroy(spitPoison);
    }
    private void SetTruePoison()
    {
        poison.SetActive(true);
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
