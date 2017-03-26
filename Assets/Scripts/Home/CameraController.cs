using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private Vector3 cameraPosition;
	private bool zoom;

	void Start(){
		zoom = false;
	}


	// Update is called once per frame
	void Update () {
		Vector3 v = Camera.main.GetComponent<Transform> ().position;
		Vector3 mouse=Input.mousePosition;
		float negative = zoom ? -8 : -4;
		float positive = zoom ? 8 : 4;


		if(mouse.y<100 && zoom && v.y>-3)
			Camera.main.GetComponent<Transform> ().position = new Vector3( v.x, v.y - (float)0.1, v.z);

		if(mouse.y>600 && zoom && v.y<3)
			Camera.main.GetComponent<Transform> ().position = new Vector3( v.x, v.y + (float)0.1, v.z);

		if(mouse.x<100 && v.x>negative)
				Camera.main.GetComponent<Transform> ().position = new Vector3( v.x - (float)0.1, v.y, v.z);

		if(mouse.x>1200 && v.x<positive)
				Camera.main.GetComponent<Transform> ().position = new Vector3( v.x + (float)0.1, v.y, v.z);
		

	}

	void OnMouseDown() {
		zoom = true;
		cameraPosition = Camera.main.GetComponent<Transform> ().position;
		Vector3 mouse=Input.mousePosition;

		Camera.main.GetComponent<Transform> ().position = new Vector3 (cameraPosition.x, cameraPosition.y, -5);

	}

	void OnMouseUp() {
		zoom = false;
		cameraPosition=Camera.main.GetComponent<Transform> ().position;
		float x=cameraPosition.x;

		if (x > 4)x = 4;
		if (x < -4)x = -4;

		Camera.main.GetComponent<Transform> ().position = new Vector3(x, 0, -10);
	}

}
