using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class DropObject : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {


	public void OnPointerEnter(PointerEventData eventData) {
		//Debug.Log("Pointer enter " + this.name);
	}

	public void OnPointerExit(PointerEventData eventData) {
		//Debug.Log ("Pointer exit " + this.name);
	}

	public void OnDrop(PointerEventData eventData) {
		Debug.Log("Drop on " + this.name);

		DragObject d = eventData.pointerDrag.GetComponent<DragObject> ();

		if (d != null) {
			d.returnPoint = eventData.position;
		}
	}

}
