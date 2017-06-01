using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ClothesController))]
public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private ClothesController dropController;

    private void Start()
    {
        this.dropController = GetComponent<ClothesController>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            if (this.dropController.TargetWasDropped(d.gameObject))
            {
                d.ObjectDroppedCorrectly();
                //***Mostrar algo de feedback al usuario y hacer un peque delay despues
            } else
            {
                ////***Mostrar algo de feedback al usuario y hacer un peque delay despues
            }
        }
            
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
    }
}
