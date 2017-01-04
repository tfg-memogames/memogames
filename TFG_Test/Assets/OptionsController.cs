using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {


    DialogueManager _dialog;

    //Constantes del diálogo.
    const int TRAVEL_DETAILS = 6;

    //Botones del diálogo.
    private GameObject _button1;
    private GameObject _button2;

    /*Contador sirve para saber en qué parte del diálogo estamos.
     * 1 - Preguntamos al usuario si necesita que repitamos la información del viaje.
     */

    private int _contador;
	
	void Start () {
        _dialog = GetComponentInParent<DialogueManager>();
        _contador = 1;
        _button1 = GameObject.Find("Button 1");
        _button2 = GameObject.Find("Button 2");
        _button1.SetActive(false);
        _button2.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void button1()
    {
        if (_contador == 1) //Necesita que le repitan los detalles del viaje.
        {
            _dialog.currentDialogueIndex = TRAVEL_DETAILS;
            
        }

    }

    public void button2()
    {
        
        if (_contador == 1) //Ya sabe los detalles del viaje.
        {
            _dialog.currentDialogueIndex++;
            _contador++;
            
        }

    }


    public void disableButtons()
    {
        _button1.SetActive(false);
        _button2.SetActive(false);
        _dialog.waitingEntry = false;
    }
}
