using UnityEngine;

public class EndOfGame : MonoBehaviour {

    public GameManager gm;

    //Crear etiqueta llamada GameState y asignarsela a GameState
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Car")
        {
            gm.win();
        }
    }
}
