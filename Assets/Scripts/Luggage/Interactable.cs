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

    private void OnMouseOver()
    {
        if (this.gameManager.isInteractable && Input.GetMouseButtonDown(1))
            this.gameManager.ClickOnDrawer(this.panel);
    }
}
