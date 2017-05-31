using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class HoverEffect : MonoBehaviour {

    private Color hoverColor;
    private SpriteRenderer render;
    private Wardrove wardroveDoor;
    private LuggageManager gameManager;

    // Use this for initialization
    void Start()
    {
        this.gameManager = GameObject.FindObjectOfType<LuggageManager>();
        render = this.gameObject.GetComponent<SpriteRenderer>();
        hoverColor = Color.cyan;
        this.wardroveDoor = this.gameObject.GetComponent<Wardrove>();
    }

    private void OnMouseEnter()
    {
        if (this.gameManager.isInteractable && (wardroveDoor == null || !wardroveDoor.isOpen))
            SetColor(this.hoverColor);
    }

    private void OnMouseExit()
    {
        SetColor(Color.white);
    }

    private void SetColor(Color c)
    {
        this.render.color = c;
    }
}
