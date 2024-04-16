using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private bool isPlayer = false;
    private bool isMoving = false;
    private float moveSpeed = 3f;
    private float minrange = -5f;
    private float maxrange = 5f;
    private SpriteRenderer sr;

    Vector3 dir;

    private float moveTimer = 3f;
    [SerializeField] private float mobHp = 10f;
    [SerializeField] private GameObject Boom;
    [SerializeField] Transform layerDynamic;
    GameObject player;

    private void Start()
    {
        GetPlayer();
    }

    private void GetPlayer()
    {
        Player playerSc = GameManager.Instance.GetPlayer();
        if (playerSc != null)
        {
            player = playerSc.gameObject;
        }
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
    }

    private void moving()
    {
        Vector3 plyer = player.transform.position;
        Vector3 Enemy = transform.position;

        dir = plyer - Enemy;//����
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
        if (mobHp <= 0f)
        {
            Destroy(gameObject);
            GameObject obj = Instantiate(Boom, transform.position, Quaternion.identity, layerDynamic);
            Explosion objSc = obj.GetComponent<Explosion>();
            float sizeWidth = sr.sprite.rect.width;
            objSc.SetAnimationSize(sizeWidth);
        }


    }
} 
