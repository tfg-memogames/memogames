using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private Vector3 cameraPosition;

	// Update is called once per frame
	void Update () {
		Vector3 v = Camera.main.GetComponent<Transform> ().position;
		Vector3 mouse=Input.mousePosition;

		if(mouse.y<Screen.height/9 && v.y>-2)
			Camera.main.GetComponent<Transform> ().position = new Vector3( v.x , v.y- (float)0.1, v.z);

		if(mouse.y>Screen.height-Screen.height/9 && v.y<2)
			Camera.main.GetComponent<Transform> ().position = new Vector3( v.x , v.y+ (float)0.1, v.z);


		v = Camera.main.GetComponent<Transform> ().position;
		mouse=Input.mousePosition;
		if(mouse.x<Screen.width/8 && v.x>-4)
				Camera.main.GetComponent<Transform> ().position = new Vector3( v.x - (float)0.1, v.y, v.z);

		if(mouse.x>Screen.width-Screen.width/8 && v.x<4)
				Camera.main.GetComponent<Transform> ().position = new Vector3( v.x + (float)0.1, v.y, v.z);
		

	}


}
