using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void iniGame(){
		SceneManager.LoadScene("Conver With Fernando");
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

	public void Quit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
