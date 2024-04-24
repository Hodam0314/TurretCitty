using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    Player player;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float damage = 0.0f;
    private bool playerBullet = false;
    private bool turretBullet = false;
    private bool isright = false;

    private void Start()
    {
        Invoke("Destroybullet", 2);
    }

    private void Awake()
    {
        player = GameManager.Instance.GetPlayer();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameTag.Enemy.ToString())
        {
            Enemy enemySc = collision.GetComponent<Enemy>();
            Destroy(gameObject);
            enemySc.checkBullet();
            enemySc.Hit(damage);
        }
    }

    void Update()
    {
        moving();
    }

    private void moving()
    {
        if(transform.eulerAngles.y == 0) //x y z = eulerAngles를 이용해서 각도설정 가능.
        {
            transform.Translate(transform.right  * bulletSpeed * Time.deltaTime);
        }
        else if(transform.eulerAngles.y == 180)
        {
            transform.Translate(transform.right * -1 * bulletSpeed * Time.deltaTime);
        }
        //else if(transform.rotation.z == )
           
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void Destroybullet()
    {
        Destroy(gameObject);
    }
}
