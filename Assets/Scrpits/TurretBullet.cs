using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    Rigidbody2D rigid;
    Turret turret;
    Enemy enemy;
    Vector3 dir;

    [Header("ÅÍ·¿ÃÑ¾Ë ¼³Á¤")]
    [SerializeField] float bulletSpeed;
    [SerializeField] float damage;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        turret = GameManager.Instance.GetTurret();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }
        Enemy enemySc = collision.GetComponent<Enemy>();
        Destroy(gameObject);
        enemySc.checkBullet();
        enemySc.Hit(damage);

    }

    private void Update()
    {
        moving();
    }

    private void moving()
    {
        rigid.velocity = dir * bulletSpeed;
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void GetPos(Vector3 _dir)
    {
        dir = _dir;
    }

}
