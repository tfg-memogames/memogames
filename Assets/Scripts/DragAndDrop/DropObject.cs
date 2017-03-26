using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour {

	public void ItemWasDropped(GameObject go)
    {
        gameObject.SetActive(false);
    }
}
