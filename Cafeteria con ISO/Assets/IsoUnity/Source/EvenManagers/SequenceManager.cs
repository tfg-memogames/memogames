using UnityEngine;
using System.Collections;

public class SequenceManager : EventManager {
	
	private SequenceInterpreter sequenceInterpreter;

	public override void ReceiveEvent (IGameEvent ev)
	{
		if(sequenceInterpreter == null){
			if(ev.Name.ToLower() == "start sequence"){
				Sequence secuence = (ev.getParameter("Sequence") as Sequence);
				sequenceInterpreter = new SequenceInterpreter(secuence);
			}
		}else sequenceInterpreter.EventHappened(ev);
	}

	public override void Tick(){
		if(sequenceInterpreter != null){
            sequenceInterpreter.Tick();
			if(sequenceInterpreter.SequenceFinished){
				Debug.Log("Sequence finished");
				this.sequenceInterpreter = null;
			}
		}
	}
}
