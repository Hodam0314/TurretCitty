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
        if (Inventory.Instance.GetItem(sprite, itemType)) //�κ��丮 ��ũ��Ʈ�� GetItem�ڵ带 �����ؼ� ����� �����ϸ� ���� , �Ұ����ϸ� �۵����ϰ� ����
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
