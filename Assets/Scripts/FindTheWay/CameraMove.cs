using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public GameObject car;
    private bool _chaseCar = true;

	// Use this for initialization
	void Start () {
		Vector3 v=car.GetComponent<Transform> ().position;	
		this.gameObject.GetComponent<Transform> ().position= new Vector3(v.x, v.y, -30);
	}
	
	// Update is called once per frame
	void Update () {
        if(_chaseCar) { 
		    Vector3 v=car.GetComponent<Transform> ().position;	
		    this.gameObject.GetComponent<Transform> ().position= new Vector3(v.x, v.y, -30);
        }
    }


    public bool chase
    {
        get { return this._chaseCar; }
        set { this._chaseCar = value; }
    }
}
