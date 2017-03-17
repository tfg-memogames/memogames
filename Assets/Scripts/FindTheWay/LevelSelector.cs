using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

    /* public Button buttonLevel1;
     public Button buttonLevel2;
     public Button buttonLevel3;
     public Button buttonLevel4;
     public Button buttonLevelElectricCar;
     public Button buttonLevelGasCar;
     */

    public Button startButton;
    public GameState gameS;

	// Use this for initialization
	void Start () {
        startButton.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void level1Clicked()
    {
        Debug.Log("Button 1");
        gameS.level = 1;
    }

    public void level2Clicked()
    {
        Debug.Log("Button 2");
        gameS.level = 2;
    }

    public void level3Clicked()
    {
        Debug.Log("Button 3");
        gameS.level = 3;
    }

   

    public void electricCarClicked()
    {
        Debug.Log("Electric");
        gameS.carType = GameState.Car.ELECTRIC;
        startButton.enabled = true;

    }

    public void gasCarClicked()
    {
        Debug.Log("Gas");
        gameS.carType = GameState.Car.GAS;
        startButton.enabled = true;
    }

    public void startButtonClicked()
    {
        string level = "Level" + gameS.level;
        SceneManager.LoadScene(level);
        
    }

}
