using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Image))]
public class shadowEffect : MonoBehaviour {

    public float timeEffect;
    public Vector3 finalPosition;

    private Image image;

    private Color startColor;
    private Vector3 startPosition;

    private Vector3 posTransition;
    private float timeToFinish;

    void Start()
    {
        image = GetComponent<Image>();
        startColor = image.color;
        startPosition = transform.localPosition;
        posTransition = finalPosition - startPosition;
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

        transform.localPosition = new Vector3(startPosition.x + posTransition.x * percOfCurrTime,
                                         startPosition.y + posTransition.y * percOfCurrTime,
                                         startPosition.z + posTransition.z * percOfCurrTime);

        image.color = new Color(startColor.r,
                                startColor.g,
                                startColor.b,
                                startColor.a * timeToFinish / timeEffect);
    }

}
