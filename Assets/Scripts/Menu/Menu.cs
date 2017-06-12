using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	

	private string _gender="";
	private int _level=0;
    
    private GameState.Gender g;
	private GameState _gs;

	public GameObject error;



	void Start(){
		this._gs = GameObject.FindObjectOfType<GameState> ();
		//_gs.playerGender = Gender.M;
	}

	public void iniGame(){
		if (_gender.Equals ("") || _level == 0) {
			error.gameObject.SetActive (true);
		} else {
			if (_gender.Equals ("M"))
				g = GameState.Gender.M;
			else
				g = GameState.Gender.F;

			//guardar g en gameState
			this._gs.playerGender=g;
            //guardar _level en gameState
            this._gs.level = this.intToEnumLevel(_level);
			
			SceneManager.LoadScene("Coffee_Shop");

		}
	
	}

    private GameState.Level intToEnumLevel(int l)
    {
        GameState.Level difficultyLevel;
        switch(l)
        {
            case 1: difficultyLevel = GameState.Level.Easy;
                break;
            case 2: difficultyLevel = GameState.Level.Medium;
                break;
            case 3: difficultyLevel = GameState.Level.Hard;
                break;
            default: difficultyLevel = GameState.Level.Easy;
                break;

        }
        return difficultyLevel;
    }

	public void Ready(){
		SceneManager.LoadScene("Ready");
	}
	public void Credits(){
		SceneManager.LoadScene("Credits");
	}

	public void Config(){
		SceneManager.LoadScene("Config");
	}

	public void Back(){
		SceneManager.LoadScene("Menu");
	}

	public string gender{
		get { return _gender; }
		set { _gender = value; }
	}
	public int level{
		get { return _level; }
		set { _level = value; }
		
	}

	public void Change(Text t){
		
		if (t.color == Color.white)
			t.color = new Color (0.1F, 0.2F, 0.3F, 1);
		else
			t.color = Color.white;

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
