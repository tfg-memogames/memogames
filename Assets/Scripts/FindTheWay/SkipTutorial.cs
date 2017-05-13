using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipTutorial : MonoBehaviour {

	public void LoadLevelSelectorScene()
    {
        SceneManager.LoadScene("Level_Selector");
    }
}
