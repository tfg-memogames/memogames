using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class CustomDialogManager : DialogEventManager {

    public enum State
    {
        Opening, Showing, Closing, Idle, Options
    }

    // Public variables
    public float timePerCharacter;
    public float fadeTime;
    public Text textHolder;
    public Text optionsMessage;
    public CanvasGroup dialogGroup;
    public CanvasGroup optionsGroup;
    public GridLayoutGroup optionsGrid;
    public GameObject optionPrefab;

    // Private variables
    private State state = State.Idle;
    private Fragment frg;
    private List<Option> opt;
    private int charactersShown;
    private float accumulated;
    private string msg = "";
    private CanvasGroup managingGroup;
    private Option optionSelected;


    //Info between scenes
    private GameState _gs;

    void Start()
    {
        state = State.Idle;
        this._gs = GameObject.FindObjectOfType<GameState>();
    }

    // ------------------------------
    // Abstract class implementation
    // ------------------------------

    protected override void DoFragment(Fragment fragment)
    {
        frg = fragment;
        msg = frg.Msg;
        //Añado las keyWords si las hubiese.
        if (_gs != null) { 
            msg = changeKeyWords(msg);
        }
        charactersShown = 0;
        state = State.Opening;
        managingGroup = dialogGroup;

        if(frg.Character != "" && frg.Parameter != "")
        {
            GameObject.Find(frg.Character).SendMessage(frg.Parameter);
        }
    }

    protected override void DoOptions(string question, List<Option> options)
    {
        opt = options;
        msg = question;
        

        optionSelected = null;
        optionsMessage.text = msg;
        managingGroup = optionsGroup;
        state = State.Opening;
        foreach (var o in opt)
        {
            // create the options
            var option = GameObject.Instantiate(optionPrefab);
            option.transform.SetParent(optionsGrid.transform);
            var text = option.transform.GetChild(0).GetComponent<Text>().text = o.Text;
            option.GetComponent<Button>().onClick.AddListener(() => {
                optionSelected = opt.Find(e => e.Text == text);
            });
        }
    }

    protected override bool IsFragmentFinised() { return frg == null; }
    protected override int IsOptionSelected() { return state == State.Idle ? opt.FindIndex(o => o == optionSelected) : -1;}

    // -----------------------------------
    // State management during update
    // -----------------------------------
	
	void Update () {
        switch (state)
        {
            case State.Opening:
                // No matter what, when opening, just fade in the group
                managingGroup.alpha = Mathf.Clamp01(managingGroup.alpha + Time.deltaTime / fadeTime);
                if (managingGroup.alpha == 1)
                    state = State.Showing;
                break;
            case State.Showing:
                // When showing we'll care about the shown group
                if (managingGroup == dialogGroup)
                {

                    // If showing a fragment, show more characters until all of them are displayed
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (charactersShown < msg.Length)
                        {
                            charactersShown = msg.Length;
                            UpdateText();
                        }
                        else state = State.Closing;
                    }

                    accumulated += Time.deltaTime;
                    while (accumulated > timePerCharacter)
                    {
                        charactersShown = Mathf.Clamp(charactersShown + 1, 0, msg.Length);
                        UpdateText();
                    }
                }
                else if (managingGroup == optionsGroup)
                {
                    if (optionSelected != null) state = State.Closing;
                }

                break;
            case State.Closing:
                // No matter what, when closing, just fade out the displayed group
                managingGroup.alpha = Mathf.Clamp01(managingGroup.alpha - Time.deltaTime / fadeTime);
                if (managingGroup.alpha == 0)
                {
                    state = State.Idle;
                    textHolder.text = "";
                    frg = null;

                }
                break;
            case State.Idle:
                break;
        }
    }


    private void UpdateText()
    {
        accumulated -= timePerCharacter;

        textHolder.text = msg.Substring(0, charactersShown);
    }

    private string changeKeyWords(string msg)
    {
        string[] wList = msg.Split(' ');
        string nMsg = "";
        int wordCounter = 0;
        while(wordCounter < wList.Length)
        {
            if(wList[wordCounter].Contains("&name"))
                wList[wordCounter] = _gs.playerName;
            //A space between every 2 words.
            nMsg += wList[wordCounter];
            nMsg += " ";
            wordCounter++;
        }

        

        return nMsg.ToString();
    }
}
