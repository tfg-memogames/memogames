using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	#region declaration

	[System.Serializable]
	public class Step {
		public Sprite _icon;
		public string _sentence;
	}

	[System.Serializable]
	public class Ingredient {
		public Sprite _icon;
		public string _name;

	}
	[System.Serializable]
	public class Utensil {
		public Sprite _icon;
		public string _name;
		public List<Ingredient> _ingredients;
	}

	[System.Serializable]
	public class Tool {
		public Sprite _icon;
		public string _name;
		public List<Utensil> _utensils;
	}

	#endregion

	#region attributes

	private List<Step> _recipe;


	#endregion

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
