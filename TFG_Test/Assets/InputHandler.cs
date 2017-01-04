using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*COSAS QUE FALTAN:
1- Almacenar la información de los usuarios en un archivo externo.
*/


public class InputHandler : MonoBehaviour {


    InputField _input;
    private DialogueManager _dialog;

    

    /*Contador para saber en qué entrada estamos
      1 - Nombre
      2 - Edad
   
    */ 
    private int _cont;

    //Almacenamos en constantes los índices de las líneas de diálogo que pueden ser repetidas
    //1.Pedir la edad al usuario
    const int AGE_INDEX = 3; 
    // Use this for initialization
    void Start () {
        this._input = GetComponent<InputField>();
        this._input.gameObject.SetActive(false);
        _dialog = GetComponentInParent<DialogueManager>();
        this._cont = 1;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return))
        {
            readInput();
        }
	}

    private void readInput()
    {
        string text;
        text = _input.text;
        this._input.gameObject.SetActive(false);
        //System.IO.File.WriteAllText("playerDatas.txt", _text);
        _dialog.waitingEntry = false;
        storeInput(_cont, text);
        _input.text = "";
        

    }

    private void  storeInput(int cont, string text)
    {

        switch(cont)
        {
            case 1: //Player name
                _dialog.playerName = text;
                _dialog.currentDialogueIndex++;
                _cont++;
                break;
            case 2: //Player age
                int age = 0;
                if (checkAge(text, out age)) { 
                    _dialog.playerAge = age;
                    _dialog.currentDialogueIndex++;
                    _cont++;
                }
                else //La entrada no es correcta;
                {
                    _dialog.currentDialogueIndex = AGE_INDEX;
                }
                break;
            
        }


    }


    private bool checkAge(string text, out int result)
    {
        bool ok = false;
        //Comprobamos que ha introducido números.
        int.TryParse(text, out result);

        //Y que la edad está entre 40 y 100.
        if (40 < result && result < 100)
            ok = true;

        return ok;
    }


    public void interactiveOn()
    {
        this._input.gameObject.SetActive(true);
    }


}
