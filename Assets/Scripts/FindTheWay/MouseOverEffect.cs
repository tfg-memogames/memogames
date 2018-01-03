using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverEffect : MonoBehaviour {
    public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    public void PointerEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    public void PointerExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
