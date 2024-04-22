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

    [Header("플레이어 관련")]
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float curHp = 100f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject Boom;
    [SerializeField] private bool isLookRight = false;

    [Header("쓰레기통")]
    [SerializeField] Transform layerDynamic;


    //private void OnValidate() // 인스펙터에서 값이 변경되면 이 함수가 호출됨
    //{
    //    if (playerHp != null)
    //    {
    //        playerHp.getPlayerHp(curHp, maxHp);
    //    }
    //}

    private void Awake()
    {
        playersr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        curHp = maxHp;
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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameTag.Item.ToString())
        {
            Item itemSc = collision.GetComponent<Item>();
            Item.ItemType itemType = itemSc.GetItemType();
            if (itemType == Item.ItemType.Hp)
            {
                curHp += 3f;
                if(curHp > maxHp)
                {
                    curHp = maxHp;
                }
            }
            else if (itemType == Item.ItemType.WeaponBox)
            {

            }
            Destroy(collision.gameObject);
        }

    }
    private void moving()
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
    private void turning()
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
    private void playerAnimation()
    {
        anim.SetInteger("isMoving", (int)moveDir.x);
        anim.SetInteger("isJump", (int)moveDir.y);
    }
    #endregion

    public void Hit(float Damage)
    {
        curHp -= Damage;
        if (curHp <= 0)
        {
            Destroy(gameObject);
            GameObject obj = Instantiate(Boom, transform.position, Quaternion.identity, layerDynamic);
            Explosion objSc = obj.GetComponent<Explosion>();
            float sizeWidth = playersr.sprite.rect.width;
            objSc.SetAnimationSize(sizeWidth);
        }
    }

    public (float _cur, float _max) GetPlayerHp() //튜플 , 2개이상의 값을 밖으로 전달
    {
        return (curHp, maxHp);
    }
}
