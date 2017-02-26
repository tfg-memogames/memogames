using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearCharacter : EventManager {

    public GameObject couple;

    public override void ReceiveEvent(IGameEvent ev)
    {
        if (ev.Name == "Couple_Leaves")
        {
            //Fernando se va
            if (this.couple != null)
                this.couple.SetActive(false);
        }
    }

    public override void Tick() {}

    
}
