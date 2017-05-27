using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour {


	public GameObject obj;
	public int speed;
	public int near;

	private bool zm;
	private Vector3 o;
	private float zIniCamera;


    private void Awake()
    {
        this.zm = false;
    }


    void Start(){
		o = obj.GetComponent<Transform> ().position;
		zIniCamera = Camera.main.GetComponent<Transform> ().position.z;
        this.zm = false;
	}

	public void zoom(){
		zm = !zm;
	}


	// Update is called once per frame
	void Update () {

		if (zm) {
			Vector3 v = Camera.main.GetComponent<Transform> ().position;

			Camera.main.GetComponent<Transform> ().position = new Vector3 (o.x, o.y, v.z + 0.01F * speed);
		
			if (v.z > ((zIniCamera + o.z) / near))
				zoom ();
		}

	}

}
