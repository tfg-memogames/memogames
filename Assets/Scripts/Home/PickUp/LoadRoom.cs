using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadRoom : MonoBehaviour {

	public string room;
	public GameObject[] sites;
	public GameObject[] objects;
	public Dictionary<string, string> d;
	// Use this for initialization
	void Start () {
		Button btn = GameObject.FindGameObjectWithTag("Back").GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);	

		d = PickUpManager.house [room];

		for (int i = 0; i < sites.Length; i++)
			if (!d [sites [i].name].Equals ("."))
				foundObject (d [sites [i].name]).transform.position = sites [i].transform.position;
	}

	GameObject foundObject(string name){
		int i=0;

		while ( i < objects.Length && !objects [i].name.Equals(name))
			i++;

		return objects [i];
	
	}

	void TaskOnClick(){
		SceneManager.LoadScene("Hallway");
		PickUpManager.house [room]=d;
	}

}
