using UnityEngine;
using IsoUnity.Events;
using System.Collections;

namespace IsoUnity.Sequences {
	public class SequenceLauncher : EventManager {

        [SerializeField]
        public bool overrideExecution = false;

        [SerializeField]
		public bool executeOnStart;

		[SerializeField]
		public bool loop;

		[SerializeField]
		public bool localExecution;

		[SerializeField]
		public bool launchOnTriggerEnter;

		[SerializeField]
		public bool launchOnMouseUpAsButton;

	    [SerializeField]
	    private Sequence sharedSequence;

	    [SerializeField]
		[HideInInspector]
	    private Sequence localSequence;

        private bool abort = false;

        public Sequence Sequence {
	        get
	        {
	            return localSequence;
	        }
	        set
	        {
	            sharedSequence = value;
				localSequence = sharedSequence == null ? null : sharedSequence.Clone();
	        }
	    }

        public bool Running
        {
            get
            {
                return interpreter != null || ge != null;
            }
        }

		void OnTriggerEnter(){
			if (launchOnTriggerEnter) {
				Launch ();
			}
		}

		void OnMouseUpAsButton(){
			if (launchOnMouseUpAsButton && (!sr || sr.color.a > 0)) {
				Launch ();
			}
		}

        private SpriteRenderer sr;
        private SequenceManager sm;

        public bool backin = true;

		void Start(){
			this.Sequence = sharedSequence;
            sr = GetComponent<SpriteRenderer>();
            sm = FindObjectOfType<SequenceManager>(); 


            if (executeOnStart) {
                if (backin)
                {
                    StartCoroutine(BackinRoutine());
                }
                else
                {
                    Launch();
                }
			}
		}

        private IEnumerator BackinRoutine()
        {
            yield return null;

            var ge = new GameEvent("backin", new System.Collections.Generic.Dictionary<string, object>() { { "synchronous", true } });
            Game.main.enqueueEvent(ge);

            yield return new WaitForEventFinished(ge);

            Launch();
        }


		private SequenceInterpreter interpreter;
		void Update(){
			if (localExecution && interpreter != null) {
				interpreter.Tick ();
				if (interpreter.SequenceFinished) {
					interpreter = null;
					if (loop && !abort) {
						Launch ();
					}
				}
			}
		}


		public void Launch(){
            if (!overrideExecution && sm.Executing.Count > 0)
                return;

			if (interpreter != null || localSequence == null)
				return;

			if (localExecution) {
                abort = false;
				if (interpreter == null)
					interpreter = new SequenceInterpreter (localSequence);
			} else {

				// Remote start
				ge = new GameEvent ("start sequence", new System.Collections.Generic.Dictionary<string, object> () {
					{ "sequence", localSequence }
				});
				Game.main.enqueueEvent (ge);
			}
		}
        public void Abort(bool instant)
        {
            if (localExecution)
            {
                abort = true;
                interpreter.Abort(instant);
            }
        }

		IGameEvent ge;
		public override void ReceiveEvent (IGameEvent ev)
		{
			if (interpreter != null)
				interpreter.EventHappened (ev);

			if (ev.Name == "event finished" && ev.getParameter ("event") == ge) {
				
				if (loop && !localExecution) {
					ge = null;
					Launch ();
				}
			}
		}

		public override void Tick (){}
	}
}