using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public struct cell{  
		public int value;  
		public int xCell;  
		public int yCell;
		public int mascarade;
	}  

	public string origin;
	public string destiny;
	public GameObject player;
	public Button left;
	public Button mid;
	public Button right;
	public GameObject canvas;

	private cell actual;
	private cell fin;
	private cell [][][] map; //primera y segunda coordenada X e Y del mapa, tercera coordenada elección de dirección del jugador


	// Use this for initialization
	void Start () {
		actual = localitation (origin);
		fin = localitation (destiny);
	
		hideButtons (actual.mascarade);
		player.transform.position = new Vector3 (actual.xCell, actual.yCell, 0);

		left.GetComponent<Button>().onClick.AddListener(clickLeft);
		right.GetComponent<Button>().onClick.AddListener(clickRight);
		mid.GetComponent<Button>().onClick.AddListener(clickMid);

	}

	void clickLeft(){
		canvas.SetActive (false);

		player.GetComponent<Animation> ().Play();


		Debug.Log ("you click left"); 
		myUpdate ();
	}

	void clickRight(){
		canvas.SetActive (false);
		Debug.Log ("you click right");
		player.transform.Translate (10,  - 10, 0);
		player.transform.Rotate (0, 0, 270);
		myUpdate ();
	}

	void clickMid(){
		canvas.SetActive (false);
		Debug.Log ("you click mid");
		player.transform.Translate (10, 0, 0);
		myUpdate ();
	}

	void myUpdate(){
		hideButtons (actual.mascarade);
		canvas.SetActive (true);
	}




	void LateUpdate () {
		if (actual.xCell == fin.xCell && actual.yCell == fin.yCell)
			Debug.Log ("Has llegado a tu destino!!");	
	}

	cell localitation(string site){
		cell c;
		c.yCell = 0; c.xCell = 0; c.value = 0; c.mascarade = 0;

		if (site.Equals ("casa")) {
			c.xCell = 45; c.yCell = -75;c.mascarade = 2;
		}
		if (site.Equals ("cafeteria")) {
			c.xCell = -75; c.yCell = 55;c.mascarade = 2;
		}
		if (site.Equals ("tienda")) {
			c.xCell = -75; c.yCell = 85;c.mascarade = 2;
		}
		if (site.Equals ("hospital")) {
			c.xCell = 75; c.yCell = 25;c.mascarade = 1;
		}
		if (site.Equals ("aeropuerto")) {
			c.xCell = -55; c.yCell = -85;c.mascarade = 3;
		}
		if (site.Equals ("maria")) {
			c.xCell = -35; c.yCell = 95;c.mascarade = 1;
		}
		if (site.Equals ("farmacia")) {
			c.xCell = 25; c.yCell = -5;c.mascarade = 2;
		}
		if (site.Equals ("colegio")) {
			c.xCell = 65; c.yCell = 65;c.mascarade = 3;
		}

		return c;

	}

	void hideButtons(int m){
		switch (m) {
		case 1:
			left.gameObject.SetActive (true);
			mid.gameObject.SetActive (true);
			right.gameObject.SetActive (true);
			break;
		case 2:
			left.gameObject.SetActive (true);
			mid.gameObject.SetActive (false);
			right.gameObject.SetActive (true);
			break;
		case 3:
			left.gameObject.SetActive (true);
			mid.gameObject.SetActive (true);
			right.gameObject.SetActive (false);
			break;
		case 4:
			left.gameObject.SetActive (false);
			mid.gameObject.SetActive (true);
			right.gameObject.SetActive (true);
			break;


		}
	}
}
