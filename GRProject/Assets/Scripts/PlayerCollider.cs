using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)     // �÷��̾� �ݶ��̴�
    {
        if (other.CompareTag("Item"))
        {
            other.GetComponent<ItemBase>().Use(transform.parent.gameObject);
        }
    }
}