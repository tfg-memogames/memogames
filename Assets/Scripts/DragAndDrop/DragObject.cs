using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]

///<summary>
///It is responsible for the drags of the mouse
///Se encarga de los movimientos del raton 
///</summary>
public class DragObject: MonoBehaviour {

    public GameObject[] destiny;

    private Collider2D coll;
    private Vector2 startPoint;
    ///<summary>
    ///Start´s the processes of entry and exit of the game
    ///Inicializa los procesos de entrada y salida del juego
    ///</summary>
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }
    
    //no se usa comprobar
    private void OnMouseOver()
    {
        //Debug.Log("OnMouseOver");
    }
    
    void OnMouseDown()
    {
        Debug.Log("click on" + gameObject.name);
        startPoint = transform.position;
    }

    void OnMouseDrag()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

        transform.position = new Vector3(pos_move.x, pos_move.y, -0.1F);
    }

    void OnMouseUp()
    {
        int i = 0;

        while (i < destiny.Length && !coll.IsTouching(destiny[i].GetComponent<Collider2D>()))
            i++;

        if (i == destiny.Length) returnToStartPoint();
        else
        {
            Debug.Log("Touching with " + destiny[i].gameObject.name);
            destiny[i].gameObject.SendMessage("ItemWasDropped", this.gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void returnToStartPoint()
    {
        transform.position = startPoint;
    }
}