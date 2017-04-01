using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Counter : MonoBehaviour {

    public Text timeToGo;
    private float time;

    private Image image;
    private float restTime;

    void Awake()
    {
        time = GameObject.FindGameObjectWithTag("GameController").GetComponent<RecipeManager>().time;
    }

	// Use this for initialization
	void Start () {
        transform.SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        image = GetComponent<Image>();
        timeToGo.text = "" + (int)time;
        restTime = time;
	}
	
	// Update is called once per frame
	void Update () {
        restTime -= Time.deltaTime;
        image.fillAmount = restTime / time;
        timeToGo.text = "" + (int)restTime;
        if (restTime <= 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<RecipeManager>().GameOver();
            Destroy(gameObject);
        }
    }
}
