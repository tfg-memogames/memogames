using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class DragObject: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	[HideInInspector]
	public Vector2 returnPoint;

	[HideInInspector]
	public bool dropZone;

	[HideInInspector]
	public Transform parent;

	private Transform firstParent;

	void Start() {
		this.gameObject.AddComponent<CanvasGroup>();
		dropZone = false;
	}

	public void OnBeginDrag(PointerEventData eventData) {
		Debug.Log ("Begin drag");

		returnPoint = this.transform.position;
	
		this.firstParent = this.transform.parent;

		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData) {
		this.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log ("End drag");

		this.transform.position = returnPoint;

		if (dropZone) {
			this.transform.SetParent (parent);
			firstParent = parent;
			dropZone = false;
		}
		
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}
		
}