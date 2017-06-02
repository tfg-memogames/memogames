using System;
using System.Collections;
using System.Collections.Generic;
using Isometra;
using Isometra.Sequences;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreHouse : Isometra.EventManager
{
	//public Sequence s;
	private GameObject pickUp;

	private bool order;

	void Start(){
		order = false;
		pickUp = GameObject.Find ("PickUpManager");
	}

	public override void ReceiveEvent(IGameEvent ev){
		if (ev.Name.Equals("Store")){


			order=pickUp.GetComponent<PickUpManager> ().data();
			pickUp.GetComponent<PickUpManager> ().feedBack++;
			print ("store");

			IsoSwitchesManager.getInstance ().getIsoSwitches ().getSwitch ("order").State = order;
	
		}
		if (ev.Name == "LoadHallWay"){
			SceneManager.LoadScene("HallWay");
			print ("scene");
		}
		if (ev.Name == "LoadRecipes"){
			SceneManager.LoadScene("SpaguettiRecipe");
			print ("scene");
		}

	}

	public override void Tick() {}


}
