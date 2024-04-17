using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Hp,
        WeaponBox,
        Coin,
        Weapon,
    }

    [SerializeField] ItemType itemType;


    private void Update()
    {
        transform.position = transform.position;
    }

    public ItemType GetItemType()
    {
        return itemType;
    }
}
