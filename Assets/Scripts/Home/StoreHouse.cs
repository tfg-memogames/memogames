using System;
using System.Collections;
using System.Collections.Generic;
using Isometra;
using UnityEngine;

public class StoreHouse : Isometra.EventManager
{

	private GameObject pickUp;

	void Start(){
		pickUp = GameObject.Find ("PickUpManager");
	}

	public override void ReceiveEvent(IGameEvent ev){
		if (ev.Name == "Store"){
			pickUp.GetComponent<PickUpManager> ().data();
		}
	}

	public override void Tick() {}


}
