using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RAGE.Analytics;
using System.Linq;

public class GameManager : MonoBehaviour
{

    private GameState gs;                                                                           //GameState

    public GameObject[] bestPath;                                                                   //Asignar en el inspector las road que son camino óptimo
    public CanvasManager cm;                                                                        //Canvas Manager
    public Button buttonShow;                                                                       //Asignar boton que muestra mapa
    public int MAP_COUNTER = 4;                                                                     //Número máximo de veces que el jugador puede ver el mapa completo.
    public Text mapCounterText;                                                                     //Asignar texto con el contador de vidas
    public Sprite mapOpenedSprite;                                                                  //Sprites del mapa
    public Sprite mapClosedSprite;
    public Camera mainCamera;                                                                       //Main Camera
    public static GameState gameS;                                                                  //Estado del juego
    public CarMove car;                                                                             //Script de movimiento del coche


    private static Vector3 cameraPosition = new Vector3(-273.4f, 10.85f, -350.0f);                  //Posición de la cámara al pulsar el mapa
    private Vector3 camCurrPos;                                                                     //Posición actual de la cámara

    private int mapCounter;                                                                         //Contador de consultas al mapa
    private int _pathLength;                                                                        //Longitud del camino óptimo
    private GameObject miniDestination;                                                             //Destino pequeño
    private GameObject destination;                                                                 //Destino grande
    private int distance;                                                                           //Contador de distancia recorrida.
    private int distanceWrongPath;                                                                  //Distancia recorrida fuera del camino óptimo
    private bool bestP;                                                                             //Booleano que indica si se ha estado siempre en el camino óptimo
    private float currentConsum = 0;                                                                //Variable que gestiona el consumo.
    private float consumption = 1f;                                                                 //Gasto de energía
                                                                                                    //private Tracker tracker;
    private float time = 0;                                                                         //Time
    private bool _counting;                                                                         //Booleano que indica si está corriendo el tiempo
    private string level;                                                                           //Nivel actual


    //===================================================================================================================
    void Awake()
    {
        miniDestination = GameObject.Find("mini-destination");
        destination = GameObject.Find("destination");
        _pathLength = bestPath.Length;
        this.mapCounter = MAP_COUNTER;

    }
    //======================================================================================================================

    // Use this for initialization
    void Start()
    {
        gs = GameObject.FindObjectOfType<GameState>();                                                                  //Buscamos el objeto GameState y lo asignamos
        this.level = this.levelToString(SceneManager.GetActiveScene().name);                                            //Cargamos el nombre del nivel 
        this.bestP = true;                                                                                              //Inicializamos las vriables relativas a la ruta
        this.distance = 0;
        this.distanceWrongPath = 0;
        this._counting = false;
        car.mapOpened = false;

        //Mandar una traza del tipo initialized al comenzar el nivel.
        //Con el nombre del jugador,apellidos,edad, el nivel, distancia óptima

        Tracker.T.setVar("Name", this.gs.playerName);
        Tracker.T.setVar("Surname", this.gs.playerSurname);
        Tracker.T.setVar("Age", this.gs.playerAge);
        Tracker.T.setVar("Optimum_Distance", (pathLength - 3) + "-" + (pathLength + 1));
        Tracker.T.setVar("Map_range", "1-" + maxMap);
        Tracker.T.setVar("Max_score", 100);

        Tracker.T.completable.Initialized(this.level);
    }
    //=========================================================================================================================

    // Update is called once per frame
    void Update()
    {
        if (mapCounter == MAP_COUNTER)                                                                      //Si el contador de pistas está entero (es la primera vez) enseñamos el mapa y actualizamos el valor del consumo.
        {
            consumption = car.getConsumption();
            this.showMap();
        }

        if (this._counting)                                                                                 //Si se está contando, se calcula el tiempo 
        {
            time += Time.deltaTime;
        }
    }

    //===========================================================================================================================
    //Método que devuelve si el camino hasta el momento ha sido el mejor camino. 
    public bool isBestPath(GameObject road)
    {
        return bestPath.ToList().Contains(road);                                                            //Transforma el array en una lista y comprueba que los caminos sean iguales.                                           
    }
    //============================================================================================================================

    //Método que activa los elementos del mapa o los desactiva segun el valor de opened.
    private void openMap(bool opened)
    {
        car.carArrow.SetActive(opened);
        miniDestination.SetActive(!opened);
        destination.SetActive(opened);
    }

    //Muestra el mapa 
    public void showMap()
    {
        if (!car.mapOpened)
        {
            if (mapCounter > 0)                                                                         //Si aún nos quedan pistas...
            {
                car.stopCar();                                                                          //Paramos el coche.
                openMap(true);                                                                         //Se abre el mapa
                this.mapCounter--;
                car.mapOpened = true;
                buttonShow.image.sprite = mapClosedSprite;
                camCurrPos = mainCamera.transform.position;                                             //Se actualiza la posición actual de la camara.
                mainCamera.GetComponent<CameraMove>().chase = false;                                    //Se configura l camara
                mainCamera.fieldOfView *= 2.4f;
                mapCounterText.text = "" + mapCounter;

                //Llamamos al método que muestra las rutas óptimas
                foreach (GameObject road in bestPath)
                {
                    road.GetComponent<SpriteRenderer>().color = Color.yellow;                           //Se pinta el mejor camino de amarillo.
                }
            }
        }

        else //Está cerrando el mapa
        {
            //El contador del tiempo empieza cuando el jugador cierra por primera vez el mapa
            //Y el coche empieza a moverse
            if (this.mapCounter == MAP_COUNTER - 1)
                counting = true;
            if (mapCounter > 0)
                buttonShow.image.sprite = mapOpenedSprite;

            if (this.car != null && !this.car.intersection)                                             //Si el coche no está en una intersección se reanuda la marcha
                car.ResumeCar();


            Tracker.T.trackedGameObject.Interacted("map");

            //Aquí modificas la escala
            openMap(false);
            mainCamera.transform.position = camCurrPos;
            mainCamera.fieldOfView /= 2.4f;

            //Llamamos al método que muestra las rutas óptimas
            foreach (GameObject road in bestPath)
            {
                road.GetComponent<SpriteRenderer>().color = Color.white;                               //Se quita el color amarillo del mejor camino pintando de blanco.
            }
            car.mapOpened = false;
            mainCamera.GetComponent<CameraMove>().chase = true;
        }
    }

    //=====================================================================================================================

    //El coche llama a este método cada vez que cambia de baldosa
    public void incrDistance(GameObject road)
    {
        distance++;                                                                                         //Se incrementa la distancia recorrida  
        cm.dist_Text.text = "Distancia: " + distance;                                                       //Se dice al Canvas Manager que actualice el cartel.  
        decreaseEnergy();                                                                                   //Se decremeta la energía

        if (this.bestP)
        {
            if (!isBestPath(road))
            {
                this.distanceWrongPath++;
                if (this.distanceWrongPath > 2)                                                             //Se comprueba que se siga por el mejor camino.
                    this.bestP = false;
            }
            else
                this.distanceWrongPath = 0;
        }
    }

    //Método que decrementa la energía
    private void decreaseEnergy()
    {
        this.currentConsum += this.consumption;                                                         //El consumo actual se incrementa con el consumo del coche
        cm.setPercentageOfEnergy(this.currentConsum);                                                   //Se dice al Canvas Manager que actualice el gráfico barra de combustible.

        //Si el consumo llega al 100% se para de contar, 
        //se manda una traza con los datos y se termina la partida.
        if (this.currentConsum >= 100)
        {
            counting = false;
            storeTracker(this.distance, mapTimes, false, 0, (int)this.time);
            cm.lose();                                                                                   //Se avisa al canvas manager de que se ha acabado para que muestre el pop up.
        }
    }
    //=============================================================================================================================================

    //Se llama a este método cuando se termina la partida habiendo completado el camino
    public void win()
    {
        float points = this.pathLength / distance;                                                      //Se calculan los puntos obtenidos
        counting = false;                                                                               //Se deja de contar
        storeTracker(this.distance, mapTimes, true, points, (int)this.time);                            //Se manda una traza con la información
        cm.win(points);                                                                                 //Se avisa al Canvas manager para que seaque el popUp
    }
    //===============================================================================================================================================

    //Tiempo, icono google maps para el destino
    private void storeTracker(int distance, int map, bool goal, float score, int seconds)
    {
        if (score > 1)
            score = 1;
        // Máxima puntuación 100
        score *= 100;
        int punt = (int)score;


        Tracker.T.setVar("Time", seconds);
        Tracker.T.setVar("Map", map);
        Tracker.T.setVar("Distance", distance);
        Tracker.T.setVar("Optimum", bestP);
        Tracker.T.completable.Completed(this.level, CompletableTracker.Completable.Level, goal, score);

    }
    //==================================================================================================================================================
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
    //Método que destruye el coche
    public void destroyCar() { car.destroyCar(); }
    //===========================================================================================================================
    //Getters y setters
    public bool counting
    {
        get { return this._counting; }
        set { this._counting = value; }
    }
    public int pathLength
    {
        get { return this._pathLength; }
        set { this._pathLength = value; }
    }
    public int mapTimes
    {
        get { return (MAP_COUNTER - this.mapCounter); }
    }

    public int maxMap
    {
        get { return MAP_COUNTER; }
    }


}