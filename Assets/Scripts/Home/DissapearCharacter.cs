using IsoUnity.Events;
using UnityEngine;

public class DissapearCharacter : EventManager
{

    public GameObject parent;
    public LockDoor door;


    public override void ReceiveEvent(IGameEvent ev)
    {
        if (ev.Name == "Parent_Leaves")
        {
            //Fernando se va
			if (this.parent != null)
				Destroy (this.parent);
            door.openDoor();
			GameObject.Find ("CharacterController").GetComponent<CharactersController>().ini=false;
        }
    }

    public override void Tick() {}

    
}
