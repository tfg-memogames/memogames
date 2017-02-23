using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour {
	public Sprite back;
	public Sprite book;
	public GameObject note;
	private bool active;


	// Use this for initialization
	void Start () {
		active = false;
		note.gameObject.SetActive (false);
		this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		
		if (active) {
			note.gameObject.SetActive (true);
			this.GetComponent<Image>().sprite = back;
			active = false;
		} else {
			note.gameObject.SetActive (false);
			this.GetComponent<Image> ().sprite = book;
			active = true;
		}
	
	
	}
}
