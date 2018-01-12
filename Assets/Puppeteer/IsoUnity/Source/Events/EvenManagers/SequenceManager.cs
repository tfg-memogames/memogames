using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IsoUnity.Events;

namespace IsoUnity.Sequences {
	public class SequenceManager : EventManager
    {
        public List<Sequence> Executing { get { return executing; } }

        private List<Sequence> executing = new List<Sequence>();
		private List<SequenceInterpreter> sequenceInterpreter = new List<SequenceInterpreter>();
        private Dictionary<SequenceInterpreter, IGameEvent> toFinish = new Dictionary<SequenceInterpreter, IGameEvent>();

		public override void ReceiveEvent (IGameEvent ev)
        {
            if (ev.Name.ToLower() == "start sequence")
            {
                Sequence sequence = (ev.getParameter("Sequence") as Sequence);
                Executing.Add(sequence);
                var i = new SequenceInterpreter(sequence);
                sequenceInterpreter.Add(i);
                toFinish.Add(i, ev);
            }

            sequenceInterpreter.ForEach(si => si.EventHappened(ev));
        }

		public override void Tick()
        {
            var toRemove = new List<SequenceInterpreter>();
            foreach(var si in sequenceInterpreter)
            {
                si.Tick();
                if (si.SequenceFinished)
                {
                    Debug.Log("Sequence finished");
                    toRemove.Add(si);
                    Game.main.eventFinished(toFinish[si]);
                    Executing.Remove(toFinish[si].getParameter("sequence") as Sequence);
                    toFinish.Remove(si);
                }
            }
            foreach (var si in toRemove)
                sequenceInterpreter.Remove(si);
	            
		}
	}
}
