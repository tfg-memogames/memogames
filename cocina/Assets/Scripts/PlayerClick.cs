using System.Collections;
using UnityEngine;

public class PlayerClick : MonoBehaviour {

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
				}
			}
		}
	}
}
