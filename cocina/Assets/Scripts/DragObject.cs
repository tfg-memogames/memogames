using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class DragObject: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	[HideInInspector]
	public Vector2 returnPoint;

	[HideInInspector]
	public bool dropDone;

	[HideInInspector]
	private Transform firstParent;

	void Start() {
		this.gameObject.AddComponent<CanvasGroup>();
		dropDone = false;
	}

	public void OnBeginDrag(PointerEventData eventData) {
		Debug.Log ("Begin drag");

		returnPoint = this.transform.position;
	
		this.firstParent = this.transform.parent;
		this.transform.SetParent (this.transform.parent.parent.parent);

		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData) {
		//Debug.Log ("Dragging");
		this.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log ("End drag");

		this.transform.position = returnPoint;

		if (!dropDone) {
			this.transform.SetParent (firstParent);
			dropDone = false;
		}
		
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}
		
}