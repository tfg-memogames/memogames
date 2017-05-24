using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadRoom : MonoBehaviour {

	public string room;
	public static string theRoom;
	public GameObject pickup;

	public GameObject[] roomSites;
	public static GameObject[] sites;

	public GameObject[] objects;
	public static Dictionary<string, string> dictionary;
	public static Dictionary<string, string> order;

	public static int corrects;
	public static int mistakes;
	public static int caught;

	void Awake () {
		theRoom = room;
		if (GameObject.Find("PickUpManager") == null){
			GameObject pum = Instantiate (pickup);
			pum.name="PickUpManager";
			DontDestroyOnLoad(pum);
		}

		sites=new GameObject[roomSites.Length]; 

		for (int i = 0; i < roomSites.Length; i++) {
			sites [i] = roomSites [i];
		}
		if (!room.Equals ("Hall")) {
			Button btn = GameObject.FindGameObjectWithTag ("Back").GetComponent<Button> ();
			btn.onClick.AddListener (TaskOnClick);	
		}

		dictionary = PickUpManager.house [room];
		order = PickUpManager.orderHouse [room];

		for (int i = 0; i < sites.Length; i++)
			if (!dictionary[sites [i].name].Equals ("."))
				foundObject (dictionary[sites [i].name]).transform.position = new Vector3(sites [i].transform.position.x, sites [i].transform.position.y, -0.1F);
			
	}

	void Start(){
		corrects = 0;
		mistakes = 0;
		caught = 0;
	}

	GameObject foundObject(string name){
		int i=0;

		while ( i < objects.Length && !objects [i].name.Equals(name))
			i++;

		return objects [i];
	}

	void TaskOnClick(){
		if(room.Equals("Hallway"))
			SceneManager.LoadScene("Hall");
		else
			SceneManager.LoadScene("Hallway");

		store ();
	}

	public static void store(){
		PickUpManager.house [theRoom]=dictionary;
		PickUpManager.totalMistakes+=mistakes;
		PickUpManager.totalCorrects+=corrects;
		PickUpManager.totalCaught +=caught;

	}

}
