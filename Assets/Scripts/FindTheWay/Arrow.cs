using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///<summary>
///Take care of the instructions that the car will take
///Se encarga de las direcciones que va a tomar el coche
///</summary>
public class Arrow : MonoBehaviour {


    CarMove car;
    ///Start´s the processes of entry and exit of the car
    ///Inicializa los procesos de entrada y salida del coche
    private void Start()
    {
        //Debug.Log("Soy: " + this.gameObject.name);
        car = GameObject.FindGameObjectWithTag("Car").GetComponent<CarMove>();
        //Debug.Log(car);
    }

    ///<summary>
    ///Take care of the instructions that the car will take
    ///Se encarga de las direcciones que va a tomar el coche
    ///</summary>
    void OnMouseDown()
    {
        //Si esta baldosa tiene la flecha activada entonces permitimos que se pulse en ella
        bool arrowActive = false;
        GameObject arrow = this.gameObject.transform.GetChild(0).gameObject;
        
        if (arrow.activeInHierarchy)
            arrowActive = true;

        //No se permite pulsar una flecha si está el mapa abierto
        if (!car.mapOpened && arrowActive && !car.isMoving())
        {
            string tagPosition = null;
            Intersection inter = transform.parent.gameObject.GetComponent<Intersection>();
            inter.HideArrows();
            car.ResumeCar();
            //El coche ya no está en una interseción.
            car.intersection = false;       
            
            switch (car.dir())
            {
                case CarMove.Direction.NE:

                    switch (name)
                    {
                        case "NW":
                            tagPosition = "L";
                            break;
                        case "NE":
                            tagPosition = "S";
                            break;
                        case "SE":
                            tagPosition = "R";
                            break;
                    }

                    break;
                case CarMove.Direction.SE:

                    switch (name)
                    {
                        case "SW":
                            tagPosition = "R";
                            break;
                        case "SE":
                            tagPosition = "S";
                            break;
                        case "NE":
                            tagPosition = "L";
                            break;
                    }

                    break;

                case CarMove.Direction.SW:

                    switch (name)
                    {
                        case "SE":
                            tagPosition = "L";
                            break;
                        case "SW":
                            tagPosition = "S";
                            break;
                        case "NW":
                            tagPosition = "R";
                            break;
                    }

                    break;

                default:

                    switch (name)
                    {
                        case "SW":
                            tagPosition = "L";
                            break;
                        case "NW":
                            tagPosition = "S";
                            break;
                        case "NE":
                            tagPosition = "R";
                            break;
                    }

                    break;
            }

            inter.ChangeTag(tagPosition, car.dir());
        }
    }
}