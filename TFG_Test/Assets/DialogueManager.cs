using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DialogueManager : MonoBehaviour {

	[SerializeField]
	private Text textComponent;

	//Conversation configuration
	[SerializeField]
	private float SecondsBetweenCharacters = 0.1f;

	[SerializeField]
	private float TextSpeedMultiplier = 1.0f;

	public KeyCode DialogueInput = KeyCode.Return;
	public string[] Dialogue;
	public GameObject ContinueIcon;

	private GameObject _dialogueBox;
	private bool _isDialoguePlaying = false;
	private bool _isStringBeingRevealed = false;
	private bool _isEndOfDialogue = false;


	// Use this for initialization
	void Start () {
		_dialogueBox = GameObject.Find ("Dialogue Box");
		_dialogueBox.SetActive (true);
		textComponent.text = "";
		HideIcons ();
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (DialogueInput)) {
			if (!_isDialoguePlaying) {
				TextSpeedMultiplier = 1.0f;
				Debug.Log ("Velocidad restablecida a " + TextSpeedMultiplier);
				_isDialoguePlaying = true;
				StartCoroutine(StartDialogue());
			} else {
				TextSpeedMultiplier *= 0.2f;
				Debug.Log ("Velocidad aumentada a " + TextSpeedMultiplier);
			}
		}
	}

	private IEnumerator StartDialogue() {
		int length = Dialogue.Length;
		int currentDialogueIndex = 0;

		while (currentDialogueIndex < length || !_isStringBeingRevealed) {
			if (!_isStringBeingRevealed) {
				_isStringBeingRevealed = true;
				StartCoroutine (ShowLine (Dialogue [currentDialogueIndex]));
				currentDialogueIndex++;

				if (currentDialogueIndex <= length) {
					_isEndOfDialogue = true;
				}
			}

			yield return 0;
		}


		while(!Input.GetKeyDown (DialogueInput))
			yield return 0;

		HideIcons ();
		_isEndOfDialogue = false;
		_isDialoguePlaying = false;
	}

	private IEnumerator ShowLine(string lineToDisplay) {
		int length = lineToDisplay.Length;
		int charIndex = 0;

		textComponent.text = "";
		HideIcons ();

		while (charIndex < length) {
			textComponent.text += lineToDisplay[charIndex];
			charIndex++;

			yield return new WaitForSeconds (SecondsBetweenCharacters * TextSpeedMultiplier);
		}

		ShowIcon ();

		while(!Input.GetKeyDown (DialogueInput))
			yield return 0;

		TextSpeedMultiplier = 1.0f;
		Debug.Log ("Velocidad restablecida a " + TextSpeedMultiplier);
		_isStringBeingRevealed = false;
		textComponent.text = "";
	}

	private void HideIcons() {
		ContinueIcon.SetActive (false);
	}

	private void ShowIcon() {
		if (_isEndOfDialogue) {
			ContinueIcon.SetActive (true);
		}
	}
}
