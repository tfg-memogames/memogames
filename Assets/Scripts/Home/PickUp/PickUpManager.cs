using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;


public class PickUpManager : MonoBehaviour {
	private int totalObjects;
	//array objeto lugar en el que se encuentra

	public static Dictionary<string, Dictionary<string, string>> house;

	void Awake(){
		
		DontDestroyOnLoad (transform.gameObject);
		initObjects ();

	}

	void initObjects(){
		
		house = new Dictionary<string, Dictionary<string, string>> ();

		Dictionary<string,string> hall = new Dictionary<string, string> (); //hall
		hall.Add ("Floor1", "Toy");
		hall.Add ("Floor2", ".");
		hall.Add ("Floor3", ".");
		hall.Add ("Table", ".");
		house.Add ("Hallway", hall);

		Dictionary<string,string> main = new Dictionary<string, string> (); //main bedroom
		main.Add ("Floor2", "Pillow");
		main.Add ("Floor3", "Phone");
		main.Add ("Floor1", ".");
		main.Add ("Bed", ".");
		main.Add ("Closet", ".");
		house.Add ("Main_bedroom", main);

		Dictionary<string,string> second = new Dictionary<string, string> (); //second bedroom
		second.Add ("Floor1", "Pen");
		second.Add ("Floor2", "Shoe");
		second.Add ("Floor3", ".");
		second.Add ("Bed", ".");
		second.Add ("Table", ".");
		house.Add ("Second_bedroom", second);

		Dictionary<string,string> living = new Dictionary<string, string> (); //livingroom
		//living.Add ("Floor1", "Remote_control");
		living.Add ("Floor1", ".");
		living.Add ("Armrest", ".");
		living.Add ("Low_table_top", ".");
		living.Add ("Low_table_down", ".");
		living.Add ("Table", ".");
		living.Add ("Table_down", ".");
		//living.Add ("Floor2", "Pillow");
		//living.Add ("Floor1", "Chair");
		//living.Add ("Floor3", "Sock");
		//living.Add ("Table1", ".");
		//living.Add ("Table2", ".");
		//living.Add ("Floor4", ".");
		house.Add ("Livingroom", living);

		Dictionary<string,string> bath = new Dictionary<string, string> (); //bathroom
		bath.Add ("Floor1", ".");
		house.Add ("Bathroom", living);

		Dictionary<string,string> kit = new Dictionary<string, string> (); //kitchen
		kit.Add ("Floor1", ".");
		kit.Add ("Floor2", ".");
		house.Add ("Kitchen", kit);


	}
		


}
