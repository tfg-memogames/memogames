using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FormManager : MonoBehaviour {

    public InputField _inputName;
    public InputField _inputSurname;
    public InputField _inputAge;
    public Text _messageError;
    private GameState _gs;
    private CursorMode cursorMode = CursorMode.Auto;

    // Use this for initialization
    void Start () {
        this._gs = GameObject.FindObjectOfType<GameState>();
        this._messageError.gameObject.SetActive(false);
    }
	
	public void ClickStart()
    {   

        Cursor.SetCursor(null, Vector2.zero, cursorMode);
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

        if (this._inputAge.text == "")
        {
            DisplayErrorMessage("Debes introducir una edad");
            return;
        }


        this._gs.playerName = _inputName.text;
        this._gs.playerSurname = _inputSurname.text;
        this._gs.playerAge = _inputAge.text;
        SceneManager.LoadScene("Level_Selector");
    }

    private void DisplayErrorMessage(string text)
    {
        this._messageError.gameObject.SetActive(true);
        this._messageError.text = text;
    }
	public void Quit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
 }
