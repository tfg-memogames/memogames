using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using IsoUnity.Sequences;

public class CustomDialogManager : DialogEventManager {

    public enum State
    {
        Opening, Showing, Closing, Idle, Options
    }

    // Public variables
    public float timePerCharacter;
    public float fadeTime;
    public Text nameText;
    public Text textHolder;
    public Text optionsMessage;
    public GameObject interactionBlocker;
    public CanvasGroup dialogGroup;
    public CanvasGroup optionsGroup;
    public LayoutGroup optionsHolder;
    public GameObject optionPrefab;
    public Scrollbar scroll;
    public float scrollSpeed = 10f;
    public float scrollTime = .2f;
    public CharacterCuller faceCamera;
    public RawImage face;

    private CharacterSpriteManager currentCharacter;

    // Private variables
    private State state = State.Idle;
    private Fragment frg;
    private List<Option> opt;
    private List<GameObject> instancedOptions = new List<GameObject>();
    private int charactersShown;
    private float accumulated;
    private string msg = "";
    private CanvasGroup managingGroup;
    private Option optionSelected;
    private float scrollWantsToBe;

    public RectTransform viewPort;
    private float startingViewportX;

    // Pensamiento
    public Image fondoTexto;
    public Sprite texturaNormal;
    public Sprite texturaPensamiento;
    public Color colorTextoNormal;
    public Color colorTextoPensamiento;


    void Start()
    {
        state = State.Idle;

        startingViewportX = viewPort.anchoredPosition.x;
    }

    // ------------------------------
    // Abstract class implementation
    // ------------------------------

    protected override void DoFragment(Fragment fragment)
    {
        frg = fragment;
        msg = frg.Msg;
        nameText.text = frg.Name;
        charactersShown = 0;
        UpdateText();
        scroll.value = 1;
        state = State.Opening;
        managingGroup = dialogGroup;
        managingGroup.gameObject.SetActive(true);
        interactionBlocker.SetActive(true);

        if(frg.Msg == "")
        {
            state = State.Closing;
        }

        if (frg.Character != "")
        {
            faceCamera.character = GameObject.Find(frg.Character);

            if(faceCamera.character)
            {
                currentCharacter = faceCamera.character.GetComponent<CharacterSpriteManager>();
                if (frg.Parameter != "")
                {
                    currentCharacter.ChangeSprite(frg.Parameter);
                }

                face.uvRect = faceCamera.character.transform.localScale.x >= 0 ? new Rect(1, 0, -1, 1) : new Rect(0, 0, 1, 1);
            }
            else
            {
                Debug.LogWarning("Personaje no encontrado: " + frg.Character);
            }
        }

        fondoTexto.sprite = frg.Parameter == "narrativo" ? texturaPensamiento : texturaNormal;
        textHolder.color = frg.Parameter == "narrativo" ? colorTextoPensamiento : colorTextoNormal;

        var size = face.rectTransform.sizeDelta;
        size.x = faceCamera.character ? size.y : 0;
        face.rectTransform.sizeDelta = size;

        viewPort.anchoredPosition = new Vector2(faceCamera.character ? startingViewportX : face.rectTransform.anchoredPosition.x, viewPort.anchoredPosition.y);
    }

    protected override void DoOptions(string question, List<Option> options)
    {
        opt = options;
        msg = question;

        optionSelected = null;
        optionsMessage.text = msg;
        managingGroup = optionsGroup;
        managingGroup.gameObject.SetActive(true);
        interactionBlocker.SetActive(true);
        state = State.Opening;
        foreach (var o in opt)
        {
            // create the options
            var option = GameObject.Instantiate(optionPrefab);
            option.transform.SetParent(optionsHolder.transform);
            option.transform.localScale = Vector3.one;
            var text = option.transform.GetChild(0).GetComponent<Text>().text = o.Text;
            option.GetComponent<Button>().onClick.AddListener(() => {
                optionSelected = opt.Find(e => e.Text == text);
            });
            instancedOptions.Add(option);
        }
    }

    protected override bool IsFragmentFinised() { return frg == null; }
    protected override int IsOptionSelected() { return state == State.Idle ? opt.FindIndex(o => o == optionSelected) : -1;}

    // -----------------------------------
    // State management during update
    // -----------------------------------

    private Vector2 speed = Vector2.zero;
	void Update () {
        switch (state)
        {
            case State.Opening:
                // No matter what, when opening, just fade in the group
                managingGroup.alpha = Mathf.Clamp01(managingGroup.alpha + Time.deltaTime / fadeTime);
                if (managingGroup.alpha == 1)
                    state = State.Showing;
                break;
            case State.Showing:
                // When showing we'll care about the shown group
                if (managingGroup == dialogGroup)
                {

                    // If showing a fragment, show more characters until all of them are displayed
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (charactersShown < msg.Length)
                        {
                            charactersShown = msg.Length;
                            UpdateText();
                        }
                        else state = State.Closing;
                    }

                    accumulated += Time.deltaTime;
                    while (accumulated > timePerCharacter)
                    {
                        accumulated -= timePerCharacter;
                        charactersShown = Mathf.Clamp(charactersShown + 1, 0, msg.Length);
                        UpdateText();
                    }

                    if (currentCharacter)
                    {
                        currentCharacter.Talking = charactersShown < msg.Length;
                    }

                    var scrollpos = new Vector2(scroll.value, 0);
                    scrollpos = Vector2.SmoothDamp(scrollpos, new Vector2(scrollWantsToBe, 0), ref speed, scrollTime, scrollSpeed, Time.deltaTime);
                    scroll.value = scrollpos.x;
                }
                else if (managingGroup == optionsGroup)
                {
                    if (optionSelected != null) state = State.Closing;
                }

                break;
            case State.Closing:
                // No matter what, when closing, just fade out the displayed group
                managingGroup.alpha = Mathf.Clamp01(managingGroup.alpha - Time.deltaTime / fadeTime);
                if (managingGroup.alpha == 0)
                {
                    faceCamera.character = null;
                    managingGroup.gameObject.SetActive(false);
                    currentCharacter = null;
                    interactionBlocker.SetActive(false);
                    state = State.Idle;
                    textHolder.text = "";
                    textHolder.rectTransform.anchoredPosition = Vector2.zero;
                    frg = null;
                    speed = Vector2.zero;
                    foreach (var io in instancedOptions)
                        GameObject.DestroyImmediate(io);
                }
                break;
            case State.Idle:
                break;
        }
    }

    private void UpdateText()
    {
        textHolder.text = msg.Substring(0, charactersShown) + "<color=#00000000>" + msg.Substring(charactersShown) + "</color>";

        var lineCount = textHolder.cachedTextGenerator.lineCount;
        if (lineCount <= 1)
        {
            scrollWantsToBe = 1;
        }
        else
        {
            var lineOfCurrentChar = 0;
            for (int i = 0; i < lineCount; i++)
            {
                var line = textHolder.cachedTextGenerator.lines[i];
                if (line.startCharIdx < charactersShown)
                {
                    lineOfCurrentChar = i;
                }
            }
            
            scrollWantsToBe = 1f - (((float)lineOfCurrentChar) / (lineCount - 1f));
        }
    }
}
