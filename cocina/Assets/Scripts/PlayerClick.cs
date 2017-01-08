using System.Collections;
using UnityEngine;

public class PlayerClick : MonoBehaviour {

	//Dragging attributes
	private bool dragging;
	private GameObject gameObjectToDrag;
	private Transform startPosition;
	private Vector3 touchPosition;
	private Vector3 offset;
	private Vector3 newCenterPosition;

	// Use this for initialization
	void Start () {
		
	}

	void Update() {
		RaycastHit hitInfo = new RaycastHit();
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		// OnMouseDown
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			if (Physics.Raycast(ray, out hitInfo)) {

				string tag = hitInfo.collider.gameObject.tag;

				Debug.Log (tag + "clicked");

				if (tag == "Cupboard") {
					Door d = hitInfo.collider.gameObject.GetComponent<Door> ();
					d.OpenCloseDoor ();

				} else if (tag == "Tap") {
					Tap t = hitInfo.collider.gameObject.GetComponent<Tap> ();
					t.OpenCloseTap ();

				} else if (tag == "Item") {
					gameObjectToDrag = hitInfo.collider.gameObject;
					startPosition = gameObjectToDrag.transform;
					touchPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					offset = touchPosition - startPosition.position;
					dragging = true;
				}
			}
		}

		if (Input.GetKey (KeyCode.Mouse0)) {
			if (dragging) {
				touchPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				newCenterPosition = touchPosition - offset;
				gameObjectToDrag.transform.position = new Vector3 (newCenterPosition.x, newCenterPosition.y, 0);
			}
		}

		//End of dragging
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			if (dragging) 
				dragging = false;
		}
	}
}
