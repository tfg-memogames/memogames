using System;
using System.Collections;
using System.Collections.Generic;
using Isometra;
using UnityEngine;

public class DissapearCharacter : Isometra.EventManager
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
        }
    }

    public override void Tick() {}

    
}
