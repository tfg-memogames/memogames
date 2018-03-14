using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using RAGE.Analytics;
using UnityEngine.SceneManagement;


public class GM : MonoBehaviour {

    public Text feedbackResponse;
    private GameState15O gameS;

    public Text noAnswer;
    int contNoAnswer = 0;
    //Lista y Contador
    private String level = "A";
    private bool isRandom = false;
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
    private SortedDictionary<string, int> simpleDictionary;        //Diccionario que contiene las palabras que se han respondido en su version simplificada.

    private GameObject levelSelectorPanel;
    private int attempts = 0;                                       //Entero que controla el número de intentos.
    private int totalAttempts = 15;                                 
    private int mistakes = 0;                                       //Entero que controla el número de errores del usuario.

    FileStream fs;

    void Start () {

        this.gameS = GameObject.FindObjectOfType<GameState15O>();
       
        levelSelectorPanel = GameObject.FindGameObjectWithTag("LevelSelector");
        levelSelectorPanel.SetActive(true);
        diccionary = new SortedDictionary<string, int>();
        answered = new SortedDictionary<string, int>();
        simpleDictionary = new SortedDictionary<string, int>();
        lista.SetActive(false);
        finalPanel.SetActive(false);
        textBx.gameObject.SetActive(false);
        A.SetActive(false); B.SetActive(false);

        string path;
        if (gameS.fileConfig)
        {
            path = @".\configFile15O.txt";
            if (!File.Exists(path))
            {
                // Note that no lock is put on the
                // file and the possibility exists
                // that another process could do
                // something with it between
                // the calls to Exists and Delete.
                fs = File.Create(path);
                Byte[] info = new UTF8Encoding(true).GetBytes("A");
                fs.Write(info, 0, info.Length);
                fs.Close();
            }
           
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            string option = file.ReadLine();
            Debug.Log(option);
            file.Close();
            SetLevel(option);
            gameS.fileConfig = false;
        }

        path = @".\Resultados.txt";

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
            contNoAnswer++;
            //Se comprueba si en el punto del mouse al hacer click hay colisión con algún objeto. Se devuelven todos los objetos en result.
            Collider2D[] result = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            int i = result.Length;
            if (i > 0)
            {
                contNoAnswer = 0;
                simpleDictionary.Clear();
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

                simpleDictionary.Add(result[i].name, id);
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

        if (contNoAnswer == 2)
        {
            noAnswer.gameObject.SetActive(true);
            contNoAnswer = 0;
        }

        if (attempts == totalAttempts)
        {
            gameS.fileConfig = false;
            finalPanel.SetActive(true);
            points.text = (attempts - mistakes).ToString() + "/" + totalAttempts;

            // Completed the 15 Objects level
            bool failed = (float)mistakes > ((float)totalAttempts / 2.0f);
            float score = 1.0f - ((float)mistakes / (float)totalAttempts);
            Tracker.T.Completable.Completed(level, CompletableTracker.Completable.Level, !failed, score);
        }
    }


    //Este método es llamado cada vez que se pulsa enter en el inputField y recibe de parámetro la palabra introducida.
    public void OnFieldEnter(string word)
    {


        string log = "";
        if (diccionary.ContainsKey(word.ToLower()))                             
            //Si la palabra se encuentra en el diccionario la añadimos al diccionario de respondidos
        {

            int value = -1;
            diccionary.TryGetValue(word.ToLower(), out value);
            answered.Add(word, value);
            log = "\t✔ Ha respondido correctamente con: " + word;
            Debug.Log("Acertaste");
            feedbackResponse.gameObject.SetActive(true);
            feedbackResponse.text = "Has respondido " + word;
            

            // Tracking
            Dictionary<String, bool> simpleVarDictionary = new Dictionary<string, bool>();
            foreach (KeyValuePair<string, int> attachStat in simpleDictionary)
            {
                int simpleValue = -1;
                simpleDictionary.TryGetValue(attachStat.Key, out simpleValue);
                if(simpleValue == value)
                {
                    simpleVarDictionary.Add(attachStat.Key, true);
                } else
                {
                    simpleVarDictionary.Add(attachStat.Key, false);
                }
                
                // Mappings por si hacen falta en el analysis
                String varKey = "mappings_" + attachStat.Key;
                String varValue = " ";
                foreach (KeyValuePair<string, int> dicKeyValues in diccionary)
                {
                    if (dicKeyValues.Value == attachStat.Value)
                    {
                        varValue += dicKeyValues.Key + ",";
                    }
                }
                if (varValue.EndsWith(","))
                {
                    varValue = varValue.Substring(0, varValue.Length - 1);
                }
                if(varKey != null && varValue != null)
                    Tracker.T.setVar(varKey, varValue);
            }
            if(simpleVarDictionary != null)
                Tracker.T.setVar("targets", simpleVarDictionary);

            foreach (KeyValuePair<string, int> attachStat in diccionary)
            {
                if(attachStat.Key != null)
                    Tracker.T.setVar(attachStat.Key, attachStat.Value);
            }
            // No hubo cambio de objeto
            Tracker.T.setVar("object-changed", 0);
            // Respuesta correcta
            Tracker.T.setVar("correct", 1);
            Tracker.T.setSuccess(true);
            Tracker.T.Alternative.Selected(level, word);
        }
        else if(word != "")
        {
            mistakes++;
            Debug.Log("Fallaste");
            if (answered.ContainsKey(word.ToLower())) log = "\t✘ Ha respondido una palabra repetida: " + word;
            else log = "\t✘ Ha respondido con error: " + word;
            feedbackResponse.gameObject.SetActive(true);
            feedbackResponse.text = "Has respondido " + word;

            // Tracking
            Dictionary<String, bool> simpleVarDictionary = new Dictionary<string, bool>();
            foreach (KeyValuePair<string, int> attachStat in simpleDictionary)
            {
                simpleVarDictionary.Add(attachStat.Key, false);

                // Mappings por si hacen falta en el analysis
                String varKey = "mappings_" + attachStat.Key;
                String varValue = " ";
                foreach (KeyValuePair<string, int> dicKeyValues in diccionary)
                {
                    if (dicKeyValues.Value == attachStat.Value)
                    {
                        varValue += dicKeyValues.Key + ",";
                    }
                }
                if (varValue.EndsWith(","))
                {
                    varValue = varValue.Substring(0, varValue.Length - 1);
                }
                if(varKey != null && varValue != null)
                    Tracker.T.setVar(varKey, varValue);
            }
            if(simpleVarDictionary != null)
                Tracker.T.setVar("targets", simpleVarDictionary);

            foreach (KeyValuePair<string, int> attachStat in diccionary)
            {
                if(attachStat.Key != null)
                    Tracker.T.setVar(attachStat.Key, attachStat.Value);
            }
            // No hubo cambio de objeto
            Tracker.T.setVar("object-changed", 0);
            // Respuesta incorrecta
            Tracker.T.setVar("correct", 0);
            Tracker.T.setSuccess(false);
            Tracker.T.Alternative.Selected(level, word);
        }
        else
        {
            log = "\tHa cambiado de objeto";

            // Tracking object changed without answer
            Dictionary<String, bool> simpleVarDictionary = new Dictionary<string, bool>();
            foreach (KeyValuePair<string, int> attachStat in simpleDictionary)
            {
                simpleVarDictionary.Add(attachStat.Key, false);

                // Mappings por si hacen falta en el analysis
                String varKey = "mappings_" + attachStat.Key;
                String varValue = " ";
                foreach (KeyValuePair<string, int> dicKeyValues in diccionary)
                {
                    if(dicKeyValues.Value == attachStat.Value)
                    {
                        varValue += dicKeyValues.Key + ",";
                    }
                }
                if(varValue.EndsWith(","))
                {
                    varValue = varValue.Substring(0, varValue.Length - 1);
                }
                if(varKey != null && varValue != null)
                    Tracker.T.setVar(varKey, varValue);
            }
            if(simpleVarDictionary != null)
                Tracker.T.setVar("targets", simpleVarDictionary);

            foreach (KeyValuePair<string, int> attachStat in diccionary)
            {
                if(attachStat.Key != null)
                    Tracker.T.setVar(attachStat.Key, attachStat.Value);
            }
            // Hubo cambio de objeto
            Tracker.T.setVar("object-changed", 1);
            // Respuesta desconocida
            Tracker.T.setVar("correct", -1);
            Tracker.T.setSuccess(false);
            Tracker.T.Alternative.Selected(level, "empty");
        }
        log += "\n";

        Byte[] info = new UTF8Encoding(true).GetBytes(log);
        fs.Write(info, 0, info.Length);

        diccionary.Clear();                                                    //Limpiamod el diccionario.
        simpleDictionary.Clear();
        textBx.gameObject.SetActive(false);
        attempts++;

        if (hayCont) cont.text = "Has respondido " + attempts.ToString() + " objetos.\nTe quedan " + (totalAttempts - attempts).ToString();
        if (hayLista)
        {
            listaText.text += "\n- " + word;
        }
        
        textBx.Select();
        textBx.text = "";

        // Progreso del nivel actual
        float progress = (float)attempts / (float)totalAttempts;
        Tracker.T.Completable.Progressed(level, CompletableTracker.Completable.Level, progress);
    }


    public void SetLevel(string level)
    {
        contNoAnswer = 0;
        if(level == "rand")
        {
            isRandom = true;
            if (UnityEngine.Random.Range(0.0f, 100.0f) < 50) level = "A";
            else level = "B";
        } else
        {
            isRandom = false;
        }
        if (level == "A")  A.SetActive(true);
        else if (level == "B") B.SetActive(true);
        else {
            gameS.fileConfig = true;
            SceneManager.LoadScene("Tutorial15O");
        } 

        this.level = level;
        levelSelectorPanel.SetActive(false);

        // Started the 15 Objects level
        Tracker.T.Completable.Initialized(level, CompletableTracker.Completable.Level);
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
