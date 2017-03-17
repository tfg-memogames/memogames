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

    //private Animator anim;

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
            
            if (other.gameObject.tag == Tag.L.ToString())
            {
                turn = false;
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

    //aparecen las flechas en los sitios a los que puede ir
    void OnTriggerEnter2D(Collider2D other)
    {
       if ((other.gameObject.name == "NW" ||
            other.gameObject.name == "NE" ||
            other.gameObject.name == "SE" ||
            other.gameObject.name == "SW") && (dir.ToString() != other.gameObject.name)
            && other != coll)
        {
            other.transform.parent.gameObject.GetComponent<Intersection>().ShowArrows(other.gameObject);
            coll = other;
        }

        if (other.gameObject.tag == "I")
        {
            stopCar();
        }
            
    }
}
