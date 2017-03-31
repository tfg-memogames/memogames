using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadRoom : MonoBehaviour {

	public string room;

	public GameObject[] roomSites;
	public static GameObject[] sites;

	public GameObject[] objects;
	public static Dictionary<string, string> dictionary;
	// Use this for initialization
	void Awake () {

		sites=new GameObject[roomSites.Length];

		for (int i = 0; i < roomSites.Length; i++) {
			sites [i] = roomSites [i];
		}

		Button btn = GameObject.FindGameObjectWithTag("Back").GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);	

		dictionary = PickUpManager.house [room];

		for (int i = 0; i < sites.Length; i++)
			if (!dictionary[sites [i].name].Equals ("."))
				foundObject (dictionary[sites [i].name]).transform.position = sites [i].transform.position;
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
		PickUpManager.house [room]=dictionary;
	}

}
