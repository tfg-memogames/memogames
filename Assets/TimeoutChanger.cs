using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeoutChanger : MonoBehaviour {

    public string scene;
    float time = 1f;

	// Use this for initialization
	void Start () {
        StartCoroutine(timeout());
	}
	
    public IEnumerator timeout()
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(scene);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
