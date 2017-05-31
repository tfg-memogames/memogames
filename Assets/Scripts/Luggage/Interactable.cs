using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour {

    public GameObject panel;
    private LuggageManager gameManager;

    private void Start()
    {
        this.gameManager = GameObject.FindObjectOfType<LuggageManager>();
        this.panel.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (this.gameManager.isInteractable)
            this.gameManager.ClickOnDrawer(this.panel);
    }
}
