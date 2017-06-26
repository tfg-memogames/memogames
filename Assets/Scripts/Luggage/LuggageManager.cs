using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RAGE.Analytics;

public class LuggageManager : MonoBehaviour {

    // This images shows the target
    public GameObject popUpPanel;

    public GameObject luggagePanel;
    public GameObject buttonCloseDrawer;
    public GameObject floorPanel;

    // Image inside the popUpPanel which shows the actual target
    private Image targetImage;
    private GameObject panelShow;

    // Clothes that the user has to move into the luggage
    private int numTargets;
    private Sprite[] targets;
    private int actualTarget;

    //Timer
    public GameObject timer;
    private Counter counter;
    // Time to complete the level
    public float time = 60.0f;
    private float actualTime = 0;

    // Is the game ended?
    private bool gameCompleted = false;
    // Can the user interact with objects in the scene? Example: when the pop up is open, player can't drag object
    private bool interactable;

    // Feedback when player does a correct drag or not
    private DisplayPanel displayPanel;
    public GameObject right;
    public GameObject wrong;

    // Button that oppens the popUpInfo
    public GameObject openInfoButton;

    // End of game panels
    public GameObject gameOver;
    public GameObject youWin;

    // Name of the level
    private string level;

    private void Awake()
    {
        this.right.SetActive(false);
        this.wrong.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        // Initialize targets
        this.actualTarget = 0;
        IntializeTargets();

        // Getting the displayPanel component
        displayPanel = GetComponent<DisplayPanel>();
   
        // Setup panels
        this.panelShow = null;
        this.buttonCloseDrawer.SetActive(false);
        this.luggagePanel.SetActive(false);
        this.floorPanel.SetActive(false);
        this.youWin.SetActive(false);
        this.gameOver.SetActive(false);
        this.targetImage = this.popUpPanel.transform.GetChild(0).GetComponent<Image>();
        ShowCurrentTarget();

        // Getting the counter component from the timer
        counter = timer.GetComponent<Counter>();

        // Setting the gameCompleted to false
        this.gameCompleted = false;

        // Tracker: NumberOfItem, time, lifes?
        this.level = SceneManager.GetActiveScene().name;

        Tracker.T.setVar("Tiempo", this.time);
        Tracker.T.setVar("NumPrendas", this.targets.Length);
        Tracker.T.Completable.Initialized(this.level);
    }

    void Update()
    {
        if (!gameCompleted && actualTime < time)
        {
            actualTime += Time.deltaTime;
            counter.PaintTheTime(actualTime, time);
        }
    }

    // This function initialize the target sprites getting the sprite of the image of luggagePanel
    private void IntializeTargets()
    {
        this.numTargets = this.luggagePanel.transform.childCount;
        this.targets = new Sprite[numTargets];
        for (int i = 0; i < numTargets; i++)
        {
            GameObject child = this.luggagePanel.transform.GetChild(i).gameObject;
            this.targets[i] = child.GetComponent<Image>().sprite;
        }
    }

    public void ShowFeedback(bool success)
    {
        GameObject instance = null;
        instance = (success) ? displayPanel.instantiatePanel(right) : displayPanel.instantiatePanel(wrong);
        instance.SetActive(true);
    }

    public void TargetCompleted()
    {

        this.actualTarget++;

        // Close drawer (target can be completed without oppening a drawer, opening the wardrove)
        CloseDrawer();

        if (this.actualTarget >= this.numTargets)
        {
            Debug.Log("You win");
            this.interactable = false;
            this.gameCompleted = true;
            this.youWin.SetActive(true);
            //Tracker: time, completed
            NotifyEndOfGameToTracker(true);
        } else
        {
            ShowFeedback(true);
            ShowCurrentTarget();
        }
    }

    private void ShowCurrentTarget()
    {
        this.popUpPanel.SetActive(true);
        this.openInfoButton.SetActive(false);
        this.interactable = false;
        this.targetImage.sprite = this.targets[this.actualTarget];
        this.targetImage.type = Image.Type.Simple;
        this.targetImage.preserveAspect = true;
    }

    public void ShowPopUpInfo()
    {
        this.popUpPanel.SetActive(true);
        this.openInfoButton.SetActive(false);
        this.interactable = false;
        // Tracker: player forgot the target and opens the popUp 
        Tracker.T.trackedGameObject.Interacted("OlvidoPrendaAGuardar");
    }
	
    public void ClickOnDrawer(GameObject go)
    {
        go.SetActive(true);
        this.panelShow = go;
        this.luggagePanel.SetActive(true);
        this.buttonCloseDrawer.SetActive(true);
        this.floorPanel.SetActive(true);
        this.interactable = false;
        // Tracker: what objects were shown
        SendTrackerWhatItemsWereShown(go);
    }

    private void SendTrackerWhatItemsWereShown(GameObject go)
    {
        string var = "";

        for (int i = 0; i < go.transform.childCount; i++)
        {
            var += go.transform.GetChild(i).gameObject.name + "-";
        }

        Tracker.T.setVar("ObjetosVistos", var);

        Tracker.T.trackedGameObject.Interacted("AbrioCajon");
    }

    public void CloseDrawer()
    {
        if (this.panelShow != null) this.panelShow.SetActive(false);
        this.panelShow = null;
        this.buttonCloseDrawer.SetActive(false);
        this.luggagePanel.SetActive(false);
        this.floorPanel.SetActive(false);
        this.interactable = true;
    }

    public void ClosePopUp()
    {
        this.popUpPanel.SetActive(false);
        this.openInfoButton.SetActive(true);
        this.interactable = true;
    }

    public bool isInteractable
    {
        get { return this.interactable; }

    }

    public int actualTargetNum
    {
        get { return this.actualTarget; }
    }

    public float ActualTime
    {
        get { return this.actualTime; }
    }

    public void GameOver()
    {
        Debug.Log("Game over");
        gameCompleted = true;
        interactable = false;
        this.gameOver.SetActive(true);
        // Tracker: gameOver, actualTarget
        NotifyEndOfGameToTracker(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	public void Continue(){
		if(GameObject.Find ("CharacterController"))
			GameObject.Find ("CharacterController").GetComponent<CharactersController>().airport=true;

		SceneManager.LoadScene ("Black");
	}

    //Send the tracker 
    private void NotifyEndOfGameToTracker(bool playerWon)
    {
        if (playerWon)
            Tracker.T.setVar("Time", this.actualTime);

        Tracker.T.Completable.Completed(this.level, CompletableTracker.Completable.Level, playerWon, this.actualTarget);
    }
}
