using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenDrop : MonoBehaviour, IDropHandler
{
    [SerializeField] Button btn;
    [SerializeField] GameObject item;
    private RectTransform rect;
    InvenDrag invendrag;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        btn.onClick.AddListener(() =>
        {
            invendrag = GetComponentInChildren<InvenDrag>();
            if (invendrag != null)
            {
            invendrag.useItem();
            }
        });
    }

    

    private void Start()
    {
        Inventory.Instance.SetInvenDrop(this);

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || 
            eventData.pointerDrag.GetComponent<InvenDrag>() == null || 
            transform.childCount != 0) return;

        eventData.pointerDrag.transform.SetParent(transform); //드래그할때 잡은 데이터의 부모가 자신으로 설정
        eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;//
    }
}
