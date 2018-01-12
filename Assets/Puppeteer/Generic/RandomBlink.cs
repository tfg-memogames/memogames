using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterSpriteManager))]
public class RandomBlink : MonoBehaviour {

    public float timeBetween = 2f;
    public float variation = 1f;

    private float toBlink;

    private CharacterSpriteManager spriteManager;

    // Use this for initialization
    void Start () {
        spriteManager = GetComponent<CharacterSpriteManager>();
        ResetTimeToBlink();
    }
	
	// Update is called once per frame
	void Update () {
        toBlink -= Time.deltaTime;
        if(toBlink <= 0)
        {
            spriteManager.Blink = true;
            ResetTimeToBlink();
        }
	}

    private void ResetTimeToBlink()
    {
        var rand = Random.Range(-1f, 1f);
        toBlink = timeBetween + ((rand*rand*rand) * variation);
    }
}
