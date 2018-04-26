using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMTutorial : MonoBehaviour
{
    public GameObject errorPanel;
    public Text info;
    public Text noAnswer;
    int contNoAnswer = 0;

    public InputField textBx;                                       //Game object que contiene el inputField
    public GameObject[] tutorialPanels;                             //Array que contiene los paneles del tutorial
    public Text points;                                             //Texto para el panel final;
    public GameObject finalPanel;                                   //Panel final;

    private bool tutorial = true;                                   //Booleano que indica si ha terminado el tutorial o no
    private int contTutorial = 0;                                   //Contador de paneles mostrados del tutorial.

    private SortedDictionary<string, int> diccionary;               //Diccionario que contendrá las palabras y sinónimos de los objetos seleccionados.
    private SortedDictionary<string, int> answered;                 //Diccionario que contiene las palabras que se han respondido.

    private int attempts=0;                                           //Entero que controla el número de intentos.
    private int mistakes = 0;                                       //Entero que controla el número de errores del usuario.

    void Start()
    {
        diccionary = new SortedDictionary<string, int>();
        answered = new SortedDictionary<string, int>();
    }

   
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            contNoAnswer++;
            if(contTutorial == tutorialPanels.Length - 2)
            {
                tutorialPanels[contTutorial].SetActive(false);
                contTutorial++;
                tutorialPanels[contTutorial].SetActive(false);
                contTutorial++;
            }
            if (tutorial) tutorialUpdate();
            else gameUpdate();

        }

        if(contNoAnswer == 2)
        {
            noAnswer.gameObject.SetActive(true);
            contNoAnswer = 0;
        }       
    }

    #region Updates

    //Este es el update que ejecuta la lógica normal del juego
    void gameUpdate()
    {

        //Se comprueba si en el punto del mouse al hacer click hay colisión con algún objeto. Se devuelven todos los objetos en result.
        Collider2D[] result = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        int i = result.Length;
        if (i > 0)
        {
            contNoAnswer = 0;
            diccionary.Clear();
            textBx.gameObject.SetActive(true);
            textBx.Select();
            textBx.ActivateInputField();
        }
        while (i > 0)
        {
            i--;
            int id;
            
            string[] aux = result[i].GetComponent<Objeto>().dameDic(out id);       //El método dameDic devuelve una vector de palabras y un identificador que nos servirá para comprobar si se había respondido ya esa palabra.

            if (!answered.ContainsValue(id))                                        //Si no se había respondido ya añadimos las palabras de cada objeto al diccionario.
            {
                for (int w = 0; w < aux.Length; w++)
                {
                    diccionary.Add(aux[w], id);
                }


            }

        }

    }

    //Este update será el que se ejecuta en el momento de tutorial para mostrar los paneles adecuados
    //Funciona como el anterior.
    void tutorialUpdate()
    {
        bool error = true;
        if (contTutorial != 0)
        {
            Collider2D[] result = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            int i = result.Length;
            if(i > 0)
            {
                contNoAnswer= 0;
            }
            while (i > 0)
            {
                i--;
                if (result[i].name.ToLower().Equals("botella"))
                {
                    info.gameObject.SetActive(true);
                    error = false;
                    tutorialPanels[contTutorial].SetActive(false);
                    contTutorial++;
                    tutorialPanels[contTutorial].SetActive(true);

                    textBx.gameObject.SetActive(true);
                    textBx.Select();
                    textBx.ActivateInputField();

                    int id;
                    string[] aux = result[i].GetComponent<Objeto>().dameDic(out id);
                    if (!answered.ContainsValue(id))
                    {
                        for (int w = 0; w < aux.Length; w++)
                        {
                            diccionary.Add(aux[w], id);
                        }


                    }

                }
                
            }
            if (error)
            {
                
                errorPanel.SetActive(true);
                
            }
        }
        else
        {
            tutorialPanels[contTutorial].SetActive(false);
            contTutorial++;
            tutorialPanels[contTutorial].SetActive(true);
        }
    }
    #endregion Updates

    //Este método es llamado cada vez que se pulsa enter en el inputField y recibe de parámetro la palabra introducida.
    public void OnFieldEnter(string word)
    {

        if (diccionary.ContainsKey(word.ToLower()))                             //Si la palabra se encuentra en el diccionario la añadimos al diccionario de respondidos
        {
            int value = -1;
            diccionary.TryGetValue(word.ToLower(), out value);
            answered.Add(word, value);

            Debug.Log("Acertaste");
        }
        else
        {
            mistakes++;
            Debug.Log("Fallaste");
        }

        if (tutorial)
        {
            tutorialPanels[contTutorial].SetActive(false);
            contTutorial++;
            tutorialPanels[contTutorial].SetActive(true);
            tutorialPanels[contTutorial+1].SetActive(true);
            tutorial = false;
        }

        diccionary.Clear();                                                    //Limpiamod el diccionario.
        textBx.gameObject.SetActive(false);
        attempts++;

        textBx.Select();
        textBx.text = "";
    }

}
