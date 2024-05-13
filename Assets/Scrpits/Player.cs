using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
   
    Rigidbody2D rigid;
    Vector3 moveDir;
    Animator anim;
    private SpriteRenderer playersr;
    //PlayerHp playerHp;
    private bool isHit;
    private Sprite playersrDefault;

    [Header("플레이어 관련")]
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float curHp = 100f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject Boom;
    [SerializeField] private bool isLookRight = false;
    [SerializeField] private float money = 0f;

    [Header("쓰레기통")]
    [SerializeField] Transform layerDynamic;

    #region 싱글턴 예시
    //private void OnValidate() // 인스펙터에서 값이 변경되면 이 함수가 호출됨
    //{
    //    if (playerHp != null)
    //    {
    //        playerHp.getPlayerHp(curHp, maxHp);
    //    }
    //}
    #endregion

    private void Awake()
    {
        playersr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        curHp = maxHp;

        playersrDefault = playersr.sprite;
    }

    private void Start()
    {
        GameManager.Instance.SetPlayer(this);
    }
    private void Update()
    {
        moving();
        turning();
        playerAnimation();
        sendmoney();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameTag.Item.ToString()) //아이템을 먹었을경우 작동되는 코드
        {
            #region 플레이어가 아이템먹으면 바로 적용하던코드
            //Item itemSc = collision.GetComponent<Item>();
            //Item.ItemType itemType = itemSc.GetItemType();
            //if (itemType == Item.ItemType.Hp)
            //{
            //    curHp += 3f;
            //    if(curHp > maxHp)
            //    {
            //        curHp = maxHp;
            //    }
            //}
            //else if (itemType == Item.ItemType.Speed)
            //{
            //    moveSpeed += 1f;
            //    if(moveSpeed >= 10)
            //    {
            //        moveSpeed = 10;
            //    }
            //}

            //else if(itemType == Item.ItemType.Coin)
            //{
            //    money += 100f;
            //    if(money >= 9999)
            //    {
            //        money = 9999;
            //    }

            //}

            //Destroy(collision.gameObject);
            #endregion
            Item itemsc = collision.gameObject.GetComponent<Item>();
            itemsc.GetItem();
            
        }

    }
    private void moving() //키보드로 값을 입력받아 플레이어를 움직여주는 코드
    {
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        moveDir.y = Input.GetAxisRaw("Vertical") * moveSpeed;
        if(moveDir.x > 0)
        {
            isLookRight = true;
        }
        else if (moveDir.x < 0)
        {
            isLookRight = false;
        }

        transform.position += new Vector3(moveDir.x, moveDir.y, 0) * Time.deltaTime;
    }

    #region 플레이어 방향전환
    private void turning() //플레이어 스프라이트의 방향전환을 도와주는 코드
    {
        //if (moveDir.x < 0 && transform.localScale.x < 1)
        //{
        //    Vector3 scale = transform.localScale;
        //    scale.x *= -1;
        //    transform.localScale = scale;
        //}
        //else if (moveDir.x > 0 && transform.localScale.x > -1)
        //{
        //    Vector3 scale = transform.localScale;
        //    scale.x *= -1;
        //    transform.localScale = scale;
        //}
        if (moveDir.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(moveDir.x < 0)
        {
            transform.eulerAngles = new Vector3(0, -180.0f, 0);
        }
    }
    #endregion

    #region 플레이어 애니메이션 코드
    private void playerAnimation() //애니메이션 작동에 필요한 함수값을 받아주는 코드
    {
        anim.SetInteger("isMoving", (int)moveDir.x);
        anim.SetInteger("isJump", (int)moveDir.y);
    }
    #endregion

    public void Hit(float Damage) //플레이어가 피해를 입었을때 동작해주는 코드
    {
        if(!isHit)
        {
        isHit = true;
        curHp -= Damage;
        if (curHp <= 0)
        {
            Destroy(gameObject);
            GameObject obj = Instantiate(Boom, transform.position, Quaternion.identity, layerDynamic);
            Explosion objSc = obj.GetComponent<Explosion>();
            float sizeWidth = playersr.sprite.rect.width;
            objSc.SetAnimationSize(sizeWidth);
            GameManager.Instance.GameOver();
        }
            else
            {
                StartCoroutine(Hitcheck());
                StartCoroutine(hitEffect());
            }

        }
    }
    
    IEnumerator Hitcheck() //코루틴을 활용한 일정시간 무적
    {
        yield return new WaitForSeconds(5f);
        isHit = false;
    }

    IEnumerator hitEffect() //코루틴을 활용한 플레이어의 피해반환 이미지 변경
    {
        while (isHit)
        {
        yield return new WaitForSeconds(0.1f);
        playersr.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(0.1f);
            playersr.color = new Color(1, 1, 1, 1);
        }
        setSprite();
    }

    private void setSprite() //플레이어가 피해를입고 정상이 되었을때 스프라이트 변경해주는 코드
    {
        playersr.sprite = playersrDefault;
    }

    public (float _cur, float _max) GetPlayerHp() //튜플 , 2개이상의 값을 밖으로 전달 , 플레이어의 체력을 HP바에게 전달해주는 코드
    {
        return (curHp, maxHp);
    }

    private void sendmoney() //플레이어의 돈 상태를 게임매니저에게 알려주는 코드
    {
        GameManager.Instance.checkmoney(money);
    }

    public void HpRecovery()
    {
        curHp += 5f;
        if(curHp >= 100f)
        {
            curHp = 100f;
        }
    }

    public void SpeedUp()
    {
        moveSpeed += 1;
        if(moveSpeed >= 10)
        {
            moveSpeed = 10;
        }
    }

    public void UseCoin()
    {
        money += 100;
        if(money >= 10000)
        {
            money = 10000;
        }
    }

}
