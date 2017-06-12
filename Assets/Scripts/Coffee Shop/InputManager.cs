using Isometra;
using RAGE.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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

    private void Start()
    {

        
        if(this._input != null) {
            //this.inputPanel = GameObject.Find("InputPanel");
            this.inputPanel.SetActive(false);
            //this._input.gameObject.SetActive(false);
            this.maria = GameObject.Find("Maria").GetComponent<ConversationLauncher>();
        }
        this.processing = null;
        this._gs = GameObject.FindObjectOfType<GameState>();
        this._zoom = GameObject.FindObjectOfType<Zoom>();
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
            //this.maria.active = true;
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

    private void finishEvent()
    {
        this._gs.playerName = _input.text;
        Game.main.eventFinished(processing);
        processing = null;
        this.inputPanel.gameObject.SetActive(false);

    }


    public void startDialogWithMaria()
    {
        maria.startDialog();
    }
}
