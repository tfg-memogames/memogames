using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public GameObject bar;
	public GameObject gas;
	public GameObject electric;

    private RectTransform rt;
    private float totalEnergy;

    private float currentConsum = 0;

    private int distance;
	public Text dist_Text;


    public float value = 100f;

    //Gasto de energía
    private float consumption = 1f;

    public CarMove car;

    //GameState
    private GameState gs;

    //PopUp
	public Button exit;
    public Button restart;
	public Button next;
	public Text wastedEnergy;
	public Text level_up;
	public Text score;


	public Button stars6;
	public Button stars5;
	public Button stars4;
	public Button stars3;
	public Button stars2;
	public Button stars1;

    private bool mapOpened;


    // Use this for initialization
    void Start()
    {
		this.dist_Text.text = "Distance: 0";
        this.distance = 0;
        this.rt = bar.GetComponent<RectTransform>();
        this.totalEnergy = rt.sizeDelta.x;
        gs = GameObject.FindObjectOfType<GameState>();

		if (gs.carType == GameState.Car.ELECTRIC)
			bar = electric;


		if (gs.carType == GameState.Car.GAS)
			bar = gas;
		

        restart.gameObject.SetActive(false);
		wastedEnergy.enabled=false;
		next.gameObject.SetActive(false);
		level_up.enabled=false;
		score.enabled = false;

		exit.gameObject.SetActive(false);
		stars6.gameObject.SetActive(false);
		stars5.gameObject.SetActive(false);
		stars4.gameObject.SetActive(false);
		stars3.gameObject.SetActive(false);
		stars2.gameObject.SetActive(false);
		stars1.gameObject.SetActive(false);

        mapOpened = false;
    }

   
    //Método cuando se acaba la energía
    private void wastedEner()
    {

        restart.gameObject.SetActive(true);
		exit.gameObject.SetActive(true);
		wastedEnergy.enabled=true;
    }

    //newValue es el porcentaje consumido
    private void setPercentageOfEnergy(float newValue)
    {
        
        float x = (newValue * totalEnergy) / 100;
        float y = rt.localPosition.y;
        float z = rt.localPosition.z;

        rt.localPosition = new Vector3(-x, y, z);
    }

    private void decreaseEnergy()
    {

        
        
        //Si es de gasolina el gasto es 2 el del eléctrico
        if (gs.carType == GameState.Car.ELECTRIC)
            this.currentConsum += this.consumption;
        else
            this.currentConsum += this.consumption * 1.5f;

        setPercentageOfEnergy(this.currentConsum);

        //Parar coche y mostrar fin.
        if (this.currentConsum >= 100)
        {
            car.stopCar();
            wastedEner();

        }

    }

    public void mapButton_Clicked()
    {
        

    }

    public void restartButtonClicked()
    {

        Debug.Log(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void incrDistance()
    {
        distance++;
        this.dist_Text.text = "Distance " + distance;
        decreaseEnergy();
    }

	public void showwin(int path){


		float points=path/distance;

		if (points == 1) {
			stars3.gameObject.SetActive (true);
			stars1.gameObject.SetActive (true);
			stars2.gameObject.SetActive (true);
		} else if (points > 0.75) {
			stars4.gameObject.SetActive (true);
			stars5.gameObject.SetActive (true);
		}else if (points > 0.5)
			stars6.gameObject.SetActive (true);
		else if (points < 0.5) {
			score.text = "No has conseguido ninguna estrella :(";
			score.enabled = true;

		}

		exit.gameObject.SetActive(true);

		next.gameObject.SetActive(true);

		level_up.enabled = true;



	}

}
