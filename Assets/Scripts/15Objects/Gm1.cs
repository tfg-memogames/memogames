using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class Gm1 : MonoBehaviour
{

    // Use this for initialization
    public GameObject textBx;  

    public GameObject[] tutorialPanels;

    private SortedDictionary<string, int> diccionary;
    private SortedDictionary<string, int> respondidos;

    private bool tutorial = true;
    private int contTutorial = 0;

    void Start()
    {
        diccionary = new SortedDictionary<string, int>();
        respondidos = new SortedDictionary<string, int>();
    }

    void tutorialUpdate()
    {
        if (contTutorial != 0)
        {
            Collider2D[] result = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            int i = result.Length;
            while (i > 0)
            {
                i--;
                if (result[i].name == "Botella")
                {

                    tutorialPanels[contTutorial].SetActive(false);
                    contTutorial++;
                    tutorialPanels[contTutorial].SetActive(true);

                    textBx.SetActive(true);

                    int id;
                    string[] aux = result[i].GetComponent<opalopa>().dameDic(out id);
                    if (!respondidos.ContainsValue(id))
                    {
                        for (int w = 0; w < aux.Length; w++)
                        {
                            diccionary.Add(aux[w], i + 1);
                        }


                    }

                }
            }
        }
        else 
        {
            tutorialPanels[contTutorial].SetActive(false);
            contTutorial++;
            tutorialPanels[contTutorial].SetActive(true);
        }
    }
    void gameUpdate()
    { 
 
        Collider2D[] result = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        
        int i = result.Length;
        if (i > 0)
        {
            diccionary.Clear();
            textBx.SetActive(true);
        }
        while (i > 0)
        {
            i--;
            Debug.Log(result[i].name);

            int id;
            string[] aux = result[i].GetComponent<opalopa>().dameDic(out id);
            if (!respondidos.ContainsValue(id))
            {
                for (int w = 0; w < aux.Length; w++)
                {
                    diccionary.Add(aux[w], id);
                }


            }

        }

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(contTutorial == tutorialPanels.Length - 1)
            {
                tutorialPanels[contTutorial].SetActive(false);
                contTutorial++;
            }
            if (tutorial) tutorialUpdate();
            else gameUpdate();

        }
    }

    public void OnFieldEnter(string word)
    {

        if (diccionary.ContainsKey(word.ToLower()))
        {
            //puntos++;
            int value = -1;
            diccionary.TryGetValue(word.ToLower(), out value);
            diccionary.Remove(word.ToLower());

            List<string> lst = new List<string>();
            int aux = -1;
            foreach (var item in diccionary)
                if (diccionary.TryGetValue(item.Key, out aux) && aux == value) lst.Add(item.Key);

            foreach (string s in lst) diccionary.Remove(s);

            respondidos.Add(word, value);

            Debug.Log("Acertaste");
        }
        else
        {
            Debug.Log("Fallaste");
        }

        if (tutorial)
        {
            tutorialPanels[contTutorial].SetActive(false);
            contTutorial++;
            tutorialPanels[contTutorial].SetActive(true);
            tutorial = false;
        }

        diccionary.Clear();
        textBx.SetActive(false);
    }

}
