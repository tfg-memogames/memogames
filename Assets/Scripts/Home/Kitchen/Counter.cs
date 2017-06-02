using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Counter : MonoBehaviour {

    public Text timeToGo;

    private Image image;
    private GameObject gameManager;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
    }

	// Use this for initialization
	void Start () {
        transform.SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        image = GetComponent<Image>();
	}

    // Time since the game started and maxTime
    public void PaintTheTime(float time, float maxTime)
    {
        float timeToFinish = maxTime - time;
        image.fillAmount = timeToFinish / maxTime;
        timeToGo.text = "" + (int)timeToFinish;
        if (time >= maxTime)
        {
            gameManager.SendMessage("GameOver");
            Destroy(gameObject);
        }
    }
}
