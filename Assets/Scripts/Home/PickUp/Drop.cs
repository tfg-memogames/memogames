using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Drop: MonoBehaviour {

	public void ItemWasDropped(GameObject go)
    {
        Debug.Log(go.name + " drag on " + name);

        FindObjectOfType<RecipeManager>().ItemWasDropped(go, gameObject);
    }
}
