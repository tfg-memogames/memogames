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
		float negative = -4;
		float positive =  4;

		if(mouse.x<Screen.width/7 && v.x>negative)
				Camera.main.GetComponent<Transform> ().position = new Vector3( v.x - (float)0.1, v.y, v.z);

		if(mouse.x>Screen.width-Screen.width/7 && v.x<positive)
				Camera.main.GetComponent<Transform> ().position = new Vector3( v.x + (float)0.1, v.y, v.z);
		

	}


}
