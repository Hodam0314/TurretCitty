using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.ReorderableList;
using UnityEditor.Build;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rigid;
    Explosion boom;
    Camera maincam;
    Player enemyplayer;

    private float attackcool = 0f;

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
        if (collision.gameObject.tag == GameTag.Player.ToString()) //collision에 닿은 게임오브젝트의 게임태그가 플레이어일경우 실행
        {
            enemyplayer = collision.gameObject.GetComponent<Player>(); //닿은 오브젝트에게서 Player 스크립트 정보를 enemyplayer에 담아줌
            enemyplayer.Hit(damage); //위에서 담은 정보를 활용하여 해당 Player 스크립트의 Hit 코드를 실행하는데 damage 값을 전달해서 실행함


        }
    }


    private void OnCollisionStay2D(Collision2D collision) // 위의 OnCollisionEnter2D함수와 동일하지만 해당함수는 Stay로써
        //OnCollisionStay2D란 콜라이더가 Stay = 머무르다 즉, 계속 닿고있을경우 지속적으로 실행해주는 함수이다.
    {
        if(collision.gameObject.tag == GameTag.Player.ToString()) 
        {
            enemyplayer = collision.gameObject.GetComponent<Player>();
            enemyplayer.Hit(damage);

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
        Player playerSc = GameManager.Instance.GetPlayer(); //싱글턴으로 만들어놓은 Player을 가져오는 코드
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
        Vector3 plyer = player.transform.position; //적의 transform 위치
        Vector3 Enemy = transform.position; // 나의 transform 위치

        dir = plyer - Enemy;//적과 나의 transform 위치를 뺌으로써 방향값을 가져옴
        dir.Normalize(); //가져온 방향값을 Normalize화 즉, 해당 방향값을 1의 값으로 변경
        transform.position += dir * Time.deltaTime; //만들어놓은 적의 방향으로 Time.deltaTime 만큼 움직이도록 설계
    }

    private void turning()
    {
        if (dir.x > 0 && transform.localScale.x < 1) //뭐임 얘 어떻게 동작하는거임??
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
        GameObject text = Instantiate(dmgtext, worldpos, Quaternion.identity, Canvas);
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
        for (int i = 0; i <= 2; i++)
        {
            yield return new WaitForSeconds(0.1f);
            spriteR.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.1f);
            spriteR.color = new Color(1, 1, 1, 1);
        }
    }

        private void slimeattack()
        {
            if (enemyAttack.checkEnemy == true && Slime == true)
            {
                attackcool += Time.deltaTime;
                if (attackcool >= attacktimer)
                {
                    Vector3 plyer = player.transform.position;
                    Vector3 Enemy = transform.position;

                    dir = plyer - Enemy;//방향
                    dir.Normalize();
                    rigid.AddForce(dir * 7, ForceMode2D.Impulse); //AddForce 사용법 , ForceMode2D 중 Impulse = 한번에 확 밀어내는 힘 , Force = 지긋이 밀어주는 힘
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
            if (isattack == true)
            {
                enemytime += Time.deltaTime;
                if (enemytime >= enemytimer)
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
