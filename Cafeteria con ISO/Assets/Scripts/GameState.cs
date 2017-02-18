using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Objeto que contiene las variables del estado del juego. Necesarias entre escenas.
public class GameState : MonoBehaviour {

    private string _playerName;
    public static GameState gameS; 


    private void Awake()
    {
        if (gameS == null)
        {
            gameS = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
        
    }

    // Use this for initialization
    void Start () {
        _playerName = "Protagonista";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string playerName
    {
        get { return this._playerName; }
        set { _playerName = value; }
    }

}
