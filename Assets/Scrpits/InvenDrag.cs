using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InvenDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Image img;
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

    public void SetItem(Sprite _spr)
    {
        img.sprite = _spr;
    }
}
