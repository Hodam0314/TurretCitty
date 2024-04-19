using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
   
    [Header("무기 종류")]
    [SerializeField] private bool isGun;
    [SerializeField] private bool isRifle;
    [SerializeField] private bool isHammer;
    [SerializeField] private bool isSword;

    [Header("무기별 이펙트 및 총알")]
    [SerializeField] private GameObject gunBullet;
    [SerializeField] private GameObject rifleBullet;
    [SerializeField] private GameObject hammerEp;   
    [SerializeField] private GameObject SwordEp;

    [Header("무기별 공격력 및 속도")]
    [SerializeField] private float gunDamage = 3f;
    [SerializeField] private float rifleDamage = 5f;
    [SerializeField] private float hammerDamage = 25f;
    [SerializeField] private float swordDamage = 10f;
    [SerializeField] private float cooltime = 0.3f;
    private float curtime;

    Player player;
    Vector3 moveDir;
    private Transform layerDynamic;
    public Transform pos;

    private void Start()
    {
        player = GameManager.Instance.GetPlayer();
    }

    private void Update()
    {
        Shooting();
    }

    private void Shooting()
    {
        if(curtime <= 0)
        {
            if (Input.GetKey(KeyCode.Z))
            {
            curtime = cooltime;
            Instantiate(gunBullet, pos.position, transform.rotation, layerDynamic);
            }
        }
        curtime -= Time.deltaTime;
    }





}
