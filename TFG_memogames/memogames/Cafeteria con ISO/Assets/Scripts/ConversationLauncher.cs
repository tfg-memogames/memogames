using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationLauncher : MonoBehaviour {


    public Sequence seq;

    public void startDialog()
    {
        var ge = new GameEvent();
        ge.Name = "start sequence";
        ge.setParameter("sequence", seq);
        Game.main.enqueueEvent(ge);
    }

    private void OnMouseDown()
    {
        startDialog();
    }



}
