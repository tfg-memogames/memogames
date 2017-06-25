using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medical : EventManager
{

	private GameObject parent;

	public override void ReceiveEvent(IGameEvent ev)
	{
		parent = gameObject;
		CharactersController c = GameObject.Find ("CharacterController").GetComponent<CharactersController> ();

		if (ev.Name == "Medical"){
			if (this.parent != null)
				Destroy (parent);

			c.medical = true;
		}
	}

	public override void Tick() {}


}
