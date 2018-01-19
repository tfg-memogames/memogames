using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RAGE.Analytics;

///<summary>
///Class that controls the whole game
///Clase que controla todo el juego
///</summary>
public class CanvasManager : MonoBehaviour
{
    private CursorMode cursorMode = CursorMode.Auto;
    public static bool end = false;                                                       //Flag que determina el final del juego   
    public GameObject electricBar;                                                      //Barra de combustible para el coche electrico.
    public GameObject gasoilBar;                                                        //Barra de combustible para el coche de gasolina.
    public Text dist_Text;                                                              //Texto con la distancia recorrida

    //Struct con parametros para el popUp
    [System.Serializable]
    public struct PopUp
    {
        public GameObject endOfGamePanel;                                               //Panel de final de la partida
        public Button goToMenu;                                                         //Botón para volver al menu de selección de niveles
        public Text message;                                                            //Mensaje que se muestra en el panel
        public Text scoreText;                                                          //Mensaje para la puntuación.
        public GameObject starsPanel;                                                   //Panel de estrellas
        public GameObject star;                                                         //Estrellas
        public Button nextLevelButton;                                                  //Botón de siguiente nivel.
        public Button restartButton;                                                    //Botón de reinicio.
    }
    public PopUp popUp;

    public bool miniGame = true;                                                        //Variable que indica si se juega en modo historia o en modo miniJuego.

    private GameObject currentBar;                                                      //Barra de energía con la que se está jugando.
    private GameObject bar;                                                             //Barra que indica la cantidad de combustible.
    private RectTransform rt;                                                           //Rectángulo para realizar las transformaciones en el objeto de la barra de combustible.
    private float totalEnergy;                                                          //Variable con el valor total de combustible.
    private float currentConsum = 0;                                                    //Variable que gestiona el consumo.

    private GameState gs;                                                               //GameState
    private GameManager gm;                                                             //GameManager


    //=======================================================================================================================
    ///<summary>
    /// Use this for initialization
    ///</summary>
    void Start()
    {
        this.dist_Text.text = "Distancia: 0";                                                           //Se escribe el cartel con la distancia
        gs = GameObject.FindObjectOfType<GameState>();                                                  //Se asigna el objeto GameState
        if (gs.carType == GameState.Car.ELECTRIC) currentBar = electricBar;                             //Si el choche es eleéctrico se asigna a la barra actual la barra electrica. Si es de gasolina, la de gasolina.
        else currentBar = gasoilBar;
        currentBar.SetActive(true);

        gm = GameObject.FindObjectOfType<GameManager>();                                                //Se asigna el objeto GameManager

        bar = currentBar.transform.GetChild(0).transform.GetChild(0).gameObject;                        //Se asigna la barra de porcentaje de combustible
        this.rt = bar.GetComponent<RectTransform>();                                                    //Se configura el rectángulo de la barra para poder decrementarla
        this.totalEnergy = rt.sizeDelta.x;                                                              //Se inicializa el valor total del combstible
        this.popUp.endOfGamePanel.SetActive(false);                                                    //Se ocultan los popUps de partida completa y fin de la partida.
        this.popUp.star.SetActive(false);
    }
    ///<summary>
    ///Load the tutorial video
    /// Carga el video tutorial
    ///</summary>
    public void video()
    {
        SceneManager.LoadScene("Video_Tutorial");

    }

    private void FixedUpdate()
    {
        if (end)
        {
            this.popUp.endOfGamePanel.SetActive(true);
        }

    }
    //======================================================================================================================================
    ///<summary>
    ///Set the bar according to the percentage of consumption
    /// Configura la barra según el porcentaje de consumo
    ///</summary>
    public void setPercentageOfEnergy(float newValue)
    {
        float x = (newValue * totalEnergy) / 100;
        float y = rt.localPosition.y;
        float z = rt.localPosition.z;

        rt.localPosition = new Vector3(-x, y, z);
    }

    //========================================================================================================================================
    ///<summary>   
    ///Buttons
    ///Botones
    ///</summary>
    public void mapButton_Clicked()
    {
        if (!end)
            gm.showMap();

    }
    ///<summary>        
    ///Menu Buttons
    ///Menu Botones
    ///</summary>
    public void menuButtonClicked()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        end = false;
        SceneManager.LoadScene("Level_Selector");
    }

    public void restartButtonClicked()
    {
        end = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //=========================================================================================================================================

    //Se colocan las estrellas al ganar la partida. Number contiene el número de estrellas consegudas.
    private void instatiateStars(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject star = Instantiate(this.popUp.star);                                                //Se instancian las estrellas
            star.SetActive(true);                                                                          //Se activan para hacerlas visibles
            star.transform.parent = this.popUp.starsPanel.transform;                                        //Se transforma el panel de estrellas para encajarlo en el popUp
            star.transform.localPosition = Vector3.zero;
            star.transform.localScale = Vector3.one;
        }
    }

    //===========================================================================================================================================
    ///<summary>
    ///Sample popup you've won (unlock the next level)
    ///Muestra popup con que has ganado (desbloqueará el siguiente nivel)
    ///</summary>
    public void win(float points)
    {
        end = true;
        gm.destroyCar();                                                             //Se destruye el coche
        this.popUp.message.text = "¡Has llegado a tu destino!";                      //Se escribe el mensaje de finalización
        this.popUp.message.color = new Color(0.34F, 0.41F, 0.39F, 1);                // 586A45FF divide (100 / FF (256 bits)) * rgb

        //Se configura el mensaje en función de los puntos obtenidos.
        if (points >= 0.95)
        {
            this.popUp.scoreText.text = "¡Has conseguido el máximo de estrellas!";
            instatiateStars(3);
        }
        else if (points > 0.80 && points < 0.95)
        {
            this.popUp.scoreText.text = "¡Has conseguido 2 estrellas (Max. 3)!";
            instatiateStars(2);
        }
        else if (points > 0.6 && points < 0.80)
        {
            this.popUp.scoreText.text = "¡Has conseguido 1 estrella (Max. 3)!";
            instatiateStars(1);
        }
        else
        {
            this.popUp.scoreText.text = "No has conseguido ninguna estrella :(";
        }

        if (miniGame)
            this.popUp.endOfGamePanel.SetActive(true);                                                    //Si estamos en modo minijuego activamos el panel de fin de partida.
        else
            SceneManager.LoadScene("Hall");                                                                //Si estamos en modo historia se carga la escena del hall
    }
    //=======================================================================================================================================
    ///<summary>
    ///It is called when you lose the game and finish everything
    ///Se llama cuando pierdes la partida e finaliza todo 
    ///</summary>
    public void lose()
    {
        end = true;
        gm.destroyCar();                                                                                    //Se destruye el coche
        this.popUp.endOfGamePanel.SetActive(true);                                                          //Se activa el panel de fin de la partida
        this.popUp.message.text = "¡Te has quedado sin combustible!";                                       //Se actualiza el mensaje
        popUp.scoreText.text = "";
        this.popUp.message.color = new Color(0.4F, 0.04F, 0.16F, 1);                                        // 680C2AFF divide (100 / FF (256 bits)) * rgb

        //Si estamos en modo hitoria
        if (!this.miniGame)
        {
            this.popUp.restartButton.gameObject.SetActive(true);
            this.popUp.nextLevelButton.gameObject.SetActive(false);
        }

    }
    //=======================================================================================================================================
}


