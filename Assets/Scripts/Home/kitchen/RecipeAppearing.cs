using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeAppearing : MonoBehaviour {

    public Text step_One;
    public Text step_Two;
    public Text step_Three;
    public Text step_Four;

    const string one = "1- Llenar una cacerola \nde agua";
	const string two = "2- Cuando hierva, \nintroducir los Spaguetti";
    const string three = "3- Dejarlos en un plato";
    const string four = "4- Echar tomate y oregano.";


    // Use this for initialization
    void Start () {
        step_One.text = "";
        step_Two.text = "";
        step_Three.text = "";
        step_Four.text = "";
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    

    public void showFirstStep()
    {
        this.gameObject.SetActive(true);
        step_One.text = one;
    }
    public void showSecondtStep()
    {
		print ("two");
        step_Two.text = two;
    }
    public void showThirdStep()
    {
        step_Three.text = three;
    }
    public void showFourthStep()
    {
        step_Four.text = four;
    }


    
}
