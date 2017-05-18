using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RAGE.Analytics;

public class CanvasManager : MonoBehaviour
{

	public CarMove car;

    public GameObject bar;
    public GameObject gas;
    public GameObject electric;
    public Text omitir;

    public static bool end=false;
    private RectTransform rt;
    private float totalEnergy;

    private float currentConsum = 0;

    private int distance;
    //Distancia fuera del camino óptimo
    private int distanceWrongPath;
    //Booleano que indica si se ha estado siempre en el camino óptimo
    private bool bestP;
    public Text dist_Text;



    //Gasto de energía
    private float consumption = 1f;

    //GameState
    private GameState gs;

    //GameManager
    private GameManager gm;


    //Time
    private float time = 0;
    private bool _counting;

    //PopUp
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

    public bool tutorial=false;

    private string level;


    // Use this for initialization
    void Start()
    {
		print(Application.persistentDataPath);

        this.level = levelToString(SceneManager.GetActiveScene().name);
        
        this.bestP = true;
        this._counting = false;
        this.dist_Text.text = "Distance: 0";
        this.distance = 0;
        this.distanceWrongPath = 0;
        gs = GameObject.FindObjectOfType<GameState>();
        gm = GameObject.FindObjectOfType<GameManager>();


        if (gs.carType == GameState.Car.ELECTRIC)
        {
            gas.SetActive(false);
            bar = electric.transform.GetChild(0).transform.GetChild(0).gameObject;
        }
        else { 
            electric.SetActive(false);
            bar = gas.transform.GetChild(0).transform.GetChild(0).gameObject;
        }


        this.rt = bar.GetComponent<RectTransform>();
        this.totalEnergy = rt.sizeDelta.x;



        wastedEnergy.enabled = false;
        next.gameObject.SetActive(false);
        level_up.enabled = false;
        score.enabled = false;


        stars6.gameObject.SetActive(false);
        stars5.gameObject.SetActive(false);
        stars4.gameObject.SetActive(false);
        stars3.gameObject.SetActive(false);
        stars2.gameObject.SetActive(false);
        stars1.gameObject.SetActive(false);


        if(tutorial){
        	electric.gameObject.SetActive(false);
        	this.dist_Text.gameObject.SetActive(false);
        	GameObject.Find("mapButton").SetActive(false);
        }else{
        	//GameObject.Find("Panel").SetActive(false);
        }
        //Mandar una traza del tipo initialized al comenzar el nivel.
        //Con el nombre del jugador,apellidos,edad, el nivel, distancia óptima
        /*
         
        Tracker.T.setVar("Surname", this.gs.playerSurname);
        Tracker.T.setVar("Age", this.gs.playerAge);
        Tracker.T.setVar("Optimum distance", (gm.pathLength - 3) + " - " + (gm.pathLength + 1));
        Tracker.T.setVar("Maximun score", 100);
        Tracker.T.completable.Initialized(this.level);
        */

        
        Tracker.T.setVar("Name", this.gs.playerName);
        Tracker.T.setVar("Surname", this.gs.playerSurname);
        Tracker.T.setVar("Age", this.gs.playerAge);
        Tracker.T.setVar("Optimum_Distance", (gm.pathLength - 3) + "-" + (gm.pathLength + 1));
        Tracker.T.setVar("Max_score", 100);
        Tracker.T.completable.Initialized(this.level);
       


    }

    public void video(){
    	electric.gameObject.SetActive(true);
        this.dist_Text.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        if(end){
            /*electric.gameObject.SetActive(true);
            this.dist_Text.gameObject.SetActive(true);*/
            omitir.text = "Salir";
        }

        if(this._counting) { 
            time += Time.deltaTime;
        }

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



        //Si es de gasolina el gasto es 1.5 el del eléctrico
        if (gs.carType == GameState.Car.GAS)
            this.currentConsum += this.consumption * 1.5f;
        else
            this.currentConsum += this.consumption;

        setPercentageOfEnergy(this.currentConsum);

        //Parar coche y mostrar fin.
        if (this.currentConsum >= 100)
        {
            lose();
        }

    }

    public void mapButton_Clicked()
    {
        gm.showMap();

    }

    public void restartButtonClicked()
    {
        SceneManager.LoadScene("Level_Selector");
    }


    //El coche llama a este método cada vez que cambia de baldosa
    
    public void incrDistance(GameObject road)
    {
        distance++;
        this.dist_Text.text = "Distance " + distance;
        decreaseEnergy();

        if(this.bestP) { 
            //Si la función devuelve true 3 veces seguidas ya podemos asegurar que se ha salido del camino óptimo
            if (!gm.isBestPath(road)) { 
                this.distanceWrongPath++;
                if (this.distanceWrongPath > 2)
                    this.bestP = false;
            }
            else
            {
                this.distanceWrongPath = 0;
            }
        }
    }


    /* private string levelToString(int level)
     {
         string levelName = "";
         switch(level)
         {
             case 1: levelName = "Fácil";
                 break;
             case 2: levelName = "Medio";
                 break;
             case 3: levelName = "Difícil";
                 break;
             default: levelName = "Fácil";
                 break;
         }
         return levelName;
     }
     */
    private string levelToString(string scene)
    {
        string levelName = "";
        switch (scene)
        {
            case "Easy_Level":
                levelName = "Fácil";
                break;
            case "Medium_Level":
                levelName = "Medio";
                break;
            case "Hard_Level":
                levelName = "Difícil";
                break;
            default:
                levelName = "Fácil";
                break;
        }
        return levelName;
    }


    //Muestra popup con que has ganado (desbloqueará el siguiente nivel)
    public void win()
    {
        counting = false;
        car.stopCar();
        
        float path = gm.pathLength;

        float dist = distance;

        float points = path / dist;
        int stars = 0;

        if (points >= 0.95)
        {
            score.text = "¡Has conseguido la máxima puntuación!";
            stars3.gameObject.SetActive(true);
            stars1.gameObject.SetActive(true);
            stars2.gameObject.SetActive(true);
            stars = 3;
        }
        else if (points > 0.80 && points < 0.95)
        {
            score.text = "¡Has conseguido 2 estrellas (Max. 3)!";
            stars4.gameObject.SetActive(true);
            stars5.gameObject.SetActive(true);
            stars = 2;
        }
        else if (points > 0.6 && points < 0.80)
        {
            score.text = "¡Has conseguido 1 estrella (Max. 3)!";
            stars6.gameObject.SetActive(true);
            stars = 1;
        }
            
        else 
        {
            score.text = "No has conseguido ninguna estrella :(";
            score.enabled = true;

        }
        storeTracker(this.distance, gm.mapTimes, true, points, stars,(int)this.time);


        

        
        next.gameObject.SetActive(true);

        level_up.enabled = true;



    }

    private void lose()
    {
        //DestroyObject(car);
        car.destroyCar();
        counting = false;
        storeTracker(this.distance, gm.mapTimes, false, 0, 0,(int) this.time);
        wastedEnergy.enabled = true;
    }



    //Tiempo, icono google maps para el destino
    private void storeTracker(int distance, int map, bool goal, float score, int stars, int seconds)
    {
        string path = "./prueba_" + name + "_" + level + ".txt";
        if (score > 1)
            score = 1;
        // Máxima puntuación 100
        score *= 100;
        int punt = (int)score;




        


        Tracker.T.setVar("Time", seconds);
        Tracker.T.setVar("Map", map);
        Tracker.T.setVar("Distance", distance);
        Tracker.T.setVar("Optimum", bestP);
        Tracker.T.setVar("Stars", stars);
        Tracker.T.completable.Completed(this.level, CompletableTracker.Completable.Level, goal,score);
    }




    public bool counting
    {
        get { return this._counting; }
        set { this._counting = value; }
    }
}



