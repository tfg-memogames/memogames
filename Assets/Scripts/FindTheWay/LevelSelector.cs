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
    }

    public void level1Clicked()
    {
        gameS.level = GameState.Level.Easy;
        SceneManager.LoadScene("Easy_Level");
    }

    public void level2Clicked()
    {
        gameS.level = GameState.Level.Medium;
        SceneManager.LoadScene("Medium_Level");
    }

    public void level3Clicked()
    {
        gameS.level = GameState.Level.Hard;
        SceneManager.LoadScene("Hard_Level");
    }

    public void tutorialClicked()
    {
        SceneManager.LoadScene("Video_tutorial");
    }

	public void Quit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
