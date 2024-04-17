using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
   
    [Header("���� ����")]
    [SerializeField] private bool isGun;
    [SerializeField] private bool isRifle;
    [SerializeField] private bool isHammer;
    [SerializeField] private bool isSword;

    [Header("���⺰ ����Ʈ �� �Ѿ�")]
    [SerializeField] private GameObject gunBullet;
    [SerializeField] private GameObject rifleBullet;
    [SerializeField] private GameObject hammerEp;
    [SerializeField] private GameObject SwordEp;

    [Header("���⺰ ���ݷ� �� �ӵ�")]
    [SerializeField] private float gunDamage = 3f;
    [SerializeField] private float rifleDamage = 5f;
    [SerializeField] private float hammerDamage = 25f;
    [SerializeField] private float swordDamage = 10f;

    Player player;
    Vector3 moveDir;

    [SerializeField]
    private Transform layerDynamic;

    private void Start()
    {
        player = GameManager.Instance.GetPlayer();
    }

    private void Update()
    {
        Position();
        Shooting();

    }

    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            createBullet(transform.position);
        }

    }

    private void Position()
    {
        //moveDir.x = Input.GetAxisRaw("Horizontal");
        //Vector3 pos = player.transform.position;
        //if (moveDir.x > 0)
        //{
        //    transform.position = new Vector3(-0.92f, -0.52f, 0);
        //}

    }

    private void createBullet(Vector3 _pos)
    {

        GameObject obj = Instantiate(gunBullet, _pos, Quaternion.identity, layerDynamic);
        Bullet bul = obj.GetComponent<Bullet>();
    }



}
