using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medical : EventManager
{

	public GameObject parent;
	public GameObject pickup;

	public override void ReceiveEvent(IGameEvent ev)
	{
		PickUpManager p = pickup.GetComponent<PickUpManager> ();

		if (ev.Name == "Medical"){
			if (this.parent != null)
				this.parent.SetActive(false);

			p.medical = true;
		}
	}

	public override void Tick() {}


}
