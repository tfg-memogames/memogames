using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RAGE.Analytics;
using System.Linq;

public class GameManager : MonoBehaviour
{

    //Asignar en el inspector las road que son camino óptimo
    public GameObject[] bestPath;
    //Canvas Manager
    public CanvasManager cm;

    //Asignar boton que muestra mapa
    public Button buttonShow;

    //Asignar texto con el contador de vidas
    public Text mapCounterText;

    //Posición de la cámara al pulsar el mapa
    private static Vector3 cameraPosition = new Vector3(-273.4f, 10.85f, -350.0f);
    //Posición actual de la cámara
    private Vector3 camCurrPos;

    //Main Camera
    public Camera mainCamera;

    //Contador de consultas al mapa
    private int mapCounter;


    //Número máximo de veces que el jugador puede ver el mapa completo.
    public int MAP_COUNTER = 4;

    //Length of optimal path
    private int _pathLength;

    public static GameState gameS;

    //private bool _mapOpened;

    public Sprite mapOpenedSprite;
    public Sprite mapClosedSprite;

    public CarMove car;

	private GameObject miniDestination;
	private GameObject destination;

	//private Tracker tracker;


    void Awake()
    {
		miniDestination = GameObject.Find ("mini-destination");
		destination = GameObject.Find ("destination");
        _pathLength = bestPath.Length;
        this.mapCounter = MAP_COUNTER;
    }

    // Use this for initialization
    void Start()
	{
		//tracker = GameObject.FindObjectOfType<Tracker> ();
        car.mapOpened = false;
        
        //_mapOpened = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mapCounter == MAP_COUNTER)
        {
            this.showMap();
        }
    }

    /*bool found = false;
        int i = 0;
        while (!found && i < bestPath.Length)
        {
            if (bestPath[i].Equals(road))
                found = true;
            else
                i++;
        }
        return found;*/


    public bool isBestPath(GameObject road)
    {
        return bestPath.ToList().Contains(road);
    }

	private void openMap(bool opened){
		car.carArrow.SetActive (opened);
		miniDestination.SetActive (!opened);
		destination.SetActive (opened);
	}
    //Muestra el mapa 
    public void showMap()
    {
 
        if (!car.mapOpened)
        {
            if (mapCounter > 0)
            {
                car.stopCar();
				openMap (true);
                
                this.mapCounter--;



                car.mapOpened = true;
                buttonShow.image.sprite = mapClosedSprite;
                camCurrPos = mainCamera.transform.position;
                
                
                mainCamera.GetComponent<CameraMove>().chase = false;
                mainCamera.fieldOfView *= 2.4f;
                
                
                mapCounterText.text = "" + mapCounter;

                //Llamamos al método que muestra las rutas óptimas
                foreach (GameObject road in bestPath)
                {
                    road.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
            }
        }
        
        else //Está cerrando el mapa
        {
            //El contador del tiempo empieza cuando el jugador cierra por primera vez el mapa
            //Y el coche empieza a moverse
            if (this.mapCounter == MAP_COUNTER - 1)
                cm.counting = true;
            if (mapCounter > 0)
            {
                buttonShow.image.sprite = mapOpenedSprite;

            }
            //Si el coche no está en una intersección se reanuda la marcha
            //Y no ha terminado el nivel
            if (this.car != null && !this.car.intersection)
                car.ResumeCar();


            Tracker.T.trackedGameObject.Interacted("map");

			//Aquí modificas la escala
			openMap(false);
            mainCamera.transform.position = camCurrPos;
            mainCamera.fieldOfView /= 2.4f;
            
            //Llamamos al método que muestra las rutas óptimas
            foreach (GameObject road in bestPath)
            {
                road.GetComponent<SpriteRenderer>().color = Color.white;
            }
            //_mapOpened = false;
            car.mapOpened = false;
            mainCamera.GetComponent<CameraMove>().chase = true;
        }
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



}
