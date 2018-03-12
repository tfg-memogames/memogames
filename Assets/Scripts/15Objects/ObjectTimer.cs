using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTimer : MonoBehaviour {


    float timeLeft;
	// Use this for initialization
	void Start () {
        timeLeft = 1.5f + Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeLeft = 1.5f + Time.deltaTime;
            this.gameObject.SetActive(false);
        }
    }
}
