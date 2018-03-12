using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState15O : MonoBehaviour {

    public static GameState15O gameState;

    private bool _fileConfig;

    private void Awake()
    {
        if (gameState == null)
        {
            gameState = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (gameState != this)
        {
            Destroy(gameObject);
        }
        this._fileConfig = false;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool fileConfig
    {
        get { return this._fileConfig; }
        set { this._fileConfig = value; }
    }
}
