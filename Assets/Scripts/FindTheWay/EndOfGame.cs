using UnityEngine;

public class EndOfGame : MonoBehaviour {

    private GameState gs;

    //Crear etiqueta llamada GameState y asignarsela a GameState
    void Start()
    {
        gs = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Car")
        {
            gs.win();
        }
    }
}
