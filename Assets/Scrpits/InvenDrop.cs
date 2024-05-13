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

        eventData.pointerDrag.transform.SetParent(transform); //�巡���Ҷ� ���� �������� �θ� �ڽ����� ����
        eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;//
    }
}
