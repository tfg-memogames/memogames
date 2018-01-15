using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ImportantManager{
public class Rotator : MonoBehaviour {

	int currentangle = 0;

	RectTransform r;
	// Use this for initialization
	void Start () {
		r = this.GetComponent<RectTransform> ();

	}
	
	private float time = 0.25f;
	private float time_since_last = 0;
	int times = 0;
	void Update () {
		time_since_last += Time.deltaTime;
		if (time_since_last > time) {
			time_since_last = 0;
			rotate ();
			times++;
		}
	}

	void rotate () {
		currentangle-=20;
		r.eulerAngles = new Vector3 (0, 0, currentangle);
	}
}
}