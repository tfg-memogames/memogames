using UnityEngine;
using System.Collections;

public class SequenceInterpreter  {

	private ISequenceInterpreter currentInterpreter;
	private SequenceNode currentNode;

	public SequenceInterpreter(Sequence sequence){
		currentNode = sequence.Root;
	}

	public bool SequenceFinished {
		get { return currentNode == null || currentNode.Content == null; }
	}

	public void EventHappened(IGameEvent ge){
		if(currentInterpreter!=null)
			currentInterpreter.EventHappened(ge);
	}

	public void Tick(){
		if(!SequenceFinished)
        {
			if(currentInterpreter == null){
				currentInterpreter = SequenceInterpreterFactory.Intance.createSequenceInterpreterFor(currentNode);
				currentInterpreter.UseNode(currentNode);
			}

			currentInterpreter.Tick();

			if(currentInterpreter.HasFinishedInterpretation()){
				Debug.Log ("Finished interpretation");
				currentNode = currentInterpreter.NextNode();
				if(currentInterpreter is Object)
					Object.DestroyImmediate(currentInterpreter as Object);
				currentInterpreter = null;
			}
		}
	}

}
