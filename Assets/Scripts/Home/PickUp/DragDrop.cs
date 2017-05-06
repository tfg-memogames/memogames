using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class DragDrop: MonoBehaviour {

    private Collider2D coll;
    private Vector2 startPoint;

	void Start(){
        coll = GetComponent<Collider2D>();
    }

	void OnMouseDown(){
        startPoint = transform.position;
    }

    void OnMouseDrag(){
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

        transform.position = new Vector3(pos_move.x, pos_move.y, -1);
    }

    void OnMouseUp(){

        int i = 0;
	
		while (i < LoadRoom.sites.Length && !coll.IsTouching(LoadRoom.sites[i].GetComponent<Collider2D>()))
            i++;


		if (i == LoadRoom.sites.Length) 
			transform.position = startPoint;
        else
			LoadRoom.dictionary[LoadRoom.sites[i].name]=this.name;
        
    }
}