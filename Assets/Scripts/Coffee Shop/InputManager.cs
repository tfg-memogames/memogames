using System;
using System.Collections;
using System.Collections.Generic;
using Isometra;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputManager : Isometra.EventManager
{

    private Isometra.IGameEvent processing;
    public InputField _input;
    private GameState _gs;
    public GameObject mainCharacter;
    public GameObject phone;

    private ConversationLauncher maria;

    private void Start()
    {
        if(this._input != null) { 
            this._input.gameObject.SetActive(false);
            this.maria = GameObject.Find("Maria").GetComponent<ConversationLauncher>();
        }
        this.processing = null;
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
        if (ev.Name == "Player_Name_Input")
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
        else if(ev.Name == "Enable_Maria")
        {
            this.phone.SetActive(false);
            this.maria.active = true;
            //Añadir zoom a la cámara

        }

        else if (ev.Name == "Right_Destination")
        {
            Debug.Log("Right_Destination");
        }
        else if (ev.Name == "Wrong_Destination")
        {
            Debug.Log("Wrong_Destination");
        }
        else if (ev.Name == "Right_Time")
        {
            Debug.Log("Right_Time");
        }
        else if (ev.Name == "Wrong_Time")
        {
            Debug.Log("Wrong_Time");
        }
        else if (ev.Name == "Right_Suitcase")
        {
            Debug.Log("Right_Suitcase");

        }
        else if (ev.Name == "Wrong__Suitcase")
        {
            Debug.Log("Wrong_Suitcase");
        }

        else if(ev.Name == "Start_FTW")
        {
            Debug.Log("cargando");
            SceneManager.LoadScene("Video_tutorial");
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
