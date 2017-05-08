using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FormManager : MonoBehaviour {


    public InputField _input;
    private GameState _gs;

    // Use this for initialization
    void Start () {
        this._gs = GameObject.FindObjectOfType<GameState>();

    }
	
	public void clickStart()
    {
        this._gs.name = _input.text;
        SceneManager.LoadScene("Level_Selector");
    }
}
