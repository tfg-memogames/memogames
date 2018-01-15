using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class SlideScene : MonoBehaviour {

    h4g2.GameState.SceneDef def;
    GameObject canvas;

	// Use this for initialization
	void Start () {

       /* var b = Camera.main.GetComponent<Blackout>();
        var bl = Camera.main.GetComponent<Blur>();
        b.enabled = false;
        b.rampOffset = 0;
        bl.enabled = false;
        bl.Iterations = 0;
        bl.BlurSpread = 0;*/

        this.def = h4g2.GameState.S.nextScene ();

        this.canvas = GameObject.Find ("Canvas");

        GameObject.Find ("title").GetComponent<Text> ().text = def.title;
        GameObject.Find ("subtitle").GetComponent<Text> ().text = def.subtitle;

        fade_in ();
	}
	
    private float msg_time = 1.5f;
    private float time_since_last_msg = 0;
    int times = 0;
    void Update () {
        time_since_last_msg += Time.deltaTime;
        if (time_since_last_msg > msg_time) {
            time_since_last_msg = 0;
            if (times==1) {
                fade_out ();
            } else if (times == 2) {
                loadnext ();
            }
            times++;
        }
    }

    public void loadnext(){
        SceneManager.LoadScene (def.next_scene);
    }

    public void fade_in(){
        Graphic[] graphics = canvas.GetComponentsInChildren<Graphic>();

        for (int i = 0; i < graphics.Length; ++i)
        {
            graphics[i].CrossFadeAlpha(0f, 0f, false);
            graphics[i].CrossFadeAlpha(1f, 1.5f, false);
        }
    }

    public void fade_out(){
        Graphic[] graphics = canvas.GetComponentsInChildren<Graphic>();

        for (int i = 0; i < graphics.Length; ++i)
        {
            graphics[i].CrossFadeAlpha(0f, 1.5f, false);
        }
    }
}
