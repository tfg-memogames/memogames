using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonColor : MonoBehaviour {
    private CursorMode cursorMode = CursorMode.Auto;
    public void reset(){
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        EventSystem.current.SetSelectedGameObject(null);
	}
}
