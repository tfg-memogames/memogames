using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///<summary>
///It ensures that the cameras change directions following the rhythm of the car
///Se encarga de que las camaras cambien de direcciones siguiendo el ritmo del coche
///</summary>
public class CameraMove : MonoBehaviour {

	public GameObject car;
    public GameObject city; 
    private bool _chaseCar = true;

    ///<summary>
    // Use this for initialization
    ///</summary>
    void Start () {
	}
    ///<summary>
    // Update is called once per frame , following the position of the car
    ///<summary>
    void Update () {
        if(_chaseCar) { 
		    Vector3 v=car.GetComponent<Transform> ().position;	
		    this.gameObject.GetComponent<Transform> ().position= new Vector3(v.x, v.y, -30);
        }
        else
        {
            Vector3 v = city.GetComponent<Transform>().position;
            this.gameObject.GetComponent<Transform>().position = new Vector3(v.x, v.y, -45);
            this.gameObject.GetComponent<Transform>().position = new Vector3(v.x, v.y, -30);
        }
    }

    ///<summary>
    ///Set and get of chase
    ///<param bool = value >
    ///Identify when the car changes its way
    ///Identifica cuando el coche cambie de vía
    ///</param>
    ///</summary>
    public bool chase
    {
        get { return this._chaseCar; }
        set { this._chaseCar = value; }
    }
}
