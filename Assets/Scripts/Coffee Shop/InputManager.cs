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
    public GameObject mainCharacter;
    public GameObject phone;

    private void Start()
    {
        this._input.gameObject.SetActive(false);
        processing = null;
        this._gs = GameObject.FindObjectOfType<GameState>();
        this.phone.SetActive(false);
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
        else if(ev.Name == "Fernando_Leaves")
        {
            //Fernando se va
            if (this.mainCharacter != null)
                this.mainCharacter.SetActive(false);
            //Y le llaman al teléfono
            this.phone.SetActive(true);
        }
        else if(ev.Name == "Change_To_Maria_Scene")
        {
            //Cambia a escena con María.
            SceneManager.LoadScene("Conver with María");
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
