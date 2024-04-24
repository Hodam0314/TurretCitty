using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [Header("≈Õ∑ø º≥¡§")]
    [SerializeField] GameObject bullet;
    [SerializeField] float turretHp = 5f;
    [SerializeField] GameObject explosion;
    [SerializeField] Transform layerDynamic;

    Rigidbody2D rigid;
    SpriteRenderer spr;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == GameTag.Enemy.ToString())
        {
            Hit(1f);
        }
    }

    private void Hit(float _damage)
    {
        turretHp -= _damage;
        if(turretHp <= 0f)
        {
            Destroy(gameObject);
            GameObject obj = Instantiate(explosion, transform.position, Quaternion.identity, layerDynamic);
            Explosion objSc = obj.GetComponent<Explosion>();
            float sizeWidth = spr.sprite.rect.width;
            objSc.SetAnimationSize(sizeWidth);
        }
    }
}
