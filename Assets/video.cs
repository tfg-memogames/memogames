using UnityEngine;
using System.Collections;

public class video : MonoBehaviour {

// In this example we show how to invoke a coroutine and continue executing
// the function in parallel.

    // In this example we show how to invoke a coroutine and
    // continue executing the function in parallel.

    private IEnumerator coroutine;

    void Start()
    {

        // Start function WaitAndPrint as a coroutine.

        coroutine = WaitAndPrint(45.0f);
        StartCoroutine(coroutine);

        
    }

    // every 2 seconds perform the print()
    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
        	CanvasManager.end=true;	
        }
        
    }
}

