using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class video : MonoBehaviour {

// In this example we show how to invoke a coroutine and continue executing
// the function in parallel.

    // In this example we show how to invoke a coroutine and
    // continue executing the function in parallel.

    private IEnumerator coroutine;
	public GameObject panel;
	public Text text;
    private GameState _gs;
    void Start()
    {

        // Start function WaitAndPrint as a coroutine.
        this._gs = GameObject.FindObjectOfType<GameState>();
        coroutine = WaitAndPrint(38.0f);
        StartCoroutine(coroutine);

        
    }
	public void Repeat(){
		panel.gameObject.SetActive (false);
		StopAllCoroutines ();
		coroutine = WaitAndPrint(38.0f);
		StartCoroutine(coroutine);
			
	}

    // every 2 seconds perform the print()
    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
			panel.gameObject.SetActive (true);
			text.text="Jugar ahora";
        }
        
    }

	public void ChangeScene(){
        
		SceneManager.LoadScene(this._gs.level + "_Level");	
	}
}

