using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStaid : MonoBehaviour {

	public GameObject c;

	// Use this for initialization
	void Start () {
		
	}

	void OnMouseUp(){
		c.SetActive (true);
		bool game=GameObject.Find ("CharacterController").GetComponent<CharactersController> ().game ();

		if (game)
			c.GetComponent<ChooseMecicine> ().ini ();

	}

	// Update is called once per frame
	void Update () {
		
	}
}
