using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Player player;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDelay = 1.0f;
    [SerializeField] private float damage = 0.0f;
    private bool playerBullet = false;
    private bool turretBullet = false;
    private bool isright = false;
    private void Awake()
    {
        player = GameManager.Instance.GetPlayer();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameTag.Enemy.ToString())
        {
            Enemy enemySc = collision.GetComponent<Enemy>();
            enemySc.Hit(damage);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        moving();
    }

    private void moving()
    {
            transform.position += transform.right * Time.deltaTime * bulletSpeed;
            
        
    }

    public void SetbulletSpeed(float _bulletSpeed, bool _isright)
    {
        _bulletSpeed = bulletSpeed;
        _isright = isright;
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
