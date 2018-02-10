using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public float life;

    private float actualTime;
	// Use this for initialization
	void Start () {
        actualTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(actualTime < life)
            actualTime += Time.deltaTime;

        else
        {
            this.destrou
        }
	}
}
