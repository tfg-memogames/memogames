using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
///<summary>
///It ensures that objects go to their places
///Se encarga de que los objetos vayan a sus lugares
///</summary>
public class DropObject : MonoBehaviour {

	public void ItemWasDropped(GameObject go)
    {
        Debug.Log(go.name + " drag on " + name);

        FindObjectOfType<RecipeManager>().ItemWasDropped(go, gameObject);
    }
}
