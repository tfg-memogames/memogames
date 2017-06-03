using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPanel : MonoBehaviour {

    //first you need the RectTransform component of your canvas
    private RectTransform canvasRect;

    private static GameObject p;

    void Awake()
    {
        canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }

    //http://answers.unity3d.com/questions/799616/unity-46-beta-19-how-to-convert-from-world-space-t.html
    public void instantiatePanel(GameObject panel, GameObject target)
    {
        p = Instantiate(panel);
        p.transform.SetParent(canvasRect);

        //this is the ui element
        RectTransform panelTransform = p.GetComponent<RectTransform>();

        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(target.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

        WorldObject_ScreenPosition.y += (panelTransform.sizeDelta.y / 2);

        //now you can set the position of the ui element
        panelTransform.anchoredPosition = WorldObject_ScreenPosition;
        panelTransform.localScale = Vector3.one;
    }

    public GameObject instantiatePanel(GameObject panel)
    {
        p = Instantiate(panel);
        p.transform.SetParent(canvasRect);
        p.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        p.transform.localScale = Vector3.one;
        return p;
    }

    //Elimina el ultimo panel instanciado
    public void DestroyPanel()
    {
        Destroy(p);
    }
}
