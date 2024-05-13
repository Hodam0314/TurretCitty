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

    [Header("�÷��̾� ����")]
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float curHp = 100f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject Boom;
    [SerializeField] private bool isLookRight = false;
    [SerializeField] private float money = 0f;

    [Header("��������")]
    [SerializeField] Transform layerDynamic;

    #region �̱��� ����
    //private void OnValidate() // �ν����Ϳ��� ���� ����Ǹ� �� �Լ��� ȣ���
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
        if (collision.gameObject.tag == GameTag.Item.ToString()) //�������� �Ծ������ �۵��Ǵ� �ڵ�
        {
            #region �÷��̾ �����۸����� �ٷ� �����ϴ��ڵ�
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
    private void moving() //Ű����� ���� �Է¹޾� �÷��̾ �������ִ� �ڵ�
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

    #region �÷��̾� ������ȯ
    private void turning() //�÷��̾� ��������Ʈ�� ������ȯ�� �����ִ� �ڵ�
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

    #region �÷��̾� �ִϸ��̼� �ڵ�
    private void playerAnimation() //�ִϸ��̼� �۵��� �ʿ��� �Լ����� �޾��ִ� �ڵ�
    {
        anim.SetInteger("isMoving", (int)moveDir.x);
        anim.SetInteger("isJump", (int)moveDir.y);
    }
    #endregion

    public void Hit(float Damage) //�÷��̾ ���ظ� �Ծ����� �������ִ� �ڵ�
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
    
    IEnumerator Hitcheck() //�ڷ�ƾ�� Ȱ���� �����ð� ����
    {
        yield return new WaitForSeconds(5f);
        isHit = false;
    }

    IEnumerator hitEffect() //�ڷ�ƾ�� Ȱ���� �÷��̾��� ���ع�ȯ �̹��� ����
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

    private void setSprite() //�÷��̾ ���ظ��԰� ������ �Ǿ����� ��������Ʈ �������ִ� �ڵ�
    {
        playersr.sprite = playersrDefault;
    }

    public (float _cur, float _max) GetPlayerHp() //Ʃ�� , 2���̻��� ���� ������ ���� , �÷��̾��� ü���� HP�ٿ��� �������ִ� �ڵ�
    {
        return (curHp, maxHp);
    }

    private void sendmoney() //�÷��̾��� �� ���¸� ���ӸŴ������� �˷��ִ� �ڵ�
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
