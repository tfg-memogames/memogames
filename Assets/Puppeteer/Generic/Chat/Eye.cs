using UnityEngine;
using System.Collections;

public class Eye : MonoBehaviour {

    public Vector2 openPosition;

    private bool closing;
    private Vector2 finalPosition;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void close(){
        this.finalPosition = new Vector3 (transform.localPosition.x, minXY.y, 4f);
        closing = true;
    }

    public void open()
    {
        this.finalPosition = new Vector3(openPosition.x, openPosition.y, 4f);
        closing = true;
    }

    private float easing = 0.05f;
    public Vector2 minXY;
    public bool _____________________________;
    // fields set dynamically
    public float camZ; // The desired Z pos of the camera
    void Awake() {
        closing = false;
        camZ = this.transform.localPosition.z;
    }

    void FixedUpdate () {
        if (closing) {
            Vector3 destination;
            // If there is no poi, return to P:[0,0,0]
            //easing += Time.fixedDeltaTime;
            if (finalPosition == null) {
                destination = Vector3.zero;
            } else {
                destination = finalPosition;
            }
            // Limit the X & Y to minimum values
            //destination.y = minXY.y > 0 ? Mathf.Max (minXY.y, destination.y) : Mathf.Min(minXY.y, destination.y);
            // Interpolate from the current Camera position toward destination
            destination = Vector3.Lerp (transform.localPosition, destination, easing);
            // Retain a destination.z of camZ
            destination.z = camZ;
            // Set the camera to the destination
            transform.localPosition = destination;
        }
    }
}
