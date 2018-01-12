using UnityEngine;
using System.Collections;
namespace ImportantManager{
public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Shader GrayScaleShader = null;
	private bool _renderFullColor = true;

	public void ChangeToGrayScaleRender () {
		Debug.Log ("CameraController:ChangeToGrayScaleRender");
		var c = GetComponent<Camera> ();
		c.SetReplacementShader (GrayScaleShader, null);
		_renderFullColor = false;
	}

	public void ChangeToFullColorRender () {
		Debug.Log ("CameraController:ChangeToFullColorRender");
		var c = GetComponent<Camera> ();
		c.SetReplacementShader (null, null);
		_renderFullColor = true;
	}

	public void FlipColorRender () {
		/*if (_renderFullColor) {
			ChangeToGrayScaleRender ();
		} else {
			ChangeToFullColorRender ();
		}*/
	}
}
}