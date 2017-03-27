using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;


public class PickUpManager : MonoBehaviour {
	private int totalObjects;
	private static string file="house.json";
	//array objeto lugar en el que se encuentra

	private class HouseObject{
		public string ob;
		public string site;
	}

	private class Room{
		public HouseObject [] room;
	}

	private class House{
		public Room [] house;
	}


	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
		initObjects ();
	
	
	}

	void initObjects(){
		string h;

		string json=LoadJSONData ();

		h=JsonUtility.FromJson<string>(json);
		Debug.Log (h);

	}


	// Update is called once per frame
	void Update () {
	

	}


	public static string LoadJSONData(){

		string filePath=Path.Combine(Application.streamingAssetsPath, file);
		string json="";

		if (File.Exists (filePath)) {
			json = File.ReadAllText (filePath);
		}
			
		return json;
	}

}
