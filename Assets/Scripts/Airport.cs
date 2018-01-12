using IsoUnity.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Airport : EventManager
{

	public GameObject maria;

	void Start(){
		//ini g
	}

	public override void ReceiveEvent(IGameEvent ev)
	{

		if (ev.Name == "MariaApears"){
			maria.transform.position = new Vector3 (6.56F, 1.5F, 0);
		}

		if (ev.Name == "TheEnd"){
			SceneManager.LoadScene ("Credits");
		}

	}

	public override void Tick() {}


}

