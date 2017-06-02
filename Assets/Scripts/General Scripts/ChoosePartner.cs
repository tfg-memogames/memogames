using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePartner : MonoBehaviour {

    private const string wifePath = "Sprites/Characters/Wife/completedWife";
    private const string husbandPath = "Sprites/Characters/Husband/completedHusband";

    private GameState _gs;
    private GameObject _partner;
    private Sprite _sprite;

    // Use this for initialization


    //No se puede instanciar un prefab porque en cada escena necesitará unos scripts distintos
    void Start () {
		this._gs = GameObject.FindObjectOfType<GameState> ();
		this._partner = GameObject.Find ("Partner");


			if (_gs.playerGender == GameState.Gender.M)
				this._sprite = Resources.Load<Sprite> (wifePath);
			else
				this._sprite = Resources.Load<Sprite> (husbandPath);

			SpriteRenderer sp = this._partner.GetComponent<SpriteRenderer> ();
       
			sp.sprite = this._sprite;
			//Instantiate(this._partner);

	}
	// Update is called once per frame
	void Update () {
		
	}
}
