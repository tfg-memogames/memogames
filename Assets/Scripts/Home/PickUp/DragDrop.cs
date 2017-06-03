using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class DragDrop: MonoBehaviour {

    private Collider2D coll;
    private Vector2 startPoint;
	public Sprite newSprite;

	private GameObject room;

	void Start(){
		room = GameObject.Find ("PickUp");

        coll = GetComponent<Collider2D>();
    }

	void OnMouseDown(){
        startPoint = transform.position;
		room.GetComponent<LoadRoom> ().caught++;
    }

    void OnMouseDrag(){
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

        transform.position = new Vector3(pos_move.x, pos_move.y, -0.01F);
    }

    void OnMouseUp(){

        int i = 0;
	
		while (i < room.GetComponent<LoadRoom> ().roomSites.Length && !coll.IsTouching(room.GetComponent<LoadRoom> ().roomSites[i].GetComponent<Collider2D>()))
            i++;


		if (i == room.GetComponent<LoadRoom> ().roomSites.Length){
			transform.position = startPoint;
		}else {
			if (room.GetComponent<LoadRoom> ().order [room.GetComponent<LoadRoom> ().roomSites[i].name] == this.name && 
				room.GetComponent<LoadRoom> ().dictionary [room.GetComponent<LoadRoom> ().roomSites[i].name] != this.name) {
				room.GetComponent<LoadRoom> ().corrects++;
				gameObject.GetComponent<SpriteRenderer> ().sprite = newSprite;
			}else 
				if(room.GetComponent<LoadRoom> ().dictionary [room.GetComponent<LoadRoom> ().roomSites[i].name] != this.name)
					room.GetComponent<LoadRoom> ().mistakes++;
			

			room.GetComponent<LoadRoom> ().dictionary [room.GetComponent<LoadRoom> ().roomSites [i].name] = this.name;
		}
    }
}