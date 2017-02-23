using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour {

	private bool isClose = true;
	private GameObject water;

	void Start() {
		water = GameObject.FindGameObjectWithTag ("Water");
	}


	public void OpenCloseTap() {
		if (isClose) {
			isClose = false;
			water.SetActive (true);

		} else {
			isClose = true;
			water.SetActive (false);

		}
	}
}
