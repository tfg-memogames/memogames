using UnityEngine;
using System.Collections;

public class Sleep : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void sleep(){
        foreach (Transform t in this.transform) {
            t.GetComponent<Eye> ().close ();
        }
    }

    public void wake()
    {
        foreach (Transform t in this.transform)
        {
            t.GetComponent<Eye>().open();
        }
    }
}
