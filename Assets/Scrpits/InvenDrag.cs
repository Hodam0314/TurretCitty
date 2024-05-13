using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InvenDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Image img;
    [SerializeField] Item.ItemType itemType;
    Player player;
    private RectTransform rect;
    private Transform canvas;
    private Transform parentTrs;
    private CanvasGroup canvasgroup;


    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        canvasgroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Inventory.Instance.SetInvenDrag(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentTrs = transform.parent;

        transform.SetParent(canvas);
        transform.SetAsLastSibling();

        canvasgroup.alpha = 0.6f;
        canvasgroup.blocksRaycasts = false;
    }


    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(transform.parent == canvas)
        {
            transform.parent = parentTrs;
            rect.position = parentTrs.GetComponent<RectTransform>().position;
        }

        canvasgroup.alpha = 1f;
        canvasgroup.blocksRaycasts = true;
    }

    public void SetItem(Sprite _spr, Item.ItemType _type)
    {
        img.sprite = _spr;
        itemType = _type;
    }

    public void useItem()
    {
        if(itemType == Item.ItemType.Hp)
        {
            Inventory.Instance.hp();
            Destroy(gameObject);
        }

        else if(itemType == Item.ItemType.Speed)
        {
            Inventory.Instance.speed();
            Destroy(gameObject);
        }

        else if(itemType == Item.ItemType.Coin)
        {
            Inventory.Instance.coin();
            Destroy(gameObject);
        }
    }
}
