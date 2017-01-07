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

	#endregion

	#region attributes

	private List<Step> _recipe;

	private Object[] _ingredients;

	private Object[] _utensils;

	private Object[] _tools;


	#endregion

	// Use this for initialization
	void Start () {
		_recipe=new List<Step>();

		_ingredients=Resources.LoadAll("Ingredients");
		_utensils=Resources.LoadAll("Utensils");
		_tools=Resources.LoadAll("Tools");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
