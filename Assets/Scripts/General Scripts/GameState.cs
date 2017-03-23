using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    public static GameState gameS;

    public enum Gender { M, F };

    //Player Name
    private string _playerName;
    //Player Gender
    private Gender _playerGender;


    /////////FindTheWay Information/////////////
    //Level selected
    private int _level;

    public enum Car { GAS, ELECTRIC};
    //Type of car
    private Car _carType;

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
        this.level = 1;
        this.carType = Car.ELECTRIC;
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

    public Car carType
    {
        get { return this._carType; }
        set { this._carType = value; }
    }

    public int level
    {
        get { return this._level; }
        set { this._level = value; }
    }




}
