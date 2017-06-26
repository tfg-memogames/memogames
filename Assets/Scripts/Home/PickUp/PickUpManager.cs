using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using RAGE.Analytics;

public class PickUpManager : MonoBehaviour {
	
	private Dictionary<string, Dictionary<string, string>> _orderHouse;
	private Dictionary<string, Dictionary<string, string>> _house;

	private int _totalMistakes;
	private int _totalCorrects;
	private int _totalCaught;
	private int _totalDoorsOpened;
	private int _totalObjects;
	private int _feedBack;
	private float _time;
	private string _room;

	void Awake(){
		//print(Application.persistentDataPath);
		DontDestroyOnLoad (transform.gameObject);
		initObjects ();
		orderObjects ();
	}

	void Start(){
		
		_totalMistakes = 0;
		_totalCorrects = 0;
		_totalCaught = 0;
		_totalDoorsOpened = 0;
		_feedBack = 0;
		_room = ".";

		Tracker.T.setVar("TotalObjects", totalObjects);

		Tracker.T.completable.Initialized("house");

	}

	private void FixedUpdate(){
		_time += Time.deltaTime;
	
	}

	public void initObjects(){
		_totalObjects = 0;

		_house = new Dictionary<string, Dictionary<string, string>> ();

		Dictionary<string,string> hall = new Dictionary<string, string> (); //hall
		hall.Add ("Mesa", "Abrigo");
		hall.Add ("Suelo1", "Sombrero");
		hall.Add ("Suelo2", ".");
		hall.Add ("Perchero", ".");
		_house.Add ("Hall", hall);
		_totalObjects += 2;


		Dictionary<string,string> hallway = new Dictionary<string, string> (); //hall
		hallway.Add ("Suelo1", ".");
		hallway.Add ("Suelo2", ".");
		hallway.Add ("Suelo3", ".");
		hallway.Add ("Mesa", ".");
		_house.Add ("Hallway", hallway);
		_totalObjects += 0;


		Dictionary<string,string> main = new Dictionary<string, string> (); //main bedroom
		main.Add ("Suelo1", "Almohada");
		main.Add ("Suelo2", ".");
		main.Add ("Suelo3", ".");
		main.Add ("Cama", ".");
		main.Add ("Armario", ".");
		main.Add ("Mesilla", ".");
		_house.Add ("Main_bedroom", main);
		_totalObjects += 1;


		Dictionary<string,string> second = new Dictionary<string, string> (); //second bedroom
		second.Add ("Suelo1", "Osito");
		second.Add ("Suelo2", "Boli");
		second.Add ("Suelo3", "Papeles");
		second.Add ("Cama", ".");
		second.Add ("Mesa1", ".");
		second.Add ("Mesa2", ".");
		_house.Add ("Second_bedroom", second);
		_totalObjects += 3;


		Dictionary<string,string> living = new Dictionary<string, string> (); //livingroom
		living.Add ("Suelo1", ".");
		living.Add ("Suelo2", "Tablet");
		living.Add ("Sofa1", "Mando");
		living.Add ("Sofa2", ".");
		living.Add ("Mesa1a", ".");
		living.Add ("Mesa1b", ".");
		living.Add ("Mesa2", "Libro");
		living.Add ("Estanteria", ".");
		_house.Add ("Livingroom", living);
		_totalObjects += 3;

		Dictionary<string,string> bath = new Dictionary<string, string> (); //bathroom
		bath.Add ("Suelo1", "Albornoz");
		bath.Add ("Suelo2", "Champu");
		bath.Add ("Suelo3", "Toalla");
		//bath.Add ("Lavabo", "Toalla");
		bath.Add ("Colgador", ".");
		bath.Add ("Toallero", ".");
		bath.Add ("Estante", ".");
		bath.Add ("Ducha", ".");
		_house.Add ("Bathroom", bath);
		_totalObjects += 3;

	}
	public void orderObjects(){
		_orderHouse = new Dictionary<string, Dictionary<string, string>> ();

		Dictionary<string,string> hall = new Dictionary<string, string> (); //hall
		hall.Add ("Mesa", "Sombrero");
		hall.Add ("Suelo1", ".");
		hall.Add ("Suelo2", ".");
		hall.Add ("Perchero", "Abrigo");
		_orderHouse.Add ("Hall", hall);

		Dictionary<string,string> hallway = new Dictionary<string, string> (); //hall
		hallway.Add ("Suelo1", ".");
		hallway.Add ("Suelo2", ".");
		hallway.Add ("Suelo3", ".");
		hallway.Add ("Mesa", ".");
		_orderHouse.Add ("Hallway", hallway);


		Dictionary<string,string> main = new Dictionary<string, string> (); //main bedroom
		main.Add ("Suelo1", ".");
		main.Add ("Suelo2", ".");
		main.Add ("Suelo3", ".");
		main.Add ("Cama", "Almohada");
		main.Add ("Armario", ".");
		main.Add ("Mesilla", ".");
		_orderHouse.Add ("Main_bedroom", main);

		Dictionary<string,string> second = new Dictionary<string, string> (); //second bedroom
		second.Add ("Suelo1", ".");
		second.Add ("Suelo2", ".");
		second.Add ("Suelo3", ".");
		second.Add ("Cama", "Osito");
		second.Add ("Mesa1", "Papeles");
		second.Add ("Mesa2", "Boli");
		_orderHouse.Add ("Second_bedroom", second);

		Dictionary<string,string> living = new Dictionary<string, string> (); //livingroom
		living.Add ("Suelo1", ".");
		living.Add ("Suelo2", ".");
		living.Add ("Sofa1", ".");
		living.Add ("Sofa2", ".");
		living.Add ("Mesa1a", "Tablet");
		living.Add ("Mesa1b", ".");
		living.Add ("Mesa2", "Mando");
		living.Add ("Estanteria", "Libro");
		_orderHouse.Add ("Livingroom", living);

		Dictionary<string,string> bath = new Dictionary<string, string> (); //bathroom
		bath.Add ("Suelo1", ".");
		bath.Add ("Suelo2", ".");
		bath.Add ("Suelo3", ".");
		//bath.Add ("Lavabo", ".");
		bath.Add ("Colgador", "Albornoz");
		bath.Add ("Toallero", "Toalla");
		bath.Add ("Estante", ".");
		bath.Add ("Ducha", "Champu");
		_orderHouse.Add ("Bathroom", bath);
				
	}

	public bool data(){
		print ("Corrects: "+_totalCorrects);
		print ("Caught: "+_totalCaught);
		print ("Mistakes: "+_totalMistakes);
		print ("Objects: "+_totalObjects);
		print ("Doors: "+_totalDoorsOpened);
		print ("Time: " + _time);
		print ("FeedBack: " + _feedBack);

		Tracker.T.setVar("FeedBack", _feedBack);
		Tracker.T.setVar("Time", _time);
		Tracker.T.setVar("Corrects", _totalCorrects);
		Tracker.T.setVar("Mistakes", _totalMistakes);
		Tracker.T.setVar("Doors", _totalDoorsOpened);
		Tracker.T.completable.Completed("house", CompletableTracker.Completable.Level, (_totalCorrects==_totalObjects), _totalCorrects);

		return (_totalCorrects == _totalObjects);

	}


	public Dictionary<string, Dictionary<string, string>> orderHouse{
		get { return _orderHouse; }
		set { _orderHouse = value; }
	}

	public Dictionary<string, Dictionary<string, string>> house{
		get { return _house; }
		set { _house = value; }
	}

	public int totalCorrects{
		get { return _totalCorrects; }
		set { _totalCorrects= value; }
	}
		
	public int totalCaught{
		get { return _totalCaught; }
		set { _totalCaught= value; }
	}

	public int totalMistakes{
		get { return _totalMistakes; }
		set { _totalMistakes= value; }
	}

	public int totalDoorsOpened{
		get { return _totalDoorsOpened; }
		set { _totalDoorsOpened= value; }
	}

	public int totalObjects{
		get { return _totalObjects; }
		set { _totalObjects= value; }
	}

	public int feedBack{
		get { return _feedBack; }
		set { _feedBack= value; }
	}

	public string room{
		get { return _room; }
		set { _room= value; }
	}


}
