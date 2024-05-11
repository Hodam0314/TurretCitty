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
    SpriteRenderer sr;
    Sprite sprite;




    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sprite = sr.sprite;
        
    }

    public void GetItem()
    {
        if (InventoryManager.Instance.GetItem(sprite))
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("�κ��丮�� ���� á���ϴ�");
        }
    }

    public ItemType GetItemType()
    {
        return itemType;
    }
}
