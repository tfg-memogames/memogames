using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    private GameState gameS;
    private CursorMode cursorMode = CursorMode.Auto;
    // Use this for initialization
    void Start()
    {
        this.gameS = GameObject.FindObjectOfType<GameState>();
    }

    public void level1Clicked()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        gameS.level = GameState.Level.Easy;
        SceneManager.LoadScene("Car_Selector");
    }

    public void level2Clicked()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        gameS.level = GameState.Level.Medium;
        SceneManager.LoadScene("Car_Selector");
    }

    public void level3Clicked()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        gameS.level = GameState.Level.Hard;
        SceneManager.LoadScene("Car_Selector");
    }

    public void tutorialClicked()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
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