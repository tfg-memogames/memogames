using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Wait : MonoBehaviour {
	public Text t;
	public float wait;
	private float actualTime;
	private CharactersController c;
	// Use this for initialization
	void Start () {
		actualTime = 0;
		c= GameObject.Find ("CharacterController").GetComponent<CharactersController> ();
		text ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (actualTime < wait)
			actualTime += Time.deltaTime;
		else {
			if(c.airport)
				SceneManager.LoadScene ("Airport");
			else if(c.sleep)
				SceneManager.LoadScene ("Main_bedroom");
			else if (c.endDinner)
				SceneManager.LoadScene ("Living_room");
		}


	}
		
	void text(){
		if (c.airport)
			t.text = "Al llegar al aeropuerto...";
		else if(c.sleep)
			t.text = "Al día siguiente...";
		else if (c.endDinner)
			t.text = "Después de cenar...";
		
	}

}
