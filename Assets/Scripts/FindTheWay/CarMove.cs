using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarMove : MonoBehaviour
{
    private GameState gs;



    public enum Direction { SW, SE, NE, NW };
    private enum Tag { S, L, R, SL, SR, LR, SLR }

    public Sprite frontGas;
    public Sprite backGas;
    public Sprite frontElec;
    public Sprite backElec;
    public Sprite front;
    public Sprite back;
    public Direction dir;
    public float speed;

    public CanvasManager cv;

    private Collider2D coll;

    private bool move = true;

    private bool turn = true;


    //Booleano que indica si se deben mostrar las flechas en una intersección.
    private bool arrowsEnabled = true;

   

    //Booleano que indica si el coche está en una intersección
    private bool _intersection = false;

    //Booleano que permite saber si el mapa está pulsado.
    private bool _mapOpened = false;

    //private Animator anim;

    public GameObject carArrow;

    // Use this for initialization
    void Start()
    {
        gs = GameObject.FindObjectOfType<GameState>();

        if (gs.carType == GameState.Car.ELECTRIC)
        {
            front = frontElec;
            back = backElec;
        } else
        {
            front = frontGas;
            back = backGas;
        }
        move = true;
       //anim = gameObject.GetComponent<Animator>();
        ChangeCarView();
        carArrow.SetActive(false);

    }

    private void ChangeCarView()
    {
        Vector3 v = this.GetComponent<Transform>().localScale;

        switch (dir)
        {
            case Direction.SW: //+x
                
                //anim.SetBool("Back", false);
                if (v.x < 0) { 
                    this.gameObject.GetComponent<Transform>().localScale = new Vector3(-v.x, v.y, v.z);
                    
                }
                this.GetComponent<SpriteRenderer>().sprite = front;

                break;
            case Direction.SE: //+y
                //anim.SetBool("Back", false);
                this.gameObject.GetComponent<Transform>().localScale = new Vector3(-v.x, v.y, v.z);
                this.GetComponent<SpriteRenderer>().sprite = front;
                break;
            case Direction.NE: //-x
                //anim.SetBool("Back", true);
                if (v.x < 0)
                    this.gameObject.GetComponent<Transform>().localScale = new Vector3(-v.x, v.y, v.z);
                this.GetComponent<SpriteRenderer>().sprite = back;
                break;
            case Direction.NW: //-y
                
                //anim.SetBool("Back", true);
                this.gameObject.GetComponent<Transform>().localScale = new Vector3(-v.x, v.y, v.z);
                this.GetComponent<SpriteRenderer>().sprite = back;
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (move)
        {
            MoveCar();
        }
    }

    private void MoveCar()
    {
        Vector3 v = this.GetComponent<Transform>().position;

        switch (dir)
        {
            case Direction.SW: //+x
                this.gameObject.GetComponent<Transform>().position = new Vector3(v.x - (float)speed, v.y - (float)(1 / Math.Sqrt(3)) * speed, v.z);
                break;
            case Direction.SE: //+y
                this.gameObject.GetComponent<Transform>().position = new Vector3(v.x + (float)speed, v.y - (float)(1 / Math.Sqrt(3)) * speed, v.z);
                break;
            case Direction.NE: //-x
                this.gameObject.GetComponent<Transform>().position = new Vector3(v.x + (float)speed, v.y + (float)(1 / Math.Sqrt(3)) * speed, v.z);
                break;
            case Direction.NW: //-y
                this.gameObject.GetComponent<Transform>().position = new Vector3(v.x - (float)speed, v.y + (float)(1 / Math.Sqrt(3)) * speed, v.z);
                break;
        }
    }

    public void ResumeCar()
    {
        this.move = true;
    }

    public void stopCar()
    {
        
        this.move = false;
        //this.anim.Stop();
    }

    public void destroyCar()
    {
        Destroy(this);
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        cv.incrDistance();
        //Recto
        if (other.gameObject.tag == Tag.S.ToString())
        {
       
            move = true;
            if (turn == false)
                turn = true;

        }
        //Intersección
        else if(turn){
            
            //Si el giro es hacia la izquierda debe girar más tarde para continuar por el carril derecho.
            if (other.gameObject.tag == Tag.L.ToString())
            {
                turn = false;
                StartCoroutine(WaitToTurn());
                
                
            }
            else if (other.gameObject.tag == Tag.R.ToString())
            {
                turn = false;
                switch (dir)
                {
                    case Direction.NW:
                        dir = Direction.NE;
                        break;
                    case Direction.NE:
                        dir = Direction.SE;
                        break;
                    case Direction.SW:
                        dir = Direction.NW;
                        break;
                    default:
                        dir = Direction.SW;
                        break;
                }
                ChangeCarView();
            }
            else if (other.gameObject.tag == Tag.SL.ToString())
            {
                turn = false;
            }
            else if (other.gameObject.tag == Tag.SR.ToString())
            {
                turn = false;
            }
            else if (other.gameObject.tag == Tag.LR.ToString())
            {
                turn = false;
            }
            else if (other.gameObject.tag == Tag.LR.ToString())
            {
                turn = false;
            }
            
        }
            
            
        
    }


    //La parte de cógido que quieres que se detenga debe estar dentro del método IEnumerator
    public IEnumerator WaitToTurn()
    {
        float delay = 0.17f;
        yield return new WaitForSeconds(delay);
        switch (dir)
        {

            case Direction.NW:
                dir = Direction.SW;
                break;
            case Direction.NE:
                dir = Direction.NW;
                break;
            case Direction.SW:
                dir = Direction.SE;
                break;
            default:
                dir = Direction.NE;
                break;
        }
        ChangeCarView();
        

    }



    //aparecen las flechas en los sitios a los que puede ir
    void OnTriggerEnter2D(Collider2D other)
    {
       if ((other.gameObject.name == "NW" ||
            other.gameObject.name == "NE" ||
            other.gameObject.name == "SE" ||
            other.gameObject.name == "SW") && (dir.ToString() != other.gameObject.name)
            && other != coll)
        {
            //Solo se muestran las flechas tras haber pasado por una carretera con la etiqueta Straight
            if (arrowsEnabled)
            {
                other.transform.parent.gameObject.GetComponent<Intersection>().ShowArrows(other.gameObject);
                coll = other;
                //Tras mostrar las flechas hay que esperar a pasar por una carretera recta.
                arrowsEnabled = false;
            }
        }
        
        //Tras pasar por una carretera recta ya se pueden volver a mostrar las flechas.
        if (other.gameObject.tag == "S")
            arrowsEnabled = true;

        if (other.gameObject.tag == "I")
        {
            this._intersection = true;
            
            stopCar();
        }
            
    }



    public bool intersection
    {
        get { return this._intersection; }
        set { this._intersection = value; }
    }

    public bool mapOpened
    {
        get { return this._mapOpened; }
        set { this._mapOpened = value; }
    }

   
}




