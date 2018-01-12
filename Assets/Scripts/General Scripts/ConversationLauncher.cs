using IsoUnity;
using IsoUnity.Events;
using IsoUnity.Sequences;
using UnityEngine;

public class ConversationLauncher : MonoBehaviour {


    public Sequence seq;
    public bool active = true;


    public void startDialog()
    {
        this.active = false;
        // Remote start
        var ge = new GameEvent("start sequence", new System.Collections.Generic.Dictionary<string, object>() {
                    { "sequence", seq }
                });
        Game.main.enqueueEvent(ge);
    }


    private void childClicked(GameObject go)
    {
        if(active)
            startDialog();
    }



}
