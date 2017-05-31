using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuggageManager : MonoBehaviour {

    // Clothes that the user has to move into the luggage
    public Sprite[] targets;
    // This images shows the target
    public GameObject popUpPanel;
    // Time to complete the level
    public float time = 60.0f;

    public GameObject luggagePanel;
    public GameObject buttonCloseDrawer;
    public GameObject floorPanel;

    // Image inside the popUpPanel which shows the actual target
    private Image targetImage;
    private int actualTarget;
    private GameObject panelShow;
    private bool interactable;

	// Use this for initialization
	void Start () {
        this.actualTarget = 0;
        this.panelShow = null;
        this.buttonCloseDrawer.SetActive(false);
        this.luggagePanel.SetActive(false);
        this.floorPanel.SetActive(false);
        this.targetImage = this.popUpPanel.transform.GetChild(0).GetComponent<Image>();
        ShowNextTarget();
        this.actualTarget = 0;
    }

    private void ShowNextTarget()
    {
        this.actualTarget++;
        this.popUpPanel.SetActive(true);
        this.targetImage.sprite = this.targets[this.actualTarget];
        this.targetImage.type = Image.Type.Simple;
        this.targetImage.preserveAspect = true;
        this.interactable = false;
    }
	
    public void ClickOnDrawer(GameObject go)
    {
        go.SetActive(true);
        this.panelShow = go;
        this.luggagePanel.SetActive(true);
        this.buttonCloseDrawer.SetActive(true);
        this.floorPanel.SetActive(true);
        this.interactable = false;
    }

    public void CloseDrawer()
    {
        this.panelShow.SetActive(false);
        this.panelShow = null;
        this.buttonCloseDrawer.SetActive(false);
        this.luggagePanel.SetActive(false);
        this.floorPanel.SetActive(false);
        this.interactable = true;
    }

    public void ClosePopUp()
    {
        this.popUpPanel.SetActive(false);
        this.interactable = true;
    }

    public bool isInteractable
    {
        get { return this.interactable; }

    }
}
