using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RAGE.Analytics;


public class ChooseMecicine : MonoBehaviour {

	private int incorrect=0;
	private int correct=0;
	public Text text;
	private bool play = false;

	public void ini(){
		play = true;
		anunce ();
		Tracker.T.completable.Initialized("medicine");
	}

	void anunce(){
		text.text = "¿Qué medicina tienes que tomar?";
	}

	public void incorrectMedicine(){
		if (play) 
			incorrect++;
	}


	public void correctMedicine(){
		if (play) {
			correct++;
			text.text = "¡Muy bien!";
			exit ();
			GameObject.Find ("CharacterController").GetComponent<CharactersController> ().sleep = true;
			SceneManager.LoadScene ("Black");
		}
	}


	void exit(){
		Tracker.T.setVar ("IncorrectMedicine", incorrect);
		Tracker.T.setVar ("CorrectMedicine", correct);
		incorrect++;
		Tracker.T.completable.Completed("medicine", CompletableTracker.Completable.Level, (correct>0), correct/incorrect);
		GameObject.Find ("CharacterController").GetComponent<CharactersController> ().sleep = true;
	}
}
