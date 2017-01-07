using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class DragObject: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	[HideInInspector]
	public Vector2 returnPoint;

	void Start() {
		this.gameObject.AddComponent<CanvasGroup>();
	}

	public void OnBeginDrag(PointerEventData eventData) {
		Debug.Log ("Begin drag");

		returnPoint = this.transform.position;

		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData) {
		//Debug.Log ("Dragging");
		this.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log ("End drag");

		this.transform.position = returnPoint;

		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}
		
}