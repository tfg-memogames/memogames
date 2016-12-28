using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class DialogueManager : MonoBehaviour {


	[SerializeField]
	private Text textComponent;

	//Conversation configuration
	
	private float SecondsBetweenCharacters = 0.1f;

    
    private float TextSpeedMultiplier = 0.25f;
	
	

    //Introducción de texto
	public KeyCode DialogueInput = KeyCode.Return;
	private string[] _dialogue;
    private string entry;
	public GameObject ContinueIcon;

	private GameObject _dialogueBox;
	private bool _isDialoguePlaying = false;
	private bool _isStringBeingRevealed = false;
	private bool _isEndOfDialogue = false;
    private bool _waitingEntry = false;

    

    //PRUEBAS
    public InputHandler _input;

    //Línea actual del diálogo.
    //Es variable global para poder ser modificada por InputHandler.
    private int _currentDialogueIndex = 0;

    //Variables del diálogo
    private string _name = "name";
    private int _age = 0;


    //Botones del diálogo.
    public Button _button1;
    public Button _button2;


    // Use this for initialization
    void Start () {
		_dialogueBox = GameObject.Find("Dialogue Box");
        //_button1 = GetComponent<Button>();
        //_button2 = GameObject.Find("Button 2").GetComponent<Button>();
        
        


        _dialogueBox.SetActive (true);
		textComponent.text = "";
		HideIcons ();

		//Read files incomplete, miss keep the number of items
		string text = System.IO.File.ReadAllText("myfile.txt");
        //Entrada
        //En "text" se almacena todo el contenido del archivo
        _dialogue = text.Split('\n');
        _currentDialogueIndex = 0;
    }


	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown (DialogueInput) && !_isDialoguePlaying) {
				_isDialoguePlaying = true;
				StartCoroutine(StartDialogue());
		}
	}


  


    private IEnumerator StartDialogue() {
        //En length almacena la cantidad de líneas
		int length = _dialogue.Length;
        

		while (_currentDialogueIndex < length || !_isStringBeingRevealed) {
			if (!_isStringBeingRevealed && !waitingEntry) {
                string[] line = _dialogue[_currentDialogueIndex].Split(' ');
                //El símbolo # marca que se debe pedir datos al usuario.
                if (line[0].Contains("#"))
                {
                    waitingEntry = true;
                    this._input.interactiveOn();
                    
                }
                else if(line[0].Contains("@"))
                {
                    //Debug.Log(line[1]);
                    int numberButtons = 0;
                    int.TryParse(line[1], out numberButtons);
                    buttonActivation(numberButtons, line);
                    waitingEntry = true;
                }
                else
                {
                    _isStringBeingRevealed = true;
                    StartCoroutine(ShowLine(line));
                    _currentDialogueIndex++;

                    if (_currentDialogueIndex <= length)
                    {
                        _isEndOfDialogue = true;
                    }
                }
            }

			yield return 0;
		}


		while(!Input.GetKeyDown (DialogueInput))
			yield return 0;

		HideIcons ();
		_isEndOfDialogue = false;
		_isDialoguePlaying = false;
	}

    
    private void buttonActivation(int number, string[] buttonNames)
    {
        //En las dos primeras posiciones de buttonNames viene basura.
        switch(number)
        {
            case 1:
                if(buttonNames[2] != null)
                    activeOneButton(buttonNames[2]);
                break;
            case 2:
                if (buttonNames[2] != null && buttonNames[3] != null)
                    activeTwoButton(buttonNames[2], buttonNames[3]);
                break;
            case 3:
                if(buttonNames[2] != null && buttonNames[3] != null && buttonNames[4] != null)
                    activeThreeButton(buttonNames[2], buttonNames[3], buttonNames[4]);
                break;
            case 4:
                if(buttonNames[2] != null && buttonNames[3] != null & buttonNames[4] != null && buttonNames[5] != null)
                    activeFourButton(buttonNames[2], buttonNames[3], buttonNames[4], buttonNames[5]);
                break;

        }

    }

    



    private IEnumerator ShowLine(string[] line) {
        //Length: Longitud de la línea
		
		int wordCounter = 0;

		textComponent.text = "";
		HideIcons();

        //Split para dividir en palabras y poder sustituir las variables.
        int length = line.Length;

        while (wordCounter < length) {
            if (line[wordCounter].Contains("&name")) 
                line[wordCounter] = _name;
            textComponent.text += line[wordCounter];
            textComponent.text += " ";
            wordCounter++;

			yield return new WaitForSeconds (SecondsBetweenCharacters * TextSpeedMultiplier);
		}

		ShowIcon();

		while(!Input.GetKeyDown (DialogueInput))
			yield return 0;

		
		//Debug.Log ("Velocidad restablecida a " + TextSpeedMultiplier);
		_isStringBeingRevealed = false;
		textComponent.text = "";
	}

	private void HideIcons() {
		ContinueIcon.SetActive (false);
	}

	private void ShowIcon() {
		if (_isEndOfDialogue) {
			ContinueIcon.SetActive (true);
		}
	}


    public bool waitingEntry
    {
        get { return this._waitingEntry; }
        set { _waitingEntry = value; }
    }

    public string playerName
    {
        get { return this._name; }
        set { _name = value; }
    }

    public int playerAge
    {
        get { return this._age; }
        set { _age = value; }
    }

    public int currentDialogueIndex
    {
        get { return this._currentDialogueIndex; }
        set { _currentDialogueIndex = value; }
    }


    private void activeOneButton(string butName)
    {
        _button1.gameObject.SetActive(true);
        _button1.name = butName;
    }

    private void activeTwoButton(string butName1, string butName2)
    {
        _button1.gameObject.SetActive(true);
        _button1.GetComponentInChildren<Text>().text = butName1;
        //_button1.GetComponent<Text>() = butName1;
        _button2.gameObject.SetActive(true);
        //butName2.Substring('\r');
        _button2.GetComponentInChildren<Text>().text = butName2;
        //_button2.name = butName2;
        

    }
    
    private void activeThreeButton(string butName1, string butName2, string butName3)
    {
        /* _button1.SetActive(true);
         _button1.name = butName1;
         _button2.SetActive(true);
         _button2.name = butName2;
         */
     }
     private void activeFourButton(string butName1, string butName2, string butName3, string butName4)
     {
        /*
         _button1.SetActive(true);
         _button1.name = butName1;
         _button2.SetActive(true);
         _button2.name = butName2;
        */
    }

}
