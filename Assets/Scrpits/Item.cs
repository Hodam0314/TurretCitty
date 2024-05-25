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
        GreenShild,
        PurpleHeart,
    }

    [SerializeField] ItemType itemType;
    [SerializeField] private bool isHp;
    [SerializeField] private bool isSpeed;
    [SerializeField] private bool isCoin;
    [SerializeField] private bool isGreenShild;
    [SerializeField] private bool isPurpleHeart;
    
    SpriteRenderer sr;
    Sprite sprite;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sprite = sr.sprite;
        
    }

    public void GetItem()
    {
        if (Inventory.Instance.GetItem(sprite, itemType)) //인벤토리 스크립트의 GetItem코드를 실행해서 등록이 가능하면 삭제 , 불가능하면 작동안하게 설정
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("인벤토리가 가득 찼습니다");
        }
    }

    public ItemType GetItemType()
    {
        return itemType;
    }
}
