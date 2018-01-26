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
    public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
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
        if (this.gameManager.isInteractable && (wardroveDoor == null || !wardroveDoor.isOpen)) { 
        SetColor(this.hoverColor);
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }

    private void OnMouseExit()
    {
        SetColor(Color.white);
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    private void SetColor(Color c)
    {
        this.render.color = c;
    }
}
