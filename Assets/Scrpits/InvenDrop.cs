using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvenDrop : MonoBehaviour, IDropHandler
{
    private RectTransform rect;
    [SerializeField] GameObject InventoryItem;


    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || 
            eventData.pointerDrag.GetComponent<InvenDrag>() == null || 
            transform.childCount != 0) return;

        eventData.pointerDrag.transform.SetParent(transform); //�巡���Ҷ� ���� �������� �θ� �ڽ����� ����
        eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;//
    }
}
