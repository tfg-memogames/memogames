using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : MonoBehaviour
{

    private List<GameObject> arrows;
    private string lastTag;
    private GameObject lastRoad;

    // Use this for initialization
    void Awake()
    {
        arrows = new List<GameObject>();
    }

    public void ShowArrows(GameObject touch)
    {
        foreach (GameObject go in arrows)
        {
            if (go.transform.parent.gameObject != touch)
                go.SetActive(true);
        }
    }

    public void HideArrows()
    {
        foreach (GameObject go in arrows)
        {
            go.SetActive(false);
        }
    }

    public void AddArrow(GameObject go)
    {
        arrows.Add(go);
    }

    public void returnTag()
    {
        //GameObject.Find(this.lastRoad);
    }

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



    void OnTriggerExit2D(Collider2D other)
    {

        //Cuando salga el coche de la intersección cambiamos 
        if(other.tag == "Car") { 
            
            this.lastRoad.tag = this.lastTag;
        }
    }
}
