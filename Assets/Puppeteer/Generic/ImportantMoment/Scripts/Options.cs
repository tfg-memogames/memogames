using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ImportantManager{
	public class Options : MonoBehaviour {

		public Text o1, o2, o3, o4;

		// Use this for initialization
		void Start () {
			/*o1 = GameObject.Find ("O1").GetComponent<Text>();
			o2 = GameObject.Find ("O2").GetComponent<Text>();
			o3 = GameObject.Find ("O3").GetComponent<Text>();
			o4 = GameObject.Find ("O4").GetComponent<Text>();*/
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void setText(string o1, string o2, string o3, string o4){
			this.o1.text = o1;
			this.o2.text = o2;
			this.o3.text = o3;
			this.o4.text = o4;
		}

		public void fade_in(){
			Graphic[] graphics = gameObject.GetComponentsInChildren<Graphic>();

			for (int i = 0; i < graphics.Length; ++i)
			{
				if(graphics[i].GetComponent<Text>() != null || graphics[i].GetComponent<Image>() != null)
					graphics [i].CrossFadeAlpha (1f, 0.5f, false);
			}
		}

		public void fade_out(){
			Graphic[] graphics = gameObject.GetComponentsInChildren<Graphic>();

			for (int i = 0; i < graphics.Length; ++i)
			{
				graphics[i].CrossFadeAlpha(0f, 0.5f, false);
			}
		}

		public void fade_out_instant(){
			Graphic[] graphics = gameObject.GetComponentsInChildren<Graphic>();

			for (int i = 0; i < graphics.Length; ++i)
			{
				graphics[i].CrossFadeAlpha(0f, 0f, false);
			}
		}
	}
}