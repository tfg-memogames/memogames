using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
///<summary>
///The class that is responsible for selecting the type of car and the level of difficulty
///La clase que se encarga de seleccionar el tipo de coche y el nivel de dificultad
///</summary>   
public class CarSelector : MonoBehaviour
{
    private CursorMode cursorMode = CursorMode.Auto;
    private GameState gameS;
    // Use this for initialization
    void Start()
    {
        this.gameS = GameObject.FindObjectOfType<GameState>();
    }
    ///<summary>
    ///Returns the level in which we are
    ///Devuelve el nivel en que nos encontramos
    ///<returns>
    ///String level;
    /// </returns>  
    ///</summary>
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
    ///<summary>
    ///Select the type of electric car and load the level
    ///Selecciona el tipo de coche electico y carga el nivel
    ///</summary> 
    public void electtricCarClicked()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        gameS.carType = GameState.Car.ELECTRIC;
        SceneManager.LoadScene(getLevel());
    }
    ///<summary>
    ///Select the type of gasoline car and load the level
    ///Selecciona el tipo de coche gasolina y carga el nivel
    ///</summary> 
    public void gasoilCarClicked()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
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