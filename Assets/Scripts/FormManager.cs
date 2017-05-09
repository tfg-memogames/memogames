using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FormManager : MonoBehaviour {

    public InputField _inputName;
    public InputField _inputSurname;
    public Text _messageError;
    //private GameState _gs;

    // Use this for initialization
    void Start () {
        //this._gs = GameObject.FindObjectOfType<GameState>();
        this._messageError.gameObject.SetActive(false);
    }
	
	public void ClickStart()
    {

        if (this._inputName.text == "")
        {
            DisplayErrorMessage("Debes introducir un nombre");
            return;
        }

        if (this._inputSurname.text == "")
        {
            DisplayErrorMessage("Debes introducir un apellido");
            return;
        }

        //this._gs.name = _inputName.text;
        //this._gs.surname = _inputSurname.text;
        //SceneManager.LoadScene("Level_Selector");
    }

    private void DisplayErrorMessage(string text)
    {
        this._messageError.gameObject.SetActive(true);
        this._messageError.text = text;
    }
 }
