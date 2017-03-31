using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private int mapCounter = 4;

    //Length of optimal path
    private int _pathLength;

    public static GameState gameS;

    //private bool _mapOpened;

    public Sprite mapOpenedSprite;
    public Sprite mapClosedSprite;

    public CarMove car;


    void Awake()
    {
        _pathLength = bestPath.Length;
    }

    // Use this for initialization
    void Start()
    {
        car.mapOpened = false;
        
        //_mapOpened = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mapCounter == 4)
        {
            this.showMap();
        }
    }

    //Muestra el mapa 
    public void showMap()
    {
        if (!car.mapOpened)
        {
            if (mapCounter > 0)
            {
                car.stopCar();
                mapCounter--;


                car.mapOpened = true;
                buttonShow.image.sprite = mapClosedSprite;
                camCurrPos = mainCamera.transform.position;
                
                //Vector3 vec = camCurrPos;
                //vec.z = vec.z - 50;
                mainCamera.GetComponent<CameraMove>().chase = false;
                mainCamera.fieldOfView = 159;
                mainCamera.gameObject.GetComponent<Transform>().position = new Vector3(-1394f, -583f, -40);
                mapCounterText.text = "" + mapCounter;

                //Llamamos al método que muestra las rutas óptimas
                foreach (GameObject road in bestPath)
                {
                    road.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
            }
        }

        else
        {
            if (mapCounter > 0)
            {
                buttonShow.image.sprite = mapOpenedSprite;

            }
            //Si el coche no está en una intersección se reanuda la marcha
            if(!car.intersection)
                car.ResumeCar();
            mainCamera.transform.position = camCurrPos;
            mainCamera.fieldOfView = 70;
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


   


    //Se llama desde CarMove cuando el coche se queda sin energía o combustible
    //Muestra popup con que has perdido (reiniciar o salir)

    //De momento esto se hace en Canvas Manager
    public void lose()
    {

    }

    public int pathLength
    {
        get { return this._pathLength; }
        set { this._pathLength = value; }
    }

   


}
