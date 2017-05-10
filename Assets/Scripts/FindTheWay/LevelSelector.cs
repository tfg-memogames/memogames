using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

    private GameState gameS;

	// Use this for initialization
	void Start () {
        this.gameS = GameObject.FindObjectOfType<GameState>();
        Debug.Log(gameS.playerName + " " + gameS.playerSurname);
    }

    public void level1Clicked()
    {
        gameS.level = 1;
        SceneManager.LoadScene("Easy_Level");
    }

    public void level2Clicked()
    {
        gameS.level = 2;
        SceneManager.LoadScene("Medium_Level");
    }

    public void level3Clicked()
    {
        gameS.level = 3;
        SceneManager.LoadScene("Hard_Level");
    }

    public void tutorialClicked()
    {
        SceneManager.LoadScene("TutorialFindTheWay");
    }
}
