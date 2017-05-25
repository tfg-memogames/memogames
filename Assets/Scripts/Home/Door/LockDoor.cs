using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class LockDoor : MonoBehaviour
{
    public string scene;
    private bool locked = true;
    

	private GameObject room;

	void Start(){
		room = GameObject.Find ("PickUp");
	}

    void OnMouseDown()
    {
		if (!locked) {
			SceneManager.LoadScene (scene);
			room.GetComponent<LoadRoom>().store ();
		}
    }



    public void openDoor()
    {
        this.locked = false;
    }
}

