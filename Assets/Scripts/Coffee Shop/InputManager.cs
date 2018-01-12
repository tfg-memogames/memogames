using IsoUnity;
using IsoUnity.Events;
using RAGE.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

///<summary>
///The Input Manager is where we define all the different input axes and game actions for our project (Coffe Shop).
///El Input Manager es donde defines todos los diferentes ejes de entrada y acciones del juego para tu proyecto (Coffe Shop).
///</summary>

public class InputManager : EventManager
{

    private IGameEvent processing;
    public GameObject inputPanel;
    public InputField _input;
    private GameState _gs;
    public GameObject mainCharacter;
    public GameObject phone;
    private ConversationLauncher maria;
    private Zoom _zoom;

    ///<summary>
    ///Start´s the processes of entry and exit of the game
    ///Inicializa los procesos de entrada y salida del juego
    ///</summary>
    private void Start()
    {        
        if(this._input != null) {
            this.inputPanel.SetActive(false);
            this.maria = GameObject.Find("Maria").GetComponent<ConversationLauncher>();
        }
        this.processing = null;
        this._gs = GameObject.FindObjectOfType<GameState>();
        this._zoom = GameObject.FindObjectOfType<Zoom>();
    }

    ///<summary>
    ///Update´s the processes of entry and exit of the game
    ///Actualiza los procesos de entrada y salida del juego
    ///</summary>
    private void Update()
    {
        if(processing != null && Input.GetKeyDown(KeyCode.Return) && _input.text != "")
        {
            finishEvent();
        }
    }

    //supongo que no se usa mirar
    public override void Tick() { }

    ///<summary>
    ///It is responsible for managing the changes corresponding to each event, from small changes to scenes
    ///Se encarga de administrar los cambios correspondientes a cada evento, desde cambios pequeños hasta de escenas
    ///<param IGameEvent = ev >
    ///Capture all actions and events within the game
    ///Captura todos las acciones y eventos dentro del juego
    ///</param>
    ///</summary>
    public override void ReceiveEvent(IGameEvent ev)
    {
        if (ev.Name == "Player_Name_Input")
        {
            processing = ev;
            this.inputPanel.gameObject.SetActive(true);
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
            //Añadir zoom a la cámara
            Camera.main.gameObject.GetComponent<Zoom>().enabled = true;
        }

        else if (ev.Name == "Right_Destination")
        {
            Tracker.T.Alternative.Selected("destination", "rightAnswer");
        }
        else if (ev.Name == "Wrong_Destination")
        {
            Tracker.T.Alternative.Selected("destination", "wrongAnswer");
        }
        else if (ev.Name == "Right_Time")
        {
            Tracker.T.Alternative.Selected("departure_time", "rightAnswer");
            this._gs.rightTime = true;
        }
        else if (ev.Name == "Wrong_Time")
        {
            Tracker.T.Alternative.Selected("departure_time", "wrongAnswer");
            this._gs.rightTime = false;
        }
        else if (ev.Name == "Right_Suitcase")
        {
            Tracker.T.Alternative.Selected("luggage", "rightAnswer");
        }
        else if (ev.Name == "Wrong__Suitcase")
        {
            Tracker.T.Alternative.Selected("luggage", "wrongAnswer");
        }

        else if(ev.Name == "Leave_Coffee_Shop")
        {
            SceneManager.LoadScene("Conver_Before_FTW");
        }

        else if(ev.Name == "Start_FTW")
        {             
            SceneManager.LoadScene("Video_tutorial");
        }
    }
    ///<summary>
    ///Ends events
    ///Finaliza los eventos
    ///</summary>
    private void finishEvent()
    {
        this._gs.playerName = _input.text;
        Game.main.eventFinished(processing);
        processing = null;
        this.inputPanel.gameObject.SetActive(false);
    }

    ///<summary>
    ///Starts Dialog With Maria
    ///Inicia el diálogo con María
    ///</summary>
    public void startDialogWithMaria()
    {
        maria.startDialog();
    }
}
