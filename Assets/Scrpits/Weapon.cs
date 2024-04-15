using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    [SerializeField]
    private Transform layerDynamic;


    private void Update()
    {
        Shooting();

    }

    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            createBullet(transform.position);
        }

    }


    private void createBullet(Vector3 _pos)
    {
        GameObject obj = Instantiate(gunBullet, _pos, Quaternion.identity, layerDynamic);
        Bullet bul = obj.GetComponent<Bullet>();
    }



}
