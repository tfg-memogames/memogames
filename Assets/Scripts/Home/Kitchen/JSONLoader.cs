using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string path = Application.streamingAssetsPath + "RecipeLevels.json";
	}
	
}

public class Recipe
{
    public float time;
    public StepJSON[] steps;
}

[System.Serializable]
public class StepJSON
{
    public string drag;
    public string drop;
    public string action;
    public string sprite;
}


