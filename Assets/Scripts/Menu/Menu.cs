using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public enum Gender { M, F };

	private string _gender="";
	private int _level=0;
	private Gender g;

	public GameObject error;

	public void iniGame(){
		if (_gender.Equals ("") || _level == 0) {
			error.gameObject.SetActive (true);
		} else {
			if (_gender.Equals ("M"))
				g = Gender.M;
			else
				g = Gender.F;

			//guardar g en gameState
			//guardar _level en gameState
			SceneManager.LoadScene("Conver With Fernando");

		}
	
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

	public void Quit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
