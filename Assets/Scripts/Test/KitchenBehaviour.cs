using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class KitchenBehaviour : MonoBehaviour {

    public GameObject panel;

    //this is your object that you want to have the UI element hovering over
    public GameObject husband;

    //this is the ui element
    private RectTransform panelTransform;

    //first you need the RectTransform component of your canvas
    private RectTransform canvasRect;

    private static string path = "Prefabs/ButtonsForKitchen";
    private static string[] but = { "Button", "Button (1)", "Button (2)" };

    void Start()
    {
        canvasRect = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        panelTransform = panel.GetComponent<RectTransform>();
        instantiatePanel();
        paintOptions();
    }

    public void paintOptions()
    {
        foreach (string s in but)
        {
            string completePath = path + "/" + s;
            GameObject button = Resources.Load(completePath) as GameObject;
            GameObject b = Instantiate(button);
            b.transform.SetParent(panel.transform, false);
        }        
    }

	public void Click1()
    {
        Debug.Log("Click 1");
    }

    public void Click2()
    {
        Debug.Log("Click 2");
    }

    public void Click3()
    {
        Debug.Log("Click 3");
    }


    //http://answers.unity3d.com/questions/799616/unity-46-beta-19-how-to-convert-from-world-space-t.html
    void instantiatePanel()
    {

        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(husband.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

        WorldObject_ScreenPosition.y += (panelTransform.sizeDelta.y / 2);

        //now you can set the position of the ui element
        panelTransform.anchoredPosition = WorldObject_ScreenPosition;
    }
}
