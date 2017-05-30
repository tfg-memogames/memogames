using System;
using System.Collections;
using System.Collections.Generic;
using Isometra;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreHouse : Isometra.EventManager
{

	private GameObject pickUp;

	void Start(){
		pickUp = GameObject.Find ("PickUpManager");
	}

	public override void ReceiveEvent(IGameEvent ev){
		if (ev.Name == "Store"){
			pickUp.GetComponent<PickUpManager> ().data();
			pickUp.GetComponent<PickUpManager> ().feedBack++;
			print ("store");
		}
		if (ev.Name == "LoadHallWay"){
			SceneManager.LoadScene("HallWay");
			print ("scene");
		}
	}

	public override void Tick() {}


}
