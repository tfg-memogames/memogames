using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Image))]
public class ShadowEffect : MonoBehaviour {

    public float timeEffect;
    public Vector3 finalPosition;

    private Image image;
    private Color startColor;
    private Vector3 startPosition;

    private Vector3 posTransition;
    private float timeToFinish;

    void Start()
    {
		print ("anim");
        image = GetComponent<Image>();
        startColor = image.color;
        startPosition = transform.localPosition;
        timeToFinish = timeEffect;
        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(timeEffect);
        Destroy(gameObject);
    }

    void Update()
    {
        timeToFinish -= Time.deltaTime;
        float percOfCurrTime = (timeEffect - timeToFinish) / timeEffect;

        transform.localPosition = new Vector3(startPosition.x + finalPosition.x * percOfCurrTime,
                                         startPosition.y + finalPosition.y * percOfCurrTime,
                                         startPosition.z + finalPosition.z * percOfCurrTime);

        image.color = new Color(startColor.r,
                                startColor.g,
                                startColor.b,
                                startColor.a * timeToFinish / timeEffect);
    }

}
