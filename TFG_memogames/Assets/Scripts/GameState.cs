using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    public static GameState gameS;

    public enum Gender { M, F };


    private string _playerName;

    private Gender _playerGender;

    void Awake()
    {
        
        if (gameS == null)
        {
            gameS = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        

        this.playerName = "Alfred";
        this.playerGender = Gender.F;
    }

    public string playerName
    {
        get { return this._playerName; }
        set { this._playerName = value; }
    }

    public Gender playerGender
    {
        get { return this._playerGender; }
        set { this._playerGender = value; }
    }

}
