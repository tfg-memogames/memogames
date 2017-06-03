using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventsCall : EventManager {

	private RecipeAppearing r;

	void Start(){
		r = GameObject.Find ("Recipe").GetComponent<RecipeAppearing>();
	}

	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name == "FirstStep")
		{
			r.showFirstStep ();
		}

		if (ev.Name == "SecondStep")
		{
			r.showSecondtStep ();
		}

		if (ev.Name == "ThirdStep")
		{
			r.showThirdStep ();
		}

		if (ev.Name == "FourthStep")
		{
			r.showFourthStep ();
		}

		if (ev.Name == "Go")
		{
			SceneManager.LoadScene("SpaguettiRecipe");
		}

	}

	
	public override void Tick() {}
}
