using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndRecipe : MonoBehaviour {

	public void end(){
		SceneManager.LoadScene("HallWay");
	}

}
