using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Zoom : MonoBehaviour
{

    public GameObject target;

    public float targetNear;
    public float time = 5.0f;
    public Vector2 offset;

    private float _currentTime;
    private float _nearIncrement;
    private Camera _camera;
    private Vector2 _targetPosition;
    private Vector2 _positionIncrement;


    private InputManager inputM;

    void Start()
    {
        this.inputM = GameObject.FindObjectOfType<InputManager>();
        _camera = this.GetComponent<Camera>();
        _currentTime = 0;
        _nearIncrement = this.targetNear - this._camera.orthographicSize;
        _targetPosition = target.transform.position;
        float actualX = this.transform.position.x;
        float actualY = this.transform.position.y;
        _positionIncrement = (_targetPosition - new Vector2(actualX, actualY)) + offset;
        this.enabled = false;
        //Debug.Log(_positionIncrement);
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        float percOfTime = Time.deltaTime / time;
        _camera.orthographicSize += _nearIncrement * percOfTime;
        _camera.transform.position += new Vector3(_positionIncrement.x * percOfTime, _positionIncrement.y * percOfTime);

        // When zoom is completed, destroy this component
        if (_currentTime >= time) {
            this.inputM.startDialogWithMaria();
            Destroy(this);

        }
    }



}