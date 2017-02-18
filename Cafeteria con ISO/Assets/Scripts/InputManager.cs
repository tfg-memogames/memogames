using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputManager : EventManager
{

    private IGameEvent processing;
    public InputField _input;
    private GameState _gs;


    private void Start()
    {
        this._input.gameObject.SetActive(false);
        processing = null;
        this._gs = GameObject.FindObjectOfType<GameState>();
    }


    private void Update()
    {
        if(processing != null && Input.GetKeyDown(KeyCode.Return) && _input.text != "")
        {
            finishEvent();
        }
    }

    public override void Tick() { }
    public override void ReceiveEvent(IGameEvent ev)
    {
        if (ev.Name == "Player Name Input")
        {
            processing = ev;
            _input.gameObject.SetActive(true);

        }
        else if(ev.Name == "Change_To_Maria_Scene")
        {
            //Cambia a escena con María.
            SceneManager.LoadScene(1);


        }
    }

    private void finishEvent()
    {
        _gs.playerName = _input.text;
        Game.main.eventFinished(processing);
        processing = null;
        _input.gameObject.SetActive(false);

    }


    

    
}
