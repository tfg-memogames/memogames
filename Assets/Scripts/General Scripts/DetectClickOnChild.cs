using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DetectClickOnChild : MonoBehaviour {

	public GameObject parent;

	// Use this for initialization
	void OnMouseDown () {
		parent.SendMessage ("childClicked", this.gameObject);	
	}
}

