using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

///<summary>
///Class that controls the car move
///Clase que controla el movimiento del coche
///</summary>   
public class CarMove : MonoBehaviour
{
    private GameState gs;

    //Enumerado con las cuatro direcciones en las que puede avanzar el coche: Suroeste, Sureste, Noreste y Noroeste respectivamente.
    public enum Direction { SW, SE, NE, NW };
    private enum Tag { S, L, R, SL, SR, LR, SLR }

    //Struct con la información relativa al coche eléctrico.
    [System.Serializable]
    public struct Car
    {
        public Sprite front, back;

        public Direction dir;
        public float speed;
        public float consumption;

        private Collider2D _coll;                           //Variable privada para el collider del coche
        public Collider2D coll
        {
            get { return this._coll; }                      //Definición de getters y setters para el coche
            set { this._coll = value; }
        }

    }
    public Car electricCar;                                 //Coche eléctrico.
    public Car gasCar;                                      //Coche de gasolina.
    public GameManager gm;                                  //Canvads manager 

    private bool _intersection = false;                     //Booleano que indica si el coche está en una intersección

    ///<summary>
    ///Returns the state of the intersection and modifies it
    ///Devuelve el estado de la intersection y la modifica
    ///</summary>  
    public bool intersection
    {
        get { return this._intersection; }                  //Definición de getters y setters para la variable interseccion
        set { this._intersection = value; }
    }

    private bool move = true;                                //Booleano que indica si el juego está en marcha (si el coche está andando)
    private bool turn = true;                                //Booleano que indica si el coche tiene que girar.


    private bool arrowsEnabled = true;                       //Booleano que indica si se deben mostrar las flechas en una intersección.
    private bool _mapOpened = false;                         //Booleano que permite saber si el mapa está pulsado.
    public GameObject carArrow;                              //Flecha indicador de posicion
    private Car car;                                         //Coche de con el que se va a jugar

    //private Animator anim;

    //======================================================================================================
    // Use this for initialization
    void Start()
    {
        gs = GameObject.FindObjectOfType<GameState>();                              //Se actualiza el estado del juego

        if (gs.carType == GameState.Car.ELECTRIC) car = electricCar;                //Se actualiza el tipo de coche.
        else car = gasCar;
        move = true;
        //anim = gameObject.GetComponent<Animator>();
        ChangeCarView();
        carArrow.SetActive(false);

    }
    /// <summary>
    /// Change the view of the car following the directions changes of this
    /// Cambia la vista del coche siguiendo los cambios de direcciones de este
    /// </summary>
    private void ChangeCarView()
    {
        Vector3 v = this.GetComponent<Transform>().localScale;
        switch (car.dir)
        {
            case Direction.SW: //+x
                if (v.x < 0)
                    this.gameObject.GetComponent<Transform>().localScale = new Vector3(-v.x, v.y, v.z);
                this.GetComponent<SpriteRenderer>().sprite = car.front;
                break;
            case Direction.SE: //+y
                this.gameObject.GetComponent<Transform>().localScale = new Vector3(-v.x, v.y, v.z);
                this.GetComponent<SpriteRenderer>().sprite = car.front;
                break;
            case Direction.NE: //-x
                if (v.x < 0)
                    this.gameObject.GetComponent<Transform>().localScale = new Vector3(-v.x, v.y, v.z);
                this.GetComponent<SpriteRenderer>().sprite = car.back;
                break;
            case Direction.NW: //-y
                this.gameObject.GetComponent<Transform>().localScale = new Vector3(-v.x, v.y, v.z);
                this.GetComponent<SpriteRenderer>().sprite = car.back;
                break;
        }
    }
    /// <summary>
    /// Update is called once per frame 
    /// Actualiza el estado del juego frame a frame
    /// </summary>
    void Update()
    {
        if (move)
            MoveCar();                                                                          //Si el coche puede moverse, actualizamos su movimiento.
    }
    //Controla el movimiento del coche
    private void MoveCar()
    {
        Vector3 v = this.GetComponent<Transform>().position;

        switch (car.dir)                                                                        //Movemos el coche en función de su dirección.
        {
            case Direction.SW: //+x
                this.gameObject.GetComponent<Transform>().position = new Vector3(v.x - (float)car.speed, v.y - (float)(1 / Math.Sqrt(3)) * car.speed, v.z);
                break;
            case Direction.SE: //+y
                this.gameObject.GetComponent<Transform>().position = new Vector3(v.x + (float)car.speed, v.y - (float)(1 / Math.Sqrt(3)) * car.speed, v.z);
                break;
            case Direction.NE: //-x
                this.gameObject.GetComponent<Transform>().position = new Vector3(v.x + (float)car.speed, v.y + (float)(1 / Math.Sqrt(3)) * car.speed, v.z);
                break;
            case Direction.NW: //-y
                this.gameObject.GetComponent<Transform>().position = new Vector3(v.x - (float)car.speed, v.y + (float)(1 / Math.Sqrt(3)) * car.speed, v.z);
                break;
        }
    }
    /// <summary>
    /// The car can move
    /// El coche ya puede moverse
    /// </summary>
    public void ResumeCar()
    {
        this.move = true;                                                       //El coche ya puede moverse
    }
    /// <summary>
    /// The car can not  move
    /// El coche no puede moverse
    /// </summary>
    public void stopCar()
    {
        this.move = false;                                                      //El coche no puede moverse
    }
    /// <summary>
    /// The car is destroyed 
    /// El coche es destruido
    /// </summary>
    public void destroyCar()
    {
        Destroy(this);
    }
    /// <summary>
    /// Controller the car moves and update the canvas
    ///Controla los movimientos del coche y actuliza canvas    ///
    ///</summary>
    void OnTriggerExit2D(Collider2D other)
    {
        gm.incrDistance(other.gameObject);                                        //Actualizamos el canvas.
        //Recto
        if (other.gameObject.tag == Tag.S.ToString())
        {
            move = true;
            if (turn == false)
                turn = true;
        }
        //Intersección
        else if (turn)
        {

            if (other.gameObject.tag == Tag.L.ToString())                       //Si el giro es hacia la izquierda debe girar más tarde para continuar por el carril derecho.
                StartCoroutine(WaitToTurn());
            else if (other.gameObject.tag == Tag.R.ToString())                  //Si el giro es hacia la derecha gira en la dirección correspondiente.
            {
                if (car.dir > 0) car.dir--;                                     //Actualizamos la direccion.
                else car.dir = (Direction)3;
                ChangeCarView();                                                //Se actualiza el sprite
            }
            turn = false;
        }
    }

    /// <summary>
    ///La parte de cógido que quieres que se detenga debe estar dentro del método IEnumerator
    ///</summary>
    //La parte de cógido que quieres que se detenga debe estar dentro del método IEnumerator
    public IEnumerator WaitToTurn()
    {
        float delay = 0.17f;
        yield return new WaitForSeconds(delay);
        car.dir = (Direction)(((int)car.dir + 1) % 4);                           //Cambiamos la dirección con aritmetica modular sobre el enumerado Direction
        ChangeCarView();
    }
    /// <summary>
    /// Arrows appear in the places you can go
    ///Aparecen las flechas en los sitios a los que puede ir
    ///</summary>

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.name == "NW" ||
             other.gameObject.name == "NE" ||
             other.gameObject.name == "SE" ||
             other.gameObject.name == "SW") && (car.dir.ToString() != other.gameObject.name)
             && other != car.coll)
        {
            //Solo se muestran las flechas tras haber pasado por una carretera con la etiqueta Straight
            if (arrowsEnabled)
            {

                other.transform.parent.gameObject.GetComponent<Intersection>().ShowArrows(other.gameObject);
                car.coll = other;
                //Tras mostrar las flechas hay que esperar a pasar por una carretera recta.
                arrowsEnabled = false;
            }
        }

        //Tras pasar por una carretera recta ya se pueden volver a mostrar las flechas.
        if (other.gameObject.tag == "S")
            arrowsEnabled = true;

        else if (other.gameObject.tag == "I")
        {
            this.intersection = true;
            stopCar();
        }

    }

    public bool mapOpened
    {
        get { return this._mapOpened; }
        set { this._mapOpened = value; }
    }
    public bool isMoving() { return move; }
    public Direction dir() { return car.dir; }          //Getter para la variable dir del coche
    public float getConsumption() { return car.consumption; }
}
//=====================================================================================================================


