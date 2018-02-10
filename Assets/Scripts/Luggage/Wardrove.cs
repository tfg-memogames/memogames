using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAGE.Analytics;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Wardrove : MonoBehaviour {

    public Vector3 pos;
    public Vector3 scale;

    public GameObject inside;
    private Vector3 initialPos;
    private Vector3 initialScale;

    private LuggageManager gameManager;
    private SpriteRenderer render;
    private bool open;

    // Use this for initialization
    void Start () {
        initialPos = this.transform.localPosition;
        initialScale = this.transform.localScale;
        this.gameManager = GameObject.FindObjectOfType<LuggageManager>();
        render = gameObject.GetComponent<SpriteRenderer>();
        CloseDoor();
        EnableColliders(false);
    }

    private void OnMouseOver()
    {
        if (!this.gameManager.isInteractable || !Input.GetMouseButtonDown(0))
            return;

        if (open) CloseDoor();
        else OpenDoor();
    }

    private void EnableColliders(bool flag)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Collider2D collider = child.GetComponent<Collider2D>();
            if (collider) collider.enabled = flag;
        }
    }

    private void CloseDoor()
    {
        open = false;
        this.transform.localPosition = initialPos;
        this.transform.localScale = initialScale;
        inside.SetActive(false);
        // this.render.sprite = closed;
        ShowChildObjects(false);
        EnableColliders(false);
    }

    private void OpenDoor()
    {
        open = true;
        inside.SetActive(true);
        this.transform.localPosition = pos;
        this.transform.localScale = scale;
        //this.render.sprite = openned;
        ShowChildObjects(true);
        EnableColliders(true);

        //Tracker: what objects were shown
        WhatItemsWereShown();
    }

    private void WhatItemsWereShown()
    {
        string var = "";

        for (int i = 0; i < inside.transform.childCount; i++)
        {
            var += inside.transform.GetChild(i).gameObject.name + "-";
        }

        Tracker.T.setVar("ObjetosVistos", var);

        Tracker.T.trackedGameObject.Interacted("AbrioArmario");
    }

    private void ShowChildObjects(bool show)
    {
        for (int i = 0; i < inside.gameObject.transform.childCount; i++)
        {
            inside.gameObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = show;
        }
    }

    public bool isOpen
    {
        get { return this.open; }
    }
}
