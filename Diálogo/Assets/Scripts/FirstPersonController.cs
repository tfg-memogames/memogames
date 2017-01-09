using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{

    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 5.0f;
    public float jumpSpeed = 20.0f;

    float verticalRotation = 0;
    public float upDownRange = 60.0f;

    float verticalVelocity = 0;

    CharacterController characterController;

    //Cambios
    [SerializeField]
    DialogueManager _dialogue;

    private bool _playerSpeaking = false;


    private GameObject fernando;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation
        if(!_playerSpeaking) { 
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);


        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        
            

        // Movement

        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded && Input.GetButton("Jump"))
        {
            verticalVelocity = jumpSpeed;

        }

        Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);

        speed = transform.rotation * speed;


        characterController.Move(speed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        string name = other.gameObject.name;
        if (name == "Fernando")
            fernando = other.gameObject;
        //Debug.Log("Chocando");
        _dialogue.showDialogue(name);
        //characterController.enabled = false;
        _playerSpeaking = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void finishedDial()
    {
        _playerSpeaking = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (fernando != null)
            fernando.SetActive(false);
    }


    /*public bool playerSpeaking
    {
        get { return this._playerSpeaking; }
        set { _playerSpeaking = value; }
    }

    */

}
