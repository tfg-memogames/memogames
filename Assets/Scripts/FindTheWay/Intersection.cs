using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : MonoBehaviour {

    private List<GameObject> arrows;

	// Use this for initialization
	void Awake () {
        arrows = new List<GameObject>();
	}

	public void ShowArrows(GameObject touch)
    {
        foreach(GameObject go in arrows)
        {
            if (go.transform.parent.gameObject != touch)
                go.SetActive(true);
        }
    }

    public void HideArrows()
    {
        foreach(GameObject go in arrows)
        {
            go.SetActive(false);
        }
    }

    public void AddArrow(GameObject go)
    {
        arrows.Add(go);
    }

    public void ChangeTag(string newTag, CarMove.Direction dir)
    {
        switch(dir)
        {
            case CarMove.Direction.NW:
                //Busco a mi hijo SE
                transform.FindChild("SE").gameObject.tag = newTag;
                break;
            case CarMove.Direction.SE:
                //Busco a mi hijo NW
                transform.FindChild("NW").gameObject.tag = newTag;
                break;
            case CarMove.Direction.NE:
                //Busco a mi hijo SW
                transform.FindChild("SW").gameObject.tag = newTag;
                break;
            case CarMove.Direction.SW:
                //Busco a mi hijo NE
                transform.FindChild("NE").gameObject.tag = newTag;
                break;
        }
    }
}
