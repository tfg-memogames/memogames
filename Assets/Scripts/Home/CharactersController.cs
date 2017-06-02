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
	}
	// Update is called once per frame
	void Update () {
		
	}
}
