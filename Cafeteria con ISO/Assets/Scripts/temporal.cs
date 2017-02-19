using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporal : MonoBehaviour {

    private PhoneBehaviour parent;

    // Use this for initialization
    void Start()
    {
        parent = GetComponentInParent<PhoneBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        parent.startDialog();
    }
}
