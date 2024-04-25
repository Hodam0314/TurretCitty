using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{

    [Header("터렛 설정")]
    [SerializeField] GameObject bullet;
    [SerializeField] float turretHp = 5f;
    [SerializeField] GameObject explosion;
    [SerializeField] Transform layerDynamic;
    [SerializeField] float scanRange;
    [SerializeField] float cooltime = 2;
    [SerializeField] Transform shootPos;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform closetarget;
    private float attacktime;
    Vector3 passDir;

    Rigidbody2D rigid;
    SpriteRenderer spr;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GameManager.Instance.SetTurret(this);
        closetarget = Getclosetarget();
        passdir();
    }

    private void Update()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        closetarget = Getclosetarget();
        Attack();
        checkEnemy();
    }

    Transform Getclosetarget()
    {
        Transform result = null;
        float diff = 100; //탐색하는 적 거리
        foreach(RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float direction = Vector3.Distance(myPos, targetPos);

            if(direction < diff)
            {
                diff = direction;
                result = target.transform;
            }

        }
        return result;
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

    private void Attack()
    {
        if(closetarget == null)
        {
            return;
        }

        attacktime += Time.deltaTime;
        if (attacktime > cooltime)
        {
            attacktime = 0f;
            Vector3 targetPos = closetarget.position;
            Vector3 dir = targetPos - transform.position;

            dir = dir.normalized;
            Instantiate(bullet, shootPos.position, Quaternion.FromToRotation(Vector3.up, dir), layerDynamic);
        }
    }
    
    private void checkEnemy()
    {
        if(closetarget == null)
        {
            return;
        }
        Vector3 enemyPos = closetarget.transform.position;
        Vector3 dir = enemyPos - transform.position;

        dir = dir.normalized;

        transform.eulerAngles = dir;
    }

    private void passdir()
    {
        Vector3 enemyPos = closetarget.transform.position;
        Vector3 dir = enemyPos - transform.position;

        passDir = dir.normalized;

        TurretBullet bulletsc = bullet.GetComponent<TurretBullet>();
        bulletsc.GetPos(passDir);
    }

}
