using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


public class SpiderControl : MonoBehaviour
{
    public Transform bulletContainer;

    public float speed = 0.1f;
    public GameObject BulletPrefab;
    public float BulletSpeed;
    private int iteration = 0;
    public int angleInterval = 30;
    public int startAngle = 0;
    public int endAngle = 360;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MakeBullet());

        StartCoroutine(MakeBullet2());

        //StartCoroutine(MakeBullet3());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0, speed, 0);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0, -speed, 0);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(speed, 0, 0);
        }

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 1; i++)
            {
                GameObject Bullet = Instantiate(BulletPrefab);
                GameObject Bullet2 = Instantiate(BulletPrefab);
                GameObject Bullet3 = Instantiate(BulletPrefab);
                Vector3 bulletPosition = transform.position;

                bulletPosition.y += 0.5f * 1;
                Bullet.transform.position = bulletPosition;
                bulletPosition.y -= 0.5f * 1;
                Bullet2.transform.position = bulletPosition;
                bulletPosition.y += 2.0f * 1;
                Bullet3.transform.position = bulletPosition;
                Bullet3.transform.RotateAround(bulletPosition, Vector3.back,1.0f);

                Bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * BulletSpeed);
                Bullet2.GetComponent<Rigidbody2D>().AddForce(Vector2.down * BulletSpeed);
           //     Bullet3.GetComponent<Rigidbody2D>().AddForce(Vector2)

                Destroy(Bullet, 3);
                Destroy(Bullet2, 3);
                Destroy(Bullet3, 3);
            }
            
        } */
    }


    private IEnumerator MakeBullet()
    {
        int fireAngle = 0;
        while (true)
        {
            GameObject tempObject = Instantiate(BulletPrefab, bulletContainer, true);
            Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
            tempObject.transform.right = direction;
            tempObject.transform.position = transform.position;

            Destroy(tempObject, 1f);
            yield return new WaitForSeconds(0.1f);

            fireAngle += angleInterval;
            if (fireAngle > 360) fireAngle -= 360;
        }
    }

    private IEnumerator MakeBullet2()
    {
        while (true)
        {
            for (int fireAngle = startAngle; fireAngle <= endAngle; fireAngle += angleInterval)
            {
                GameObject tempObject = Instantiate(BulletPrefab, bulletContainer, true);
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));

                tempObject.transform.right = direction;
                tempObject.transform.position = transform.position;
                Destroy(tempObject, 1f);
            }

            yield return new WaitForSeconds(4f);
        }
    }

    private IEnumerator MakeBullet3()
    {
        while (true)
        {
            GameObject tempObject = Instantiate(BulletPrefab, bulletContainer, true);
            Vector3 bulletPosition = transform.position;
            bulletPosition.y += 1.0f * 1;
            tempObject.transform.position = bulletPosition;

            // tempObject.transform.position = transform.position;
            Destroy(tempObject, 1f);


            yield return new WaitForSeconds(1f);
        }
    }

}
