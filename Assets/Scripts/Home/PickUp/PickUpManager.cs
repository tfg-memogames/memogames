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

	public static Dictionary<String, Dictionary<string, string>> house;

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
		initObjects ();
	}

	void initObjects(){
		
		house = new Dictionary<string, Dictionary<string, string>> ();

		Dictionary<string,string> aux;
		aux= new Dictionary<string, string> (); //hall
		aux.Add ("Floor1", "Toy");
		aux.Add ("Table", ".");
		house.Add ("Hall", aux);

		aux.Clear(); //main bedroom
		aux.Add ("Floor2", "Pillow");
		aux.Add ("Floor3", "Shirt");
		aux.Add ("Floor4", "Shoe");
		aux.Add ("Bed", ".");
		aux.Add ("Closet", ".");
		house.Add ("Main_bedroom", aux);

		aux.Clear(); //second bedroom
		aux.Add ("Floor1", "Towel");
		aux.Add ("Floor2", "Cuack");
		aux.Add ("Bed1", ".");
		aux.Add ("Bed2", ".");
		house.Add ("Second_bedroom", aux);

		aux.Clear(); //livingroom
		aux.Add ("Remote_control", "Floor4");
		aux.Add ("Floor2", "Pillow");
		aux.Add ("Floor1", "Chair");
		aux.Add ("Floor3", "Sock");
		aux.Add ("Table1", ".");
		aux.Add ("Table2", ".");
		aux.Add ("Floor4", ".");
		house.Add ("Livingroom", aux);

	}


	// Update is called once per frame
	void Update () {
	

	}


}
