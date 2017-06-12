using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonColor : MonoBehaviour {
	
	public void reset(){
		EventSystem.current.SetSelectedGameObject(null);
	}
}
