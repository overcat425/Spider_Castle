using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
