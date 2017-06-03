using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAGE.Analytics;

//The luggage panel has this script, because this controls what clothes
//can be put into.
public class ClothesController : MonoBehaviour {

    // Non canvas object representation of luggage
    public GameObject dropObject;

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
            this.representation[i].SetActive(false);
        }

        InitializeTargets();
    }

    // Getting the targets that are inside the wardrove (not canvas renderer)
    // and adding BoxCollider2D and script DragObject
    private void InitializeTargets()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Item"))
        {
            if (go.GetComponent<CanvasRenderer>() == null)
            {
                go.GetComponent<DragObject>().destiny = new GameObject[1];
                go.GetComponent<DragObject>().destiny.SetValue(dropObject, 0);
            }
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

            //Tracker: object name, time
            SendTrackerDragInfo(true, go.name);

            this.gameManager.TargetCompleted();
            
            return true;
        }

        this.gameManager.ShowFeedback(false);

        //Tracker: Error user drag a wrong item
        SendTrackerDragInfo(false, go.name);

        return false;
    }

    private void SendTrackerDragInfo(bool right, string name)
    {
        Tracker.T.setVar("Tiempo", this.gameManager.ActualTime);
        Tracker.T.setVar("Item", name);

        string id = right ? "AccionCorrecta" : "AccionErronea";

        Tracker.T.trackedGameObject.Interacted(id);
    }
}
