using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventsCall : EventManager {

	private RecipeAppearing r;
	public GameObject recipe;

	void Start(){
		r = recipe.GetComponent<RecipeAppearing>();
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

		if (ev.Name == "RecipeLoad")
		{
			SceneManager.LoadScene("SpaguettiRecipe");
		}

	}

	
	public override void Tick() {}
}
