using IsoUnity.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ListenerMainroom : EventManager
{

	private GameObject phone;
	private GameObject luggage;
	private GameState gs;
	private bool maria=false;


	void Start(){
		//ini gs
		gs = GameObject.FindObjectOfType<GameState> ();

		if (gs.rightTime)
			maria = true;
	}

	public override void ReceiveEvent(IGameEvent ev)
	{
		phone = gameObject;
		CharactersController c = GameObject.Find ("CharacterController").GetComponent<CharactersController> ();

		if (ev.Name == "Colgar"){
			Destroy (phone);
		}

		if (ev.Name == "LoadLuggage") {
			SceneManager.LoadScene ("LuggageGame");
		}
	}

	public override void Tick() {}


}

