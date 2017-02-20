using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
	/*
	public struct cell{  
		public int value;  
		public int xCell;  
		public int yCell;  
	}  
		
	public string origin;
	public string destiny;

	private cell actual;
	private cell fin;
	private cell [][][] map; //primera y segunda coordenada X e Y del mapa, tercera coordenada elección de dirección del jugador


	// Use this for initialization
	void Start () {
		if(!loadMap (nmap))return;
		actual = localitation (origin);
		fin = localitation (destiny);

	}
	
	// Update is called once per frame
	void Update () {
		//if(actual.xCell==fin.xCell && actual.yCell==fin.yCell)
					

	}

	void onButtonDown(){
	
		actual.xCell = map [actual.xCell] [actual.yCell] [option].xCell;
		actual.yCell = map [actual.xCell] [actual.yCell] [option].yCell;
		actual.value +=  map [actual.xCell] [actual.yCell] [option].value;

	}


	bool loadMap(int nmap){

		switch (nmap) {
			case 1:map=load1();break;
			case 2:map=load2();break; 
			case 3:map=load3();break;
			case 4:map=load4();break;
			case 5:map=load5();break;
		}

	}


	cell [][][] load1(){
		cell [][][]t=new cell[10][10][3];

		t [1] [2] [3].value = 10; t [1] [2] [3].xCell = 2;t [1] [2] [3].yCell = 4;




		return t;
	}


	cell [][][] load2(){
	}

	cell [][][] load3(){
	}

	cell [][][] load4(){
	}

	cell [][][] load5(){
	}

	cell localitation(string s){
		cell c;
		c.value = 0;

		if (s.Equals ("casa")) {
			c.xCell = 3;
			c.yCell = 4;
		}

		if (s.Equals ("farmacia")) {
			c.xCell = 3;
			c.yCell = 4;
		}

		if (s.Equals ("colegio")) {
			c.xCell = 3;
			c.yCell = 4;
		}




		return c;
	}*/
}
