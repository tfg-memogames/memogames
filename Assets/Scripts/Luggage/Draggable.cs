using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// https://www.youtube.com/watch?v=bMuYUOIAdnc
[RequireComponent(typeof(CanvasGroup))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform parentToReturnTo;

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.startPosition = this.transform.position;
        this.parentToReturnTo = this.transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.transform.SetParent(parentToReturnTo);
        this.transform.position = startPosition;

    }

    // DropZone script calls this function
    public void ObjectDroppedCorrectly()
    {
        Destroy(this.gameObject);
    }
}
