using Isometra;
using System;
using System.Collections;
using System.Collections.Generic;
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
                this.parent.SetActive(false);
            door.openDoor();
			GameObject.Find ("PickUpManager").GetComponent<PickUpManager>().ini=false;
        }
    }

    public override void Tick() {}

    
}
