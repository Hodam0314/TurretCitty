using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnlyDrag : MonoBehaviour, IDragHandler
{
    private RectTransform rect;
    Vector3 mousePos;
    Vector2 pos;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        mousePos = Input.mousePosition;
        pos = mousePos - rect.position;

    }


    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position - pos; 
    }

}
