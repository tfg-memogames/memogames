using UnityEngine;

public class scr : MonoBehaviour {

	// Use this for initialization
	void Start () {

		string text = System.IO.File.ReadAllText("myfile.txt");
	
		Debug.Log (text);
		
	}

	// Update is called once per frame
	void Update () {
		
	}
		

}
