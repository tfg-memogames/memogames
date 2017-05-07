using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class LockDoor : MonoBehaviour
{
    public string scene;
    private bool locked = true;
    

    void OnMouseDown()
    {
        if(!locked)
            SceneManager.LoadScene(scene);
    }



    public void openDoor()
    {
        this.locked = false;
    }
}

