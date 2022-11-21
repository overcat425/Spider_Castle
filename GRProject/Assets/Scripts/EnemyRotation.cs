using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion dir = Quaternion.LookRotation(transform.position - target.position);
        Vector3 angle = Quaternion.RotateTowards(transform.rotation, dir, 2000 * Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(0, 180, angle.z);
    }
}
