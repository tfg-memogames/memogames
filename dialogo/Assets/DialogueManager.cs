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
    private OptionsController _optionC;

    //Línea actual del diálogo.
    //Es variable global para poder ser modificada por InputHandler.
    private int _currentDialogueIndex = 0;

    //Variables del diálogo
    private string _name = "name";
    private int _age = 0;
    private string _destination = "Destino";
    private string _flightDate = "Date";
    private string _suitCase = "suit";


    //Botones del diálogo.
    public Button _button1;
    public Button _button2;

    //Archivos .txt
    private const string FIRST_DIAL = "first_dialogue.txt";
    private const string SECOND_DIAL = "second_dialogue.txt";


    // Use this for initialization
    void Start () {
		_dialogueBox = GameObject.Find("Dialogue Box");

        _optionC = GameObject.FindObjectOfType<OptionsController>();

        this.readFile(FIRST_DIAL);
    }

    private void readFile(string textName)
    {
        _dialogueBox.SetActive(true);
        textComponent.text = "";
        HideIcons();

        //Read files incomplete, miss keep the number of items
        string text = System.IO.File.ReadAllText(textName);
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

        //Comienza el segundo diálogo. (De momento comienza al pulsar la letra T)
        else if(Input.GetKeyDown(KeyCode.T) && !_isDialoguePlaying)
        {
            readFile(SECOND_DIAL);
        }
	}


  


    private IEnumerator StartDialogue() {
        //En length almacena la cantidad de líneas
		int length = _dialogue.Length;
        

		while (_currentDialogueIndex < length || !_isStringBeingRevealed) {
			if (!_isStringBeingRevealed && !waitingEntry) {
                string l = _dialogue[_currentDialogueIndex];
                //string[] line = _dialogue[_currentDialogueIndex].Split(' ');
                //El símbolo # marca que se debe pedir datos al usuario.
                if (l.StartsWith("#"))
                {
                    waitingEntry = true;
                    this._input.interactiveOn();
                    
                }
                //El símbolo @ marca que se deben mostrar opciones al usuario.
                else if (l.StartsWith("@"))
                {
                    //Avisa a OptionsController y le pasa el contenido de la línea.
                    _optionC.prepareOptions(l);
                    waitingEntry = true;
                }
                else
                {
                    _isStringBeingRevealed = true;
                    StartCoroutine(ShowLine(l));
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


    private IEnumerator ShowLine(string l) {
        //Length: Longitud de la línea

        string[] line = l.Split(' ');
		int wordCounter = 0;

		textComponent.text = "";
		HideIcons();

        //Split para dividir en palabras y poder sustituir las variables.
        int length = line.Length;

        while (wordCounter < length) {
            if (line[wordCounter].Contains("&name")) 
                line[wordCounter] = _name;
            else if(line[wordCounter].Contains("&destination"))
                line[wordCounter] = _destination;
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

    public string destination
    {
        get { return this._destination; }
        set { _destination = value; }
    }

    public string flightDate
    {
        get { return this._flightDate; }
        set { _flightDate = value; }
    }


    public int currentDialogueIndex
    {
        get { return this._currentDialogueIndex; }
        set { _currentDialogueIndex = value; }
    }

    public string suitCase
    {
        get { return this._suitCase; }
        set { _suitCase = value; }
    }



}
