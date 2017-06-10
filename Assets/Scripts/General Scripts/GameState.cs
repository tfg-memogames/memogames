using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    public static GameState gameS;

    public enum Gender { M, F };
    public enum Level { Easy, Medium, Hard };

    //Player Name
    private string _playerName;
    //Player Surname
    private string _playerSurname;
    //Player Age
    private string _playerAge;
    //Player Gender
    private Gender _playerGender;

    //Player says the right time to Maria
    private bool _rightTime;


    /////////FindTheWay Information/////////////
    //Level selected
    private Level _level;

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
        this.playerSurname = "Pennyworth Wayne";
        this.playerGender = Gender.F;
        this.playerAge = "22";
        this._level = Level.Easy;
        this.carType = Car.ELECTRIC;
    }

    public string playerName
    {
        get { return this._playerName; }
        set { this._playerName = value; }
    }

    public string playerSurname
    {
        get { return this._playerSurname; }
        set { this._playerSurname = value; }
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

    public Level level
    {
        get { return this._level; }
        set { this._level = value; }
    }
    public string playerAge
    {
        get { return this._playerAge; }
        set { this._playerAge = value; }
    }

    public bool rightTime
    {
        get { return this._rightTime; }
        set { this._rightTime = value; }
    }

}
