using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterSpriteManager : MonoBehaviour {

    public float talkTime = 0.3f;

    private SpriteRenderer spriteRenderer;

    private bool blinking = false;
    private bool talking = false;

    private float timeTalking = 0;

    public bool Blink {
        get { return blinking; }
        set
        {
            if (blinking) return;
            else
            {
                blinking = value;
                if (blinking) StartCoroutine(BlinkWait());
            }
        }
    }
    public bool Talking { get
        {
            return talking;
        }
        set
        {
            if(value != talking)
                timeTalking = 0;
            talking = value;
        }
    }

    public string startingExpression;

    [SerializeField]
    List<CharacterSprite> expressions;

    private int currentIndex = 0;
    private CharacterSprite currentExpression;

    [System.Serializable]
    public class CharacterSprite
    {
        public string expression;
        public Sprite sprite;
        public Sprite blink;
        public Sprite talking;
        public Sprite talkingBlink;
    }

    BoxCollider2D col;

    private void Start()
    {
        col = this.GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeSprite(string.IsNullOrEmpty(startingExpression) ? "normal" : startingExpression);
    }

    private void Update()
    {
        /*Talking = Input.GetKey(KeyCode.Q);
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            currentIndex = (currentIndex + 1) % expressions.Count;
            currentExpression = expressions[currentIndex];
        }*/

        if(col) col.enabled = spriteRenderer.color.a > 0;

        if (currentExpression == null)
        {
            spriteRenderer.sprite = null;
            return;
        }

        // Talking counter
        if (Talking)
        {
            timeTalking += Time.deltaTime;
            timeTalking %= talkTime;
        }

        // Sprite Update
        if (Talking && Blink)
        {
            spriteRenderer.sprite = (timeTalking >= talkTime/2f) ? currentExpression.blink : currentExpression.talkingBlink;
        }
        else if (Talking)
        {
            spriteRenderer.sprite = (timeTalking >= talkTime / 2f) ? currentExpression.sprite : currentExpression.talking;
        } 
        else if (Blink)
        {
            spriteRenderer.sprite = currentExpression.blink;
        }
        else
        {
            spriteRenderer.sprite = currentExpression.sprite;
        }
    }

    public void ChangeSprite(string expression)
    {
        currentExpression = expressions.Find(e => e.expression == expression);
    }

    private IEnumerator BlinkWait()
    {
        yield return new WaitForSeconds(0.1f);
        blinking = false;
    }
}
