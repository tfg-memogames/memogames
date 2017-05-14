using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RAGE.Analytics;

public class CanvasManager : MonoBehaviour
{

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

    public CarMove car;

    //GameState
    private GameState gs;

    //GameManager
    private GameManager gm;


    //Time
    private float time = 0;
    private bool _counting;

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

    public bool tutorial=false;


    // Use this for initialization
    void Start()
    {
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


        restart.gameObject.SetActive(false);
        wastedEnergy.enabled = false;
        next.gameObject.SetActive(false);
        level_up.enabled = false;
        score.enabled = false;

        exit.gameObject.SetActive(false);
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
        	GameObject.Find("Panel").SetActive(false);
        }

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


        

        exit.gameObject.SetActive(true);

        next.gameObject.SetActive(true);

        level_up.enabled = true;



    }

    private void lose()
    {
        //DestroyObject(car);
        car.destroyCar();
        counting = false;
        storeTracker(this.distance, gm.mapTimes, false, 0, 0,(int) this.time);
        restart.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        wastedEnergy.enabled = true;
    }



    //Tiempo, icono google maps para el destino
    private void storeTracker(int distance, int map, bool goal, float score, int stars, int seconds)
    {
        string name = gs.playerName;
        string level = levelToString(SceneManager.GetActiveScene().name);
        string path = "./prueba_" + name + "_" + level + ".txt";
        string content = "";
        string finished = "Sí";
        string bestPath = "Sí";
        if (!this.bestP)
            bestPath = "No";
        if (!goal)
            finished = "No";
        if (score > 1)
            score = 1;
        // Máxima puntuación 100
        score *= 100;
        int punt = (int)score;

        
        content = "Jugador: " + name + "\n";
        content += "Nivel: " + level + "\n";
        content += "Conseguido: " + finished + "\n";
        content += "Distancia total: " + distance + "(Óptimo: (" + (gm.pathLength - 3) + " - " + (gm.pathLength + 1) + ")\n";
        content += "Camino óptimo: " + bestPath + "\n";
        content += "Puntuacion: " + punt + " (Max: 100 puntos)\n";
        content += "Estrellas: " + stars + " (Max: 3 estrellas)\n";
        
        content += "Veces consultado el mapa: " + map + " (Min: 1, Max: " + gm.MAP_COUNTER + ")\n";
        content += "Tiempo transcurrido: " + seconds + " segundos\n";

        System.IO.File.WriteAllText(path, content);


        Tracker.T.completable.Completed("Nivel: " + level, CompletableTracker.Completable.Completable, goal,score);
        Tracker.T.alternative.Selected("¿Camino óptimo?", bestPath, AlternativeTracker.Alternative.Path);
        Tracker.T.alternative.Selected("Tiempo", seconds.ToString(), AlternativeTracker.Alternative.Question);
        Tracker.T.alternative.Selected("Mapa", map.ToString(), AlternativeTracker.Alternative.Question);

    }




    public bool counting
    {
        get { return this._counting; }
        set { this._counting = value; }
    }
}



