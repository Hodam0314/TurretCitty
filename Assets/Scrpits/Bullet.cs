using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDelay = 1.0f;
    private float damage = 0.0f;
    private bool playerBullet = false;
    private bool turretBullet = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameTag.Enemy.ToString())
        {
            Enemy enemysc = collision.GetComponent<Enemy>();
            enemysc.Hit(damage);
            Destroy(gameObject);
        }
    }


    void Update()
    {
        transform.position += transform.right * Time.deltaTime * bulletSpeed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void SetDamage(bool _isPlayer, float _damage, float _speed = -1)
    {
        playerBullet = _isPlayer;
        damage = _damage;
        if(_speed != -1)
        {
            bulletSpeed = _speed;
        }
    }

    public void turretDamage(bool _isTurret, float _damage, float _speed = -1)
    {
        turretBullet = _isTurret;
        damage = _damage;
        if(_speed != -1)
        {
            bulletSpeed = _speed;
        }
    }
}
