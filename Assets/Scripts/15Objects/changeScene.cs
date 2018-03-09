using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RAGE.Analytics;

public class changeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeScene(string scene)
    {
        Tracker.T.Accessible.Accessed(scene, AccessibleTracker.Accessible.Screen);
        if(scene != "exit")            
            SceneManager.LoadScene(scene);
        else
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }

}
