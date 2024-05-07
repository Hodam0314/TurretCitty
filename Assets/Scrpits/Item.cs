using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Hp,
        Speed,
        Coin,
    }

    [SerializeField] ItemType itemType;

    public ItemType GetItemType()
    {
        return itemType;
    }
}
