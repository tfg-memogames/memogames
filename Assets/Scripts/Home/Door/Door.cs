using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour {

    public string scene;

	private GameObject room;
	private GameObject pickUp;

	void Start(){
		room = GameObject.Find ("PickUp");
		pickUp = GameObject.Find ("PickUpManager");
	}

    void OnMouseDown()
    {
		pickUp.GetComponent<PickUpManager> ().totalDoorsOpened++;
		room.GetComponent<LoadRoom>().store ();
        SceneManager.LoadScene(scene);
		if (scene.Equals ("Kitchen")) {
			pickUp.GetComponent<PickUpManager> ().data();
		}
    }
    
}
