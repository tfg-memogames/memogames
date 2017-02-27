using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class DropObject : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {


	public void OnPointerEnter(PointerEventData eventData) {
	}

	public void OnPointerExit(PointerEventData eventData) {
	}

	public void OnDrop(PointerEventData eventData) {

		DragObject d = eventData.pointerDrag.GetComponent<DragObject> ();

		if (d != null) {
			d.returnPoint = eventData.position;
			d.dropZone = true;
			d.parent = this.transform;
		}
	}

}
