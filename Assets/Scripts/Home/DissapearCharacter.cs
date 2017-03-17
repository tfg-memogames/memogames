using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearCharacter : EventManager {

    public GameObject parent;

    public override void ReceiveEvent(IGameEvent ev)
    {
        if (ev.Name == "Parent_Leaves")
        {
            //Fernando se va
            if (this.parent != null)
                this.parent.SetActive(false);
        }
    }

    public override void Tick() {}

    
}
