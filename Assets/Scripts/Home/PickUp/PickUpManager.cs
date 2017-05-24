using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;


public class PickUpManager : MonoBehaviour {
	
	public static Dictionary<string, Dictionary<string, string>> orderHouse;
	public static Dictionary<string, Dictionary<string, string>> house;

	public static Time t;
	public static int totalMistakes;
	public static int totalCorrects;
	public static int totalCaught;
	public static int totalDoorsOpened;

	public static int totalObjects;

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
		initObjects ();
		orderObjects ();
	}

	void Start(){
		totalMistakes = 0;
		totalCorrects = 0;
		totalCaught = 0;
		totalDoorsOpened = 0;
	}

	public static void initObjects(){
		totalObjects = 0;

		house = new Dictionary<string, Dictionary<string, string>> ();

		Dictionary<string,string> hall = new Dictionary<string, string> (); //hall
		hall.Add ("Mesa", "Abrigo");
		hall.Add ("Suelo1", "Llaves");
		hall.Add ("Suelo2", ".");
		hall.Add ("Perchero", ".");
		house.Add ("Hall", hall);
		totalObjects += 2;


		Dictionary<string,string> hallway = new Dictionary<string, string> (); //hall
		hallway.Add ("Suelo1", "Zapato");
		hallway.Add ("Suelo2", ".");
		hallway.Add ("Suelo3", "Maceta");
		hallway.Add ("Mesa", ".");
		house.Add ("Hallway", hallway);
		totalObjects += 2;


		Dictionary<string,string> main = new Dictionary<string, string> (); //main bedroom
		main.Add ("Suelo1", "Almohada");
		main.Add ("Suelo2", "Jersey");
		main.Add ("Suelo3", "Cartera");
		main.Add ("Cama", ".");
		main.Add ("Armario", ".");
		main.Add ("Mesilla", ".");
		house.Add ("Main_bedroom", main);
		totalObjects += 3;


		Dictionary<string,string> second = new Dictionary<string, string> (); //second bedroom
		second.Add ("Suelo1", ".");
		second.Add ("Suelo2", ".");
		second.Add ("Suelo3", ".");
		second.Add ("Cama", ".");
		second.Add ("Mesa1", "Papeles");
		second.Add ("Mesa2", "Boli");
		house.Add ("Second_bedroom", second);
		totalObjects += 2;


		Dictionary<string,string> living = new Dictionary<string, string> (); //livingroom
		living.Add ("Suelo1", ".");
		living.Add ("Suelo2", "Tablet");
		living.Add ("Sofa1", "Mando");
		living.Add ("Sofa2", "Manta");
		living.Add ("Mesa1a", ".");
		living.Add ("Mesa1b", ".");
		living.Add ("Mesa2", "Libro");
		living.Add ("Estanteria", ".");
		house.Add ("Livingroom", living);
		totalObjects += 4;

		Dictionary<string,string> bath = new Dictionary<string, string> (); //bathroom
		bath.Add ("Suelo1", "Albornoz");
		bath.Add ("Suelo2", "Champu");
		bath.Add ("Suelo3", "Secador");
		bath.Add ("Lavabo", "Toalla");
		bath.Add ("Colgador", ".");
		bath.Add ("Toallero", ".");
		bath.Add ("Estante", ".");
		bath.Add ("Ducha", ".");
		house.Add ("Bathroom", bath);
		totalObjects += 4;

	}
	public static void orderObjects(){
		orderHouse = new Dictionary<string, Dictionary<string, string>> ();

		Dictionary<string,string> hall = new Dictionary<string, string> (); //hall
		hall.Add ("Mesa", "Llaves");
		hall.Add ("Suelo1", ".");
		hall.Add ("Suelo2", ".");
		hall.Add ("Perchero", "Abrigo");
		orderHouse.Add ("Hall", hall);

		Dictionary<string,string> hallway = new Dictionary<string, string> (); //hall
		hallway.Add ("Suelo1", ".");
		hallway.Add ("Suelo2", "Zapato");
		hallway.Add ("Suelo3", ".");
		hallway.Add ("Mesa", "Maceta");
		orderHouse.Add ("Hallway", hallway);


		Dictionary<string,string> main = new Dictionary<string, string> (); //main bedroom
		main.Add ("Suelo1", ".");
		main.Add ("Suelo2", ".");
		main.Add ("Suelo3", ".");
		main.Add ("Cama", "Almohada");
		main.Add ("Armario", "Jersey");
		main.Add ("Mesilla", "Cartera");
		orderHouse.Add ("Main_bedroom", main);

		Dictionary<string,string> second = new Dictionary<string, string> (); //second bedroom
		second.Add ("Suelo1", ".");
		second.Add ("Suelo2", ".");
		second.Add ("Suelo3", ".");
		second.Add ("Cama", ".");
		second.Add ("Mesa1", "Papeles");
		second.Add ("Mesa2", "Boli");
		orderHouse.Add ("Second_bedroom", second);

		Dictionary<string,string> living = new Dictionary<string, string> (); //livingroom
		living.Add ("Suelo1", ".");
		living.Add ("Suelo2", ".");
		living.Add ("Sofa1", ".");
		living.Add ("Sofa2", ".");
		living.Add ("Mesa1a", "Tablet");
		living.Add ("Mesa1b", "Manta");
		living.Add ("Mesa2", "Mando");
		living.Add ("Estanteria", "Libro");
		orderHouse.Add ("Livingroom", living);

		Dictionary<string,string> bath = new Dictionary<string, string> (); //bathroom
		bath.Add ("Suelo1", ".");
		bath.Add ("Suelo2", ".");
		bath.Add ("Suelo3", ".");
		bath.Add ("Lavabo", ".");
		bath.Add ("Colgador", "Albornoz");
		bath.Add ("Toallero", "Toalla");
		bath.Add ("Estante", "Secador");
		bath.Add ("Ducha", "Champu");
		orderHouse.Add ("Bathroom", bath);
				
	}

	public static void data(){
		print ("Corrects: "+totalCorrects);
		print ("Caught: "+totalCaught);
		print ("Mistakes: "+totalMistakes);
		print ("Objects: "+totalObjects);
		print ("Doors: "+totalDoorsOpened);
	}
}
