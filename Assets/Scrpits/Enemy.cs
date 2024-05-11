using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rigid;
    Explosion boom;
    Camera maincam;
    private bool isPlayer = false;
    private bool isMoving = false;
    private float moveSpeed = 3f;
    private float minrange = -5f;
    private float maxrange = 5f;
    private float attackcool = 0f;
    private float movingtime = 0f;
    private float movingtimer = 0.5f;
    private SpriteRenderer spriteR;
    private bool bulletattack = false;
    private bool isattack = false;
    private float enemytime;
    private float enemytimer = 1f;
    private Vector3 worldpos;

    Vector3 dir;

    private float moveTimer = 3f;
    [Header("몬스터 설정")]
    [SerializeField] private float mobHp = 10f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private GameObject Boom;
    [SerializeField] Transform layerDynamic;
    [SerializeField] GameObject attackCheck;
    [SerializeField] private float attacktimer = 5f;
    [SerializeField] GameObject bulletHit;
    [SerializeField] private bool haveItem = false;
    //GameObject player;
    GameObject player;
    EnemyAttack enemyAttack;

    [Header("몬스터 타입")]
    [SerializeField] private bool Slime;
    [SerializeField] private bool Shark;

    [Header("피격 텍스트")]
    [SerializeField] Transform Canvas;
    [SerializeField] private GameObject dmgtext;



    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == GameTag.Player.ToString())
    //    {
    //        Hit(damage);
    //        Player playerSc = collision.GetComponent<Player>();
    //        playerSc.Hit(damage);
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == GameTag.Player.ToString())
        {
            Hit(damage);
            Player playersc = player.GetComponent<Player>();
            playersc.Hit(damage);
        }
    }

    private void Awake()
    {
        boom = GetComponent<Explosion>();
        spriteR = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        maincam = Camera.main;
        
    }

    private void Start()
    {
        GetPlayer();
        GetEnemyCheck();
    }

    private void GetPlayer()
    {
        Player playerSc = GameManager.Instance.GetPlayer();
        if (playerSc != null)
        {
            player = playerSc.gameObject;
        }
    }

    private void GetEnemyCheck()
    {
        enemyAttack = attackCheck.GetComponent<EnemyAttack>();
    }

    void Update()
    {
        if (player == null)
        {
            GetPlayer();
            return;
        }

        moving();
        turning();
        slimeattack();
        checkattack();
    }

    private void moving()
    {
        Vector3 plyer = player.transform.position;
        Vector3 Enemy = transform.position;

        dir = plyer - Enemy;//방향
        dir.Normalize();
        transform.position += dir * Time.deltaTime;
    }

    private void turning()
    {
        if (dir.x > 0 && transform.localScale.x < 1)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        else if (dir.x < 0 && transform.localScale.x > -1)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void Hit(float _damage)
    {
        mobHp -= _damage;
        StartCoroutine(enemyhit());
        worldpos = maincam.WorldToScreenPoint(transform.position);
        GameObject text = Instantiate(dmgtext, worldpos, Quaternion.identity,Canvas);
        Damage dmg = text.GetComponent<Damage>();
        dmg.SetDamage(_damage, transform.position);



        if (bulletattack == true)
        {
        Instantiate(bulletHit, transform.position, Quaternion.identity, layerDynamic);
        bulletattack = false;
        }
        if (mobHp <= 0)
        {
            Destroy(gameObject);
            GameObject obj = Instantiate(Boom, transform.position, Quaternion.identity, layerDynamic);
            Explosion objSc = obj.GetComponent<Explosion>();
            float sizeWidth = spriteR.sprite.rect.width;
            objSc.SetAnimationSize(sizeWidth);

            if (haveItem == true)
            {
                GameManager.Instance.CreateGameItem(transform.position);
            }
        }
    }

    IEnumerator enemyhit()
    {
        for(int i = 0; i <= 2; i++)
        {
        yield return new WaitForSeconds(0.1f);
        spriteR.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(0.1f);
        spriteR.color = new Color(1, 1, 1, 1);
        }
    }

    private void slimeattack()
    {
        if(enemyAttack.checkEnemy == true && Slime == true)
        {
            attackcool += Time.deltaTime;
            if (attackcool >= attacktimer)
            {
            Vector3 plyer = player.transform.position;
            Vector3 Enemy = transform.position;

            dir = plyer - Enemy;//방향
            dir.Normalize();
            rigid.AddForce(dir*7, ForceMode2D.Impulse); //AddForce 사용법 , ForceMode2D 중 Impulse = 한번에 확 밀어내는 힘 , Force = 지긋이 밀어주는 힘
                attackcool = 0f;
                isattack = true;
            }
        }


    }

    public void checkBullet()
    {
        bulletattack = true;
    }

    private void checkattack()
    {
        if(isattack == true)
        {
            enemytime += Time.deltaTime;
            if(enemytime >= enemytimer)
            {
                rigid.velocity = Vector2.zero;
                enemytime = 0f;
                isattack = false;
            }

        }
    }

    public void GetItem()
    {
        haveItem = true;
        spriteR.color = new Color(1, 0.3f, 1, 1);
    }

    public void SpawnLevelUp()
    {
        mobHp += 10f;
        damage += 3f;
    }


}
