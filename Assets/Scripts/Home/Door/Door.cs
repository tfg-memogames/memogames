using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour {

    public string scene;

    void OnMouseDown()
    {
		PickUpManager.totalDoorsOpened++;
        SceneManager.LoadScene(scene);
		if (scene.Equals ("Kitchen")) {
			PickUpManager.data ();
		}
    }
    
}
