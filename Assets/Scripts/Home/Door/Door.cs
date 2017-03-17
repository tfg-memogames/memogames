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
        SceneManager.LoadScene(scene);
    }
    
}
