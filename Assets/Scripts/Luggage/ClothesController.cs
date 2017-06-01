using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The luggage panel has this script, because this controls what clothes
//can be put into.
public class ClothesController : MonoBehaviour {

    //Each GameObject target, has an image that represents it inside the luggage
    public GameObject[] targets;
    private GameObject[] representation;
    private LuggageManager gameManager;

    // Use this for initialization
    void Start()
    {
        this.gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<LuggageManager>();

        this.representation = new GameObject[this.targets.Length];

        // Getting the childs and hiding them
        for (int i = 0; i < this.representation.Length; i++)
        {
            this.representation[i] = this.transform.GetChild(i).gameObject;
            Debug.Log(representation[i].name);
            this.representation[i].SetActive(false);
        }
    }

    // Check if the target was put into the luggage and updates gameManager
    public bool TargetWasDropped(GameObject go)
    {
        int actualTarget = this.gameManager.actualTargetNum;
        // Call gameManager
        if (targets[actualTarget].Equals(go))
        {
            this.representation[actualTarget].SetActive(true);
            this.gameManager.TargetCompleted();
            return true;
        }
        return false;
    }
}
