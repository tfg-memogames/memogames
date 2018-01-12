using IsoUnity.Events;
using UnityEngine;

public class Medical : EventedEventManager
{

	private GameObject parent;

    [GameEvent(true, false)]
    public void medical()
    {
        CharactersController c = GameObject.Find("CharacterController").GetComponent<CharactersController>();
        parent = gameObject;
        if (this.parent != null)
            Destroy(parent);

        c.medical = true;
    }

	public override void Tick() {}


}
