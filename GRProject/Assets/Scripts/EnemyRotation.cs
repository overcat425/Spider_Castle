using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour          // �� ���� ; �÷��̾� �ٶ󺸱�
{
    Transform target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        Quaternion dir = Quaternion.LookRotation(transform.position - target.position);
        Vector3 angle = Quaternion.RotateTowards(transform.rotation, dir, 2000 * Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(0, 180, angle.z);
    }
}
