using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class Chat : MonoBehaviour {

    static public Chat S;

    public GameObject receiveBubble;
    public GameObject sendBubble;
    public GameObject amistad_bubble;
    public GameObject borrar_bubble;
    public GameObject alert_bubble;

    private GameObject content;

    private GameObject textchat;

    private List<GameObject> bubbles;

    public Dictionary<string,Color> colors;

    public List<Sprite> characters;
    public Sprite transparente;

	// Use this for initialization
	void Start () {
        S = this;

        this.bubbles = new List<GameObject> ();

        this.colors = new Dictionary<string, Color> ();

        Color ventu = Color.red;
        ColorUtility.TryParseHtmlString ("#FFDDDDFF", out ventu);
        this.colors.Add ("Ventu", ventu);

        Color alicia = Color.green;
        ColorUtility.TryParseHtmlString ("#F0FFDDFF", out alicia);
        this.colors.Add ("Alicia", alicia);

        Color ines = Color.magenta;
        ColorUtility.TryParseHtmlString ("#FFDDF0FF", out  ines);
        this.colors.Add ("Inés", ines);

        Color armando = Color.yellow;
        ColorUtility.TryParseHtmlString ("#DDF1FFFF", out armando);
        this.colors.Add ("Armando", armando);

        Color valentin = Color.cyan;
        ColorUtility.TryParseHtmlString ("#C3FFFAFF", out valentin);
        this.colors.Add ("Valentín", valentin);

        Color teresa = Color.blue;
        ColorUtility.TryParseHtmlString ("#E2FFC3FF", out teresa);
        this.colors.Add ("Teresa", teresa);

        Color raul = Color.magenta;
        ColorUtility.TryParseHtmlString ("#FFF0C3FF", out raul);
        this.colors.Add ("Raúl", raul);

        Color miguel = Color.green;
        ColorUtility.TryParseHtmlString ("#FFE2C3FF", out miguel);
        this.colors.Add ("Miguel", miguel);

        Color nerea = Color.blue;
        ColorUtility.TryParseHtmlString ("#F4C3FFFF", out nerea);
        this.colors.Add ("Nerea", nerea);

        this.content = GameObject.Find ("content");

        textchat = GameObject.Find ("textochat");
        fade_out_instant(this.content);
	}
	
	// Update is called once per frame
    private float msg_time = 0.025f;
    private float time_since_last_msg = 0;

    string text_to_show = "";
    string current_text = "";
    bool a = false;
	void Update () {
       time_since_last_msg += Time.deltaTime;
        if (time_since_last_msg > msg_time) {
            time_since_last_msg = 0;
            if (current_text.Length < text_to_show.Length) {
                addCharacter ();
            }
        }
	}

    void addCharacter(){
        this.current_text = text_to_show.Substring (0, current_text.Length+1);
        GameObject.Find ("texto_textochat").GetComponent<Text> ().text = current_text;

    }

    public void sendMessage(string text){
        GameObject bubble = GameObject.Instantiate (sendBubble);
        bubble.transform.SetParent (content.transform);


        bubble.transform.localScale = new Vector3 (1f, 1f, 1f);

        bubble.GetComponent<Bubble> ().text = text;
        bubble.GetComponent<Bubble> ().resize ();

        bubble.transform.localPosition = new Vector3 (400 - (bubble.GetComponent<RectTransform> ().rect.width/2), 0, 0);

        this.bubbles.Add (bubble);

        this.pushBubbles (75f);
    }

    public void receiveMessage(string text, string talker){
        GameObject bubble = GameObject.Instantiate (receiveBubble);
        bubble.transform.SetParent (content.transform);


        bubble.transform.localScale = new Vector3 (1f, 1f, 1f);

        bubble.GetComponent<Bubble> ().text = text;
        bubble.GetComponent<Bubble> ().resize ();
        if(!text.Contains("\n"))
            bubble.GetComponent<Bubble> ().talker = talker;

        if (colors.ContainsKey (talker))
            bubble.GetComponentInChildren<Image> ().color = colors [talker];

        bubble.transform.localPosition = new Vector3 (200  + (bubble.GetComponent<RectTransform> ().rect.width/2), 0, 0);

        this.bubbles.Add (bubble);

        this.pushBubbles (75f);
    }

    public void mensajeAmistad(string text){
        GameObject bubble = GameObject.Instantiate (amistad_bubble);
        bubble.transform.SetParent (content.transform);


        bubble.transform.localScale = new Vector3 (1f, 1f, 1f);

        bubble.transform.localPosition = new Vector3 (300  + (bubble.GetComponent<RectTransform> ().rect.width/2), 0, 0);
        bubble.GetComponent<Bubble> ().text = text + " quiere ser tu amig@";

        this.bubbles.Add (bubble);

        this.pushBubbles (75f);
    }

    public void mensajeBorrar(){
        GameObject bubble = GameObject.Instantiate (borrar_bubble);
        bubble.transform.SetParent (content.transform);


        bubble.transform.localScale = new Vector3 (1f, 1f, 1f);

        bubble.transform.localPosition = new Vector3 (300  + (bubble.GetComponent<RectTransform> ().rect.width/2), 0, 0);
        bubble.GetComponent<Bubble> ().text = "¿Deseas borrar la foto?";

        this.bubbles.Add (bubble);

        this.pushBubbles (75f);
    }

    public void mensajeAlerta(string text){
        GameObject bubble = GameObject.Instantiate (alert_bubble);
        bubble.transform.SetParent (content.transform);

        bubble.transform.localScale = new Vector3 (1f, 1f, 1f);

        bubble.transform.localPosition = new Vector3 (300  + (bubble.GetComponent<RectTransform> ().rect.width/2), 0, 0);
        bubble.GetComponent<Bubble> ().text = text;

        this.bubbles.Add (bubble);

        this.pushBubbles (75f);
    }

    private int[] talkers = null;
    public void talk(int[] chars, string text){
        if (chars == null) {
            this.talkers = null;
            fade_out (this.content);
        }{
            if (this.talkers != chars) {
                this.talkers = chars;
                for(int i = 0; i<3; i++){
                    if(talkers[i] != -1)
                        //GameObject.Find("char_" + i).GetComponent<Image> ().sprite = this.transparente;
                    //else
                        GameObject.Find("char_" + i).GetComponent<Image> ().sprite = this.characters [talkers[i]]; 

                }

                fade_in (this.content);
            }

            this.text_to_show = text;
            this.current_text = "";
        }
    }

    public void pushBubbles(float height){
        foreach (GameObject go in bubbles) {
            go.GetComponent<Bubble>().moveTo(new Vector3 (go.transform.localPosition.x, go.transform.localPosition.y + height, go.transform.localPosition.z));
        }
    }

    public void fade_in(GameObject go){
        Graphic[] graphics = go.GetComponentsInChildren<Graphic>();

        for (int i = 0; i < graphics.Length; ++i)
        {
            if(graphics[i].GetComponent<Text>() != null || graphics[i].GetComponent<Image>() != null)
                if (i == 0 || i > 3)
                    graphics [i].CrossFadeAlpha (1f, 0.5f, false);
                 else if (this.talkers [i - 1] != -1) {
                    graphics [i].CrossFadeAlpha (1f, 0.5f, false);
                }else
                    graphics[i].CrossFadeAlpha(0f, 0.5f, false);
                
        }
    }

    public void fade_out(GameObject go){
        Graphic[] graphics = go.GetComponentsInChildren<Graphic>();

        for (int i = 0; i < graphics.Length; ++i)
        {
            graphics[i].CrossFadeAlpha(0f, 0.5f, false);
        }
    }

    public void fade_out_instant(GameObject go){
        Graphic[] graphics = go.GetComponentsInChildren<Graphic>();

        for (int i = 0; i < graphics.Length; ++i)
        {
            graphics[i].CrossFadeAlpha(0f, 0f, false);
        }
    }
}
