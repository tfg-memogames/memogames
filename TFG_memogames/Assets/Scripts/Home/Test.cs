using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	private const string wifePath = "Prefabs/wife";
	private const string husbandPath = "Prefabs/husband";


	private GameState _gs;
	private GameObject _couple;

	// Use this for initialization
	void Start () {

		this._gs = GameObject.FindObjectOfType<GameState> ();

		if (_gs.playerGender == GameState.Gender.M)
			this._couple = Resources.Load <GameObject> (wifePath);
		else
			this._couple = Resources.Load <GameObject> (husbandPath);

		Instantiate (_couple);
	}
}
