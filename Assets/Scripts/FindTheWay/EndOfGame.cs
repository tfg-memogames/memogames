using UnityEngine;
///<summary>
///The class that is responsible for finish the game 
///La clase que se encarga de finalizar el juego
///</summary>  
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
