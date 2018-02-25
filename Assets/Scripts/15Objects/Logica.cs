using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Logica : MonoBehaviour {

    // Use this for initialization
   private  SortedDictionary<string, int> diccionary;

    
    [System.Serializable]
    public struct Object
    {
       public string word;

        public string[] sinonimos;
        
    }

    public GameObject sprite;
    public Text texto;
    public Text puntuacion;
    public GameObject finish;

    public Object[] A;
    public Object[] B;

    private int puntos, intentos;

    void Start () {
        puntos = 0;
        diccionary = new SortedDictionary<string, int>();

        Random rnd = new Random();
        string type = "A";
        if (Random.Range(0, 100) < 50) type = "B"; 

        sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/15Objects/"+type);
        Object[] aux;
        if (type == "A") aux = A;
        else aux = B;

        int cont = 0;
        foreach (Object ob in aux)
        {
            diccionary.Add( ob.word,cont);
            foreach (string s in ob.sinonimos)
            {
                Debug.Log(s);
                diccionary.Add(s, cont);
            }
            cont++;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

   public void OnFieldEnter(string word)
    {
        intentos++;
        if (intentos < 15) { 
            if (diccionary.ContainsKey(word.ToLower()))
            {
                puntos++;
                int value = -1;
                diccionary.TryGetValue(word.ToLower(), out value);
                diccionary.Remove(word.ToLower());

                List<string> lst = new List<string>();
                int aux = -1;
                foreach (var item in diccionary)
                    if (diccionary.TryGetValue(item.Key, out aux) && aux == value) lst.Add(item.Key);

                foreach (string s in lst) diccionary.Remove(s);
            }

            
            texto.text = "Has contestado "+ intentos+ " objetos\n\nTe quedan "+ (15-intentos);
            Debug.Log(puntos);
        }
        else
        {
            finish.SetActive(true);
            puntuacion.text = puntos + "/15";
            
        }
    }

}
