using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///<summary>
///The class that handles the intersections of the game (where the car must stop) 
//////La clase que se encarga de las intersecciones del jueg.(donde debe detenerse el coche)
///</summary>  
public class Intersection : MonoBehaviour
{

    private List<GameObject> arrows;
    private string lastTag;
    private GameObject lastRoad;
    ///<summary>
    /// Use this for initialization
    /// Se usa para inicializar
    ///</summary>
    void Awake()
    {
        arrows = new List<GameObject>();
    }
    ///<summary>
    ///Show the directions and wait for a selection to be made
    ///Muestra las direcciones y espera a que se seleccione una
    ///<param>
    ///GameObject touch 
    ///Wait until you select one of the addresses - Espera hasta que seleccione una de las direcciones
    /// </param>  
    ///</summary>
    public void ShowArrows(GameObject touch)
    {
        foreach (GameObject go in arrows)
        {
            if (go.transform.parent.gameObject != touch)
                go.SetActive(true);
        }
    }
    ///<summary>
    /// Hide the Arrows
    /// Oculta las flechas
    ///</summary>
    public void HideArrows()
    {
        foreach (GameObject go in arrows)
        {
            go.SetActive(false);
        }
    }
    ///<summary>
    ///Add arrows
    /// Añade direcciones
    ///</summary>
    public void AddArrow(GameObject go)
    {
        arrows.Add(go);
    }
    
    /// <summary>
    /// Find the last Road and return
    /// Retorna la condición del último tag o posición del mapa
    /// </summary>
    public void returnTag()
    {
        //GameObject.Find(this.lastRoad);
    }
    /// <summary>
    /// Change the map tag depending on the path selected by the user
    /// Cambia el tag del mapa en dependencia del camino seleccionado por el usuario
    /// /// </summary>
    /// <param String name="newTag"></param>
    /// <param CarMove name="dir"></param>
    public void ChangeTag(string newTag, CarMove.Direction dir)
    {
        //Para cambiar el tag, debemos guardar el que tenía anteriormente y devolvérselo en algún momento.
        switch (dir)
        {
            case CarMove.Direction.NW:
                //Busco a mi hijo SE
                this.lastRoad = transform.Find("SE").gameObject;
                this.lastTag = this.lastRoad.tag;

                transform.Find("SE").gameObject.tag = newTag;
                break;
            case CarMove.Direction.SE:
                //Busco a mi hijo NW
                this.lastRoad = transform.Find("NW").gameObject;
                this.lastTag = this.lastRoad.tag;

                transform.Find("NW").gameObject.tag = newTag;
                break;
            case CarMove.Direction.NE:
                //Busco a mi hijo SW
                this.lastRoad = transform.Find("SW").gameObject;
                this.lastTag = this.lastRoad.tag;


                transform.Find("SW").gameObject.tag = newTag;
                break;
            case CarMove.Direction.SW:
                //Busco a mi hijo NE

                this.lastRoad = transform.Find("NE").gameObject;
                this.lastTag = this.lastRoad.tag;


                transform.Find("NE").gameObject.tag = newTag;
                break;
        }
        Debug.Log(lastTag);
    }


    /// <summary>
    /// After selecting the address, put the car on the road
    /// Tras seleccionar la dirección pone el coche en carretera
    /// </summary>
    /// <param Collider2D name="other"></param>
    void OnTriggerExit2D(Collider2D other)
    {

        //Cuando salga el coche de la intersección cambiamos 
        if(other.tag == "Car") { 
            
            this.lastRoad.tag = this.lastTag;
        }
    }
}
