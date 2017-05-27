using Isometra;
using Isometra.Sequences;
using UnityEngine;

public class ConversationLauncher : MonoBehaviour {


    public Sequence seq;
    public bool active = true;

    public void startDialog()
    {
        /*
        var ge = new GameEvent();
        ge.Name = "start sequence";
        ge.setParameter("sequence", seq);
        Game.main.enqueueEvent(ge);
        */


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
