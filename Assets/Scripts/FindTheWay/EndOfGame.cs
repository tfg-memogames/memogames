using UnityEngine;

public class EndOfGame : MonoBehaviour {

    public CanvasManager cm;

    //Crear etiqueta llamada GameState y asignarsela a GameState
    void Start()
    {
        cm = GameObject.FindObjectOfType<CanvasManager>();
        
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Car")
        {
            cm.win();
        }
    }
}
