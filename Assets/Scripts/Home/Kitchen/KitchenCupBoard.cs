using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class KitchenCupBoard : MonoBehaviour
{ 
    public Sprite openned;
    public Sprite closed;

    private SpriteRenderer sr;
    private bool isClose = false;


    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();

        //Cerramos las puertas
        OpenCloseDoor();

        //Dehabilitamos los colliders de los hijos
        EnableColliders(false);
    }

    private void EnableColliders(bool flag)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Collider2D>().enabled = flag;
        }
    }

    void OnMouseOver()
    {
        Debug.Log("Funciona");
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log("Entra");
            OpenCloseDoor();
        }
    }

    private void OpenCloseDoor()
    {
        if (isClose)
        {

            isClose = false;

            //Change Sprite
            sr.sprite = openned;

            //Show child object (show objects inside in)

        }
        else
        {
            isClose = true;

            //Change Sprite 
            sr.sprite = closed;

            //Hide child object (it can't be seen because the door ir close)
        }

        ShowChildObjects(!isClose);
        EnableColliders(!isClose);
    }

    private void ShowChildObjects(bool show)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(show);
        }
    }

}