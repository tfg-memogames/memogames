using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class GM : MonoBehaviour {

    // Use this for initialization
    public GameObject image;

    private GameObject levelSelectorPanel;
    private int level;              //0 = facil, 1= medio, 2 = dificil
    private float timer = 1;
    
    void Start () {
        levelSelectorPanel = GameObject.FindGameObjectWithTag("LevelSelector");
	}
	
	// Update is called once per frame
	void Update () {

        if(timer > 0)
            timer -= Time.deltaTime;
        else
        {
            image.SetActive(false);
        }
        image.GetComponentInChildren<Text>().text = timer.ToString("f0");
		
	}

    public void SetLevel(int l)
    {
        level = l;
        switch (level)
        {
            case 0: timer = 60; break;
            case 1: timer = 30; break;
            case 2: timer = 10; break;
        }
       
        levelSelectorPanel.SetActive(false);
        image.SetActive(true);
       

    }
    public void pista()
    {
        if (timer <= 0)
        {
            timer = 10;
            image.SetActive(true);
        }
        else
        {
            image.SetActive(false);
            timer = 0;
        }
    }


}
