using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nest0 : MonoBehaviour
{
     [SerializeField]
    public GameObject[] Nest; //배열 종류? 갯수?
    private BoxCollider area; // 콜라이더 범위?
    public int count = 15; // 개수
    private List<GameObject> props = new List<GameObject>();

    void Start()
    {
        area = GetComponent<BoxCollider>();

        for (int i = 0; i < count; i++)
        {
            Nspon();
        }

        area.enabled = false; //다른 것들과 Trigger로서 충돌 처리가 일어날 수도 있기 때문에 Box Collider를 꺼준다.
    }

    void Nspon()
    {
        int selection = Random.Range(0, Nest.Length);

        GameObject selectedPrefab = Nest[selection];

        Vector3 NsponPos = GetRandomPosition();

        GameObject instance = Instantiate(selectedPrefab, NsponPos, Quaternion.identity);

        props.Add(instance);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = area.size;

        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);

        Vector3 NsponPos = new Vector3(posX, posY, 0);

        return NsponPos;
    }

    public void Reset() 
    {
        for (int i = 0; i < props.Count; i++) 
        {
            props[i].transform.position = GetRandomPosition();
            props[i].SetActive(true); 
        }
    }
}

