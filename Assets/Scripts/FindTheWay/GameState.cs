using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

    //Asignar en el inspector las road que son camino óptimo
    public GameObject[] bestPath;
	public CanvasManager cm;

    private int _level;
    public enum Car { GAS, ELECTRIC, NONE};
    private Car _carType;

    //Asignar road donde queremos que finalice el juego
    public GameObject end;

    //Asignar boton que muestra mapa
    public Button buttonShow;

    //Asignar texto con el contador de vidas
    public Text countText;

    private static Vector3 cameraPosition = new Vector3(-273.4f, 10.85f, -350.0f);
    public Camera mainCamera;
    private int count = 3;
    private Vector3 camCurrPos;


    public static GameState gameS;

    private bool mapOpened;

    public Sprite mapO;
    public Sprite mapC;

    public CarMove car;

    void Awake()
    {

        mapOpened = false;
        if (gameS == null)
        {
            
            gameS = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
        //-this._level = 1;
        //this._carType = Car.ELECTRIC;


    }

    // Use this for initialization
    void Start () {
        if(end != null)
            end.AddComponent<EndOfGame>();
	}

    public Car carType
    {
        get { return this._carType; }
        set { this._carType = value; }
    }

    public int level
    {
        get { return this._level; }
        set { this._level = value; }
    }

    public void showMap()
    {
        if (!mapOpened)
        {
            if (count > 0)
            {
                car.stopCar();
                
                mapOpened = true;
                  buttonShow.image.sprite = mapC;
                camCurrPos = mainCamera.transform.position;
                count--;
                Vector3 vec = camCurrPos;
                vec.z = vec.z - 50;
                mainCamera.GetComponent<CameraMove>().chase = false;
                mainCamera.fieldOfView = 150;
                mainCamera.gameObject.GetComponent<Transform>().position = new Vector3(-273.4f, 10.85f, -40);
                countText.text = "" + count;

                //Llamamos al método que muestra las rutas óptimas
                foreach (GameObject road in bestPath)
                {
                    road.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
            }
           

           
        }

        else
        {
            if(count > 0) { 
                buttonShow.image.sprite = mapO;
                
            }
            car.ResumeCar();
            mainCamera.transform.position = camCurrPos;
            mainCamera.fieldOfView = 70;
            //Llamamos al método que muestra las rutas óptimas
            foreach (GameObject road in bestPath)
            {
                road.GetComponent<SpriteRenderer>().color = Color.white;
            }
            mapOpened = false;
            mainCamera.GetComponent<CameraMove>().chase = true;
        }
     }

    


    //Muestra popup con que has ganado (desbloqueará el siguiente nivel)
    public void win()
    {
		cm.showwin (bestPath.Length);

        Debug.Log("You Won;");
    }

    //Se llama desde CarMove cuando el coche se queda sin energía o combustible
    //Muestra popup con que has perdido (reiniciar o salir)
    public void lose()
    {

    }
}
