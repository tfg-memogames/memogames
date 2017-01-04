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
    private GameObject _button3;
    private GameObject _button4;

    /*Contador sirve para saber en qué parte del diálogo estamos.
     * 1 - Preguntamos al usuario si necesita que repitamos la información del viaje.
     */

    private int _contador;

    //Aciertos en las preguntas
    private int _goals = 0;
	
	void Start () {
        _dialog = GetComponentInParent<DialogueManager>();


        //PRUEBAS
        _contador = 1; //DEBE EMPEZAR EN 1 
        //PRUEBAS



        _button1 = GameObject.Find("Button 1");
        _button2 = GameObject.Find("Button 2");
        _button3 = GameObject.Find("Button 3");
        _button4 = GameObject.Find("Button 4");
        _button1.SetActive(false);
        _button2.SetActive(false);
        _button3.SetActive(false);
        _button4.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}



    public void prepareOptions(string l)
    {

        string[] line = l.Split('%', '%');
        int numberButtons = 0;
        int.TryParse(line[1], out numberButtons);
        buttonActivation(numberButtons, line);

    }


    


    public void button1()
    {
        switch(_contador)
        {
            case 1: //Necesita que le repitan los detalles del viaje.
                _dialog.currentDialogueIndex = TRAVEL_DETAILS;
                break;
            case 2: //Ha elegido la opción "Acapulco" (CORRECTA).
                _dialog.destination = "Acapulco";
                _dialog.currentDialogueIndex++;
                _contador++;
                _goals++;
                break;
            case 3: //Ha elegido la opción "Lunes 11:45".
                _dialog.flightDate = "Lunes 11:45";
                _dialog.currentDialogueIndex++;
                _contador++;
                break;
            case 4: //Ha elegido la opción "Maleta" (CORRECTA).
                _dialog.suitCase = "Maleta";
                _dialog.currentDialogueIndex++;
                _contador++;
                _goals++;
                break;

        }

    }

    public void button2()
    {
        
        switch(_contador) 
        {
            case 1: //Ya sabe los detalles del viaje.
                _dialog.currentDialogueIndex++;
                _contador++;
                break;
            case 2: //Ha elegido la opción "París".
                _dialog.destination = "París";
                _dialog.currentDialogueIndex++;
                _contador++;
                break;
            case 3: //Ha elegido la opción "Lunes 15:35".
                _dialog.flightDate = "Lunes 15:35";
                _dialog.currentDialogueIndex++;
                _contador++;
                break;
            case 4: //Ha elegido la opción "Equipaje de mano" (CORRECTA).
                _dialog.suitCase = "Equipaje de mano";
                _dialog.currentDialogueIndex++;
                _contador++;
                _goals++;
                break;

        }

    }


    public void button3()
    {

        switch (_contador)
        {
            case 2: //Ha elegido la opción "Los Ángeles".
                _dialog.destination = "Los Ángeles";
                _dialog.currentDialogueIndex++;
                _contador++;
                break;
            case 3: //Ha elegido la opción "Viernes 11:45" (Correcta).
                _dialog.flightDate = "Viernes 11:45";
                _dialog.currentDialogueIndex++;
                _contador++;
                _goals++;
                break;

        }

    }

    public void button4()
    {

        switch (_contador)
        {
            case 2: //Ha elegido la opción "Berlín".
                _dialog.destination = "Berlín";
                _dialog.currentDialogueIndex++;
                _contador++;
                break;
            case 3: //Ha elegido la opción "Sábado 13:45".
                _dialog.flightDate = "Sábado 13:45";
                _dialog.currentDialogueIndex++;
                _contador++;
                break;

        }

    }

    public void disableButtons()
    {
        _button1.SetActive(false);
        _button2.SetActive(false);
        _button3.SetActive(false);
        _button4.SetActive(false);
        _dialog.waitingEntry = false;
    }


    private void buttonActivation(int number, string[] buttonNames)
    {
        //En las dos primeras posiciones de buttonNames viene basura.
        switch (number)
        {
            case 1:
                if (buttonNames[2] != null)
                    activeOneButton(buttonNames[2]);
                break;
            case 2:
                if (buttonNames[2] != null && buttonNames[3] != null)
                    activeTwoButton(buttonNames[2], buttonNames[3]);
                break;
            case 3:
                if (buttonNames[2] != null && buttonNames[3] != null && buttonNames[4] != null)
                    activeThreeButton(buttonNames[2], buttonNames[3], buttonNames[4]);
                break;
            case 4:
                if (buttonNames[2] != null && buttonNames[3] != null & buttonNames[4] != null && buttonNames[5] != null)
                    activeFourButton(buttonNames[2], buttonNames[3], buttonNames[4], buttonNames[5]);
                break;

        }

    }

    private void activeOneButton(string butName)
    {
        _button1.gameObject.SetActive(true);
        _button1.GetComponentInChildren<Text>().text = butName;
    }

    private void activeTwoButton(string butName1, string butName2)
    {
        _button1.gameObject.SetActive(true);
        _button1.GetComponentInChildren<Text>().text = butName1;
        //_button1.GetComponent<Text>() = butName1;
        _button2.gameObject.SetActive(true);
        //butName2.Substring('\r');
        _button2.GetComponentInChildren<Text>().text = butName2;
        //_button2.name = butName2;


    }

    private void activeThreeButton(string butName1, string butName2, string butName3)
    {
        _button1.SetActive(true);
        _button1.GetComponentInChildren<Text>().text = butName1;
        _button2.SetActive(true);
        _button2.GetComponentInChildren<Text>().text = butName2;
        _button3.SetActive(true);
        _button3.GetComponentInChildren<Text>().text = butName3;




    }
    private void activeFourButton(string butName1, string butName2, string butName3, string butName4)
    {

        _button1.SetActive(true);
        _button1.GetComponentInChildren<Text>().text = butName1;
        _button2.SetActive(true);
        _button2.GetComponentInChildren<Text>().text = butName2;
        _button3.SetActive(true);
        _button3.GetComponentInChildren<Text>().text = butName3;
        _button4.SetActive(true);
        _button4.GetComponentInChildren<Text>().text = butName4;
    }
}
