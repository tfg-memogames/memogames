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

    private GameObject bar;
    public GameObject electric;

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
    private float consumption = 10f;

    //GameState
    private GameState gs;

    //GameManager
    private GameManager gm;


    //Time
    private float time = 0;
    private bool _counting;

    //PopUp
	public GameObject endOfGamePanel;
    public Button goToMenu;
    public Text message;
	public Text scoreText;
	public GameObject starsPanel;
	public GameObject star;
    public Button nextLevelButton;
    public Button restartButton;

    private string level;

    //True significa que es para hacer experimento con usuarios
    private bool experiment;


    // Use this for initialization
    void Start()
    {
        this.experiment = false;
		//print(Application.persistentDataPath);
        this.level = this.levelToString(SceneManager.GetActiveScene().name);
        
        this.bestP = true;
        this._counting = false;
        this.dist_Text.text = "Distancia: 0";
        this.distance = 0;
        this.distanceWrongPath = 0;
        gs = GameObject.FindObjectOfType<GameState>();
        gm = GameObject.FindObjectOfType<GameManager>();

        bar = electric.transform.GetChild(0).transform.GetChild(0).gameObject;

        this.rt = bar.GetComponent<RectTransform>();
        this.totalEnergy = rt.sizeDelta.x;

		this.endOfGamePanel.SetActive (false);
		this.star.SetActive (false);

        //Mandar una traza del tipo initialized al comenzar el nivel.
        //Con el nombre del jugador,apellidos,edad, el nivel, distancia óptima

        
        Tracker.T.setVar("Name", this.gs.playerName);
        Tracker.T.setVar("Surname", this.gs.playerSurname);
        Tracker.T.setVar("Age", this.gs.playerAge);
        Tracker.T.setVar("Optimum_Distance", (gm.pathLength - 3) + "-" + (gm.pathLength + 1));
		Tracker.T.setVar("Map_range",   "1-" + gm.maxMap);
		Tracker.T.setVar("Max_score", 100);
        
        Tracker.T.completable.Initialized(this.level);
        

    }

    public void video(){
		SceneManager.LoadScene("Video_Tutorial");
    	
    }

    private void FixedUpdate()
    {
        if(end){
			this.endOfGamePanel.SetActive (true);
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

    public void menuButtonClicked()
    {
        SceneManager.LoadScene("Level_Selector");
    }

    public void restartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    //El coche llama a este método cada vez que cambia de baldosa

    public void incrDistance(GameObject road)
    {
        distance++;
        this.dist_Text.text = "Distancia: " + distance;
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

    private string levelToString(string scene)
    {
        string levelName = "";
        switch (scene)
        {
            case "Tutorial_Jugable":
                levelName = "Tutorial";
                break;
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

	private void instatiateStars(int number) {
		for (int i = 0; i < number; i++) {
			GameObject star = Instantiate (this.star);
			star.SetActive (true);
			star.transform.parent = this.starsPanel.transform;
			star.transform.localPosition = Vector3.zero;
			star.transform.localScale = Vector3.one;
		}
	}

    //Muestra popup con que has ganado (desbloqueará el siguiente nivel)
    //Versión para experimento
    public void win()
    {
        counting = false;
        car.destroyCar();
        float path = gm.pathLength;

        float dist = distance;

        float points = path / dist;
        int stars = 0;

        this.message.text = "¡Has llegado a tu destino!";
        this.message.color = new Color(0.34F, 0.41F, 0.39F, 1); // 586A45FF divide (100 / FF (256 bits)) * rgb

        if (points >= 0.95)
        {
            this.scoreText.text = "¡Has conseguido el máximo de estrellas!";
            instatiateStars(3);
        }
        else if (points > 0.80 && points < 0.95)
        {
            this.scoreText.text = "¡Has conseguido 2 estrellas (Max. 3)!";
            instatiateStars(2);
        }
        else if (points > 0.6 && points < 0.80)
        {
            this.scoreText.text = "¡Has conseguido 1 estrella (Max. 3)!";
            instatiateStars(1);
        }

        else
        {
            this.scoreText.text = "No has conseguido ninguna estrella :(";
        }
        storeTracker(this.distance, gm.mapTimes, true, points, (int)this.time);

        if (experiment)
            this.endOfGamePanel.SetActive (true);
        else 
            SceneManager.LoadScene("Hall");
        

    }







    private void lose()
    {

        car.destroyCar();
        counting = false;
        storeTracker(this.distance, gm.mapTimes, false, 0, (int)this.time);


         
            this.endOfGamePanel.SetActive(true);
            this.message.text = "¡Te has quedado sin combustible!";
            scoreText.text = "";
            this.message.color = new Color(0.4F, 0.04F, 0.16F, 1); // 680C2AFF divide (100 / FF (256 bits)) * rgb
        
        if (!this.experiment) { 
            this.restartButton.gameObject.SetActive(true);
            this.nextLevelButton.gameObject.SetActive(false);
        }

    }


    //Tiempo, icono google maps para el destino
    private void storeTracker(int distance, int map, bool goal, float score, int seconds)
    {
        //string path = "./prueba_" + name + "_" + level + ".txt";
        if (score > 1)
            score = 1;
        // Máxima puntuación 100
        score *= 100;
        int punt = (int)score;

        
        Tracker.T.setVar("Time", seconds);
        Tracker.T.setVar("Map", map);
        Tracker.T.setVar("Distance", distance);
        Tracker.T.setVar("Optimum", bestP);
        Tracker.T.completable.Completed(this.level, CompletableTracker.Completable.Level, goal,score);
        
    }




    public bool counting
    {
        get { return this._counting; }
        set { this._counting = value; }
    }
}



