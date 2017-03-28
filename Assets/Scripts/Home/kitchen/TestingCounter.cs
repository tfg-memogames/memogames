using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TestingCounter : MonoBehaviour {

    public Text timeToGo;
    public float time;

    private Image image;
    private float restTime;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        timeToGo.text = "" + (int)time;
        restTime = time;
	}
	
	// Update is called once per frame
	void Update () {
        restTime -= Time.deltaTime;
        image.fillAmount = restTime / time;
        timeToGo.text = "" + (int)restTime;
        if (restTime <= 0) Debug.Log("GameOver");
    }
}
