using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Selector : MonoBehaviour {

	//EventSystem.current.SetSelectedGameObject(null);
	public Button man;
	public Button woman;
	public Button easy;
	public Button medium;
	public Button hard;

	private int level;//1 easy 2 medium 3 hard
	private int gender;//0 man, 1 woman

	void Start()
	{
		woman.onClick.AddListener(WomanOnClick);
		man.onClick.AddListener(ManOnClick);

		easy.onClick.AddListener(EasyOnClick);
		medium.onClick.AddListener (MediumOnClick);
		hard.onClick.AddListener(HardOnClick);

	}

	void ManOnClick()
	{
		if (gender == 1) {
			ChangeColor (woman, false);
		}

		ChangeColor (man, true);

		gender = 0;
	}

	void WomanOnClick()
	{
		if (gender == 0) {
			ChangeColor (man, false);
		}
		ChangeColor (woman, true);

		gender = 1;
	}

	private void ChangeColor(Button b, bool blue){
		Color c = Color.white;

		if(blue)
			c= new Color (0.1F, 0.2F, 0.3F, 1);

		b.GetComponent<Image> ().color = c;
		Text t=b.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();

		t.color=c;
	}


	void EasyOnClick()
	{
		if (level != 1) {
			ChangeColor (medium, false);
			ChangeColor (hard, false);
		}

		ChangeColor (easy, true);

		level = 1;
	}

	void MediumOnClick()
	{
		if (level != 2) {
			ChangeColor (easy, false);
			ChangeColor (hard, false);
		}

		ChangeColor (medium, true);

		level=2;
	}

	void HardOnClick()
	{
		if (level != 3) {
			ChangeColor (easy, false);
			ChangeColor (medium, false);
		}

		ChangeColor (hard, true);

		level = 3;
	}


}
