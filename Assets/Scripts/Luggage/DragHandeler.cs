using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// https://www.youtube.com/watch?v=c47QYgsJrWc
public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    // Static because user can't drag more than one object at the same time
    public static GameObject itemBeingDragged;
    private Vector3 startPoint;
    private Transform startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        this.startPoint = transform.position;
        this.startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent != startParent) { 
            transform.position = startPoint;
        }
    }
}
