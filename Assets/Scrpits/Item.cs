using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Hp,
        WeaponBox,
        Damage,
    }

    [SerializeField] ItemType itemType;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == GameTag.Player.ToString())
        {
            
        }
    }

    private void Update()
    {
        transform.position = transform.position;
    }

    public ItemType GetItemType()
    {
        return itemType;
    }
}
