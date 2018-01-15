using IsoUnity;
using IsoUnity.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreHouse : EventManager
{
	private GameObject phone;
	//public Sequence s;
	private GameObject pickUp;
	private GameObject partner;

	private bool order;

	void Start(){
		order = false;
		pickUp = GameObject.Find ("PickUpManager");
		partner = GameObject.Find ("Partner");
		phone=GameObject.Find ("Phone");
	}

	public override void ReceiveEvent(IGameEvent ev){
		if (ev.Name.Equals("Store")){

			order=pickUp.GetComponent<PickUpManager> ().data();
			pickUp.GetComponent<PickUpManager> ().feedBack++;
		
			IsoSwitchesManager.getInstance ().getIsoSwitches ().getSwitch ("order").State = order;
	
		}
		if (ev.Name == "LoadHallWay"){

			SceneManager.LoadScene("HallWay");
		}
		if (ev.Name == "LoadRecipes"){
			CharactersController c = GameObject.Find ("CharacterController").GetComponent<CharactersController> ();
			c.endDinner = true;
			partner.SetActive (false);
			phone.gameObject.GetComponent<Transform> ().position = new Vector3(0,0,0);
		}

	}

	public override void Tick() {}


}
