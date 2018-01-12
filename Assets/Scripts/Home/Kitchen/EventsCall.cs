using IsoUnity.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventsCall : EventManager {

	private RecipeAppearing r;
	public GameObject recipe;

	void Start(){
	}

	public override void ReceiveEvent(IGameEvent ev)
	{
		if (ev.Name == "FirstStep")
		{
			recipe.GetComponent<RecipeAppearing> ().showFirstStep ();
		}

		if (ev.Name == "SecondStep")
		{
			recipe.GetComponent<RecipeAppearing>().showSecondtStep ();
		}

		if (ev.Name == "ThirdStep")
		{
			recipe.GetComponent<RecipeAppearing>().showThirdStep ();
		}

		if (ev.Name == "FourthStep")
		{
			recipe.GetComponent<RecipeAppearing>().showFourthStep ();
		}

		if (ev.Name == "RecipeLoad")
		{
			SceneManager.LoadScene("SpaguettiRecipe");
		}

	}

	
	public override void Tick() {}
}
