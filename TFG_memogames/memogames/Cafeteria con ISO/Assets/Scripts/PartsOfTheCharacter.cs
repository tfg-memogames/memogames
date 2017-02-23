using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsOfTheCharacter : MonoBehaviour {

    private ConversationLauncher parent;

    // Use this for initialization
	void Start () {
        parent = GetComponentInParent<ConversationLauncher>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        parent.startDialog();
    }
}
