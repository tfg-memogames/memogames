using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {


	public void OnBeginDrag(PointerEventData eventData) {
		Debug.Log ("Begin drag");
	}

	public void OnDrag(PointerEventData eventData) {
		Debug.Log ("Dragging");
		this.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log ("End drag");
	}
		
}