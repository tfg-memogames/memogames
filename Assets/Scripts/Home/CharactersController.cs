using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour {

	public GameObject kit;
	private LockDoor _door;
	private GameObject _partner;
	private string _room;

	private bool _ini;
	private bool _medical; 
	private bool _endDinner;
	private bool _sleep;
	private bool _airport;

	void Awake(){
		_ini = true;
		_medical = false;
		_endDinner = false;
		_sleep = false;
		_airport = false;
		DontDestroyOnLoad (gameObject);	
	}

	public void control(){
		_room = GameObject.Find ("PickUp").GetComponent<LoadRoom> ().room;

		if(GameObject.Find("Partner")){
			_partner = GameObject.Find ("Partner");
		}

		if (_room.Equals ("Hall") && !_ini) {
			Destroy (_partner);
			_door = GameObject.Find ("Door").GetComponent<LockDoor> ();
			_door.openDoor ();

		}

		if (_room.Equals ("Livingroom") && _endDinner) {
			//Camera.main.GetComponent<CameraController> ();
			//b.SetActive (true);
		
			_partner.transform.position = new Vector3 (0, -3.82F, 0);
			//_partner.GetComponent<ConversationLauncher> ().active = true;
			//StartCoroutine(WaitAndDestroy());
		}

		if (_room.Equals ("Main_bedroom") && _sleep) {
			GameObject.Find ("maleta").transform.position = new Vector3 (-1.29F, -1.61F, 0F);
			GameObject.Find ("Phone").transform.position = new Vector3 (0, 0, 0);
		}
	}

	public bool game(){
		return (_room.Equals ("Bathroom") && _medical);
	}

	public bool medical{
		get { return _medical; }
		set { _medical = value; }
	}

	public bool sleep{
		get { return _sleep; }
		set { _sleep = value; }
	}

	public bool ini{
		get { return _ini; }
		set { _ini= value; }
	}

	public bool endDinner{
		get { return _endDinner; }
		set { _endDinner= value; }
	}

	public bool airport{
		get { return _airport; }
		set { _airport= value; }
	}
}
