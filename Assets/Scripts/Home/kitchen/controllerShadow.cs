using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class controllerShadow : MonoBehaviour {

    public GameObject error;

    // Use this for initialization
    void Start()
    {
        GameObject instance = Instantiate(error);
        instance.transform.SetParent(transform);
        instance.transform.localPosition = new Vector3(0, 0, 0);
    }
}
