using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RAGE.Analytics;

public class changeScene : MonoBehaviour {

    public SettingsApp sa;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckAndChangeScene(string scene)
    {
        string type = PlayerPrefs.GetString("type", null);

        if (type == "A" || type == "B")
        {
            scene = "exit";
        }
        ChangeScene(scene);
    }

    public void ChangeScene(string scene)
    {
        Tracker.T.Accessible.Accessed(scene, AccessibleTracker.Accessible.Screen);
        if (scene != "exit")
            SceneManager.LoadScene(scene);
        else
        {
            sa.ExitGameConfirmed();
        }
            
    }
}