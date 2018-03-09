using RAGE.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractedTracker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {

            //Se comprueba si en el punto del mouse al hacer click hay colisión con algún objeto. Se devuelven todos los objetos en result.
            Collider2D[] result = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            int i = result.Length;
            if (i == 0)
            {
                Tracker.T.setVar("empty", 1);
            }
            else
            {
                while (i > 0)
                {
                    i--;
                    int id;
                    string objName = result[i].name;
                    Tracker.T.setVar(objName, 1);
                }
            }

            //Return the current Active Scene in order to get the current Scene's name
            Scene scene = SceneManager.GetActiveScene();
            string name = scene.name;


            if (scene.name == "15Objects")
            {
                GameObject a = GameObject.Find("A");
                if (a != null)
                {
                    name += "-A";
                }
                else
                {
                    name += "-B";
                }
            }

            Tracker.T.GameObject.Interacted(name);
        }
    }
}
