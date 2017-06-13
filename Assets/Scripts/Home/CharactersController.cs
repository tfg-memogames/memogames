using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour {

	private GameObject _partner;
	public LockDoor door;

	// Use this for initialization
	void Start () {
		this._partner = GameObject.Find ("Partner");

		_partner.SetActive (GameObject.Find ("PickUpManager").GetComponent<PickUpManager> ().ini);

		if(!GameObject.Find ("PickUpManager").GetComponent<PickUpManager> ().ini)
			door.openDoor();
		/*
		if (room.Equals ("Livingroom") && pum.GetComponent<PickUpManager>().endDinner) {
			//Camera.main.GetComponent<CameraController> ();
			b.SetActive (true);

			partner.SetActive (true);

			StartCoroutine(WaitAndDestroy());

		}
*/
	}
	// Update is called once per frame
	void Update () {
		
	}
}
