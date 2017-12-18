using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarSelector : MonoBehaviour
{

    private GameState gameS;
    // Use this for initialization
    void Start()
    {
        this.gameS = GameObject.FindObjectOfType<GameState>();
    }

   string getLevel()
    {
        GameState.Level currentLevel = this.gameS.level;
        string level = "Null";
        switch (currentLevel)
        {
            case GameState.Level.Easy: level = "Easy_level"; break;
            case GameState.Level.Medium: level = "Medium_Level"; break;
            case GameState.Level.Hard: level = "Hard_Level"; break;
        }
        return level;
    }
    public void electtricCarClicked()
    {
        gameS.carType = GameState.Car.ELECTRIC;
        SceneManager.LoadScene(getLevel());
    }

    public void gasoilCarClicked()
    {
        gameS.carType = GameState.Car.GAS;
        SceneManager.LoadScene(getLevel());
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
