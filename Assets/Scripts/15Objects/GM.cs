using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.IO;
using System.Text;


public class GM : MonoBehaviour {


    //Lista y Contador
    public GameObject lista;
    public Text listaText;
    public Text cont;
    private bool hayLista = false, hayCont = false;

    public Text points;                                             //Texto para el panel final;
    public GameObject finalPanel;                                   //Panel final;
    public InputField textBx;                                       //Game object que contiene el inputField
    public GameObject A, B;

    private SortedDictionary<string, int> diccionary;               //Diccionario que contendrá las palabras y sinónimos de los objetos seleccionados.
    private SortedDictionary<string, int> answered;                 //Diccionario que contiene las palabras que se han respondido.

    private GameObject levelSelectorPanel;
    private int attempts = 0;                                           //Entero que controla el número de intentos.
    private int mistakes = 0;                                       //Entero que controla el número de errores del usuario.

    FileStream fs;

    void Start () {
        levelSelectorPanel = GameObject.FindGameObjectWithTag("LevelSelector");
        levelSelectorPanel.SetActive(true);
        diccionary = new SortedDictionary<string, int>();
        answered = new SortedDictionary<string, int>();
        lista.SetActive(false);
        finalPanel.SetActive(false);
        textBx.gameObject.SetActive(false);
        A.SetActive(false); B.SetActive(false);
        string path = @".\Resultados.txt";

        if (File.Exists(path))
        {
            // Note that no lock is put on the
            // file and the possibility exists
            // that another process could do
            // something with it between
            // the calls to Exists and Delete.
            File.Delete(path);
        }
        fs = File.Create(path);

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            //Se comprueba si en el punto del mouse al hacer click hay colisión con algún objeto. Se devuelven todos los objetos en result.
            Collider2D[] result = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            int i = result.Length;
            if (i > 0)
            {
                diccionary.Clear();
                textBx.gameObject.SetActive(true);
                textBx.Select();
                textBx.ActivateInputField();
            }
            string log = "Se ha pinchado en: ";
            while (i > 0)
            {
                i--;
                int id;
                log += result[i].name+" ";
                Debug.Log("manpinchao " + result[i].name);
                string[] aux = result[i].GetComponent<Objeto>().dameDic(out id);       //El método dameDic devuelve una vector de palabras y un identificador que nos servirá para comprobar si se había respondido ya esa palabra.

                if (!answered.ContainsValue(id))                                        //Si no se había respondido ya añadimos las palabras de cada objeto al diccionario.
                {
                    for (int w = 0; w < aux.Length; w++)
                    {
                        diccionary.Add(aux[w], id);
                    }


                }

            }
            log += "\n";
            Byte[] info = new UTF8Encoding(true).GetBytes(log);
           if(result.Length > 0)  fs.Write(info, 0, info.Length);
        }

        if (attempts == 15)
        {
            finalPanel.SetActive(true);
            points.text = (attempts - mistakes).ToString() + "/15";
        }


    }


    //Este método es llamado cada vez que se pulsa enter en el inputField y recibe de parámetro la palabra introducida.
    public void OnFieldEnter(string word)
    {
        string log = "";
        if (diccionary.ContainsKey(word.ToLower()))                             //Si la palabra se encuentra en el diccionario la añadimos al diccionario de respondidos
        {
            int value = -1;
            diccionary.TryGetValue(word.ToLower(), out value);
            answered.Add(word, value);
            log = "\t✔ Ha respondido correctamente con: " + word;
            Debug.Log("Acertaste");
        }
        else if(word != "")
        {
            mistakes++;
            Debug.Log("Fallaste");
            if (answered.ContainsKey(word.ToLower())) log = "\t✘ Ha respondido una palabra repetida: " + word;
            else log = "\t✘ Ha respondido con error: " + word;
        }
        else
        {
            log = "\tHa cambiado de objeto";
        }
        log += "\n";

        Byte[] info = new UTF8Encoding(true).GetBytes(log);
        fs.Write(info, 0, info.Length);

        diccionary.Clear();                                                    //Limpiamod el diccionario.
        textBx.gameObject.SetActive(false);
        attempts++;

        if (hayCont) cont.text = "Has respondido " + attempts.ToString() + " objetos.\nTe quedan " + (15 - attempts).ToString();
        if (hayLista)
        {
            listaText.text += "\n- " + word;
        }
        
        textBx.Select();
        textBx.text = "";
    }


    public void SetLevel(string level)
    {
        if(level == "rand")
        {
            if (UnityEngine.Random.Range(0.0f, 100.0f) < 50) level = "A";
            else level = "B";
        }
        if (level == "A") A.SetActive(true);
        else B.SetActive(true);
       
        levelSelectorPanel.SetActive(false);       

    }
    public void Limpiatexto (Text txt)
    {
        txt.text = "";
    }
    public void HayLista(bool hay)
    {
        hayLista = hay;
        SwActive(lista);
    }
    public void HayCont(bool hay)
    {
        hayCont = hay;
        cont.text = "Has respondido 0 objetos\nTe quedan 15";
        SwActive(cont.gameObject);
    }
    public void SwActive(GameObject ob)
    {
        ob.SetActive(!ob.active);
    }


}
