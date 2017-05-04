using UnityEngine;
using System.Collections.Generic;

namespace Isometra {
	public class Game : MonoBehaviour {

	    public string Test { get { return "test"; } }

		/**
		 * This var allows new Game instances load destroy the previous one and replace it.
		 * This can have unexpected behaviours, we recommend to use only one Game class along
		 * all the game execution.
		 */
		public bool shouldReplacePreviousGame = false;

		Queue<IGameEvent> events;

	    /*
	     * Event Manager Things
	     * Use this list to create the managers at the start of the game.
	     * (By default Animation, Secuence and IsoSwitches Managers are created).
	     */
		private List<EventManager> eventManagers;


	    /*
	     * Screen controls default controller.
	     */
		public bool onScreenControls;

	    /*
	     * Static main game instance
	     */
		private static Game m;
		public static Game main {
			get{
				if (m == null) {
					m = FindObjectOfType<Game> ();
	                if (!m && !quitting)
	                {
	                    var g = new GameObject();
	                    m = g.AddComponent<Game>();
	                }
	                if(!quitting)
	                    m.Awake();
	            }
				return m;
			}
		}

	    /*
	     * Game initialization
	     */
		private bool awakened = false;
		void Awake () {
			if (awakened)
				return;
			awakened = true;

			if (Game.main != this) {
				if (shouldReplacePreviousGame) {
					GameObject.DestroyImmediate (Game.main.gameObject);
				} else {
					if (Game.main != null) {
						GameObject.DestroyImmediate (this.gameObject);
						return;
					}
				}
			}

			Game.m = this;
			if(Application.isPlaying)
				GameObject.DontDestroyOnLoad (this.gameObject);

	        // Event Queue
			events = new Queue<IGameEvent>();
	        
			IsoSwitchesManager.getInstance().getIsoSwitches();

	        // Event Managers Creation
			eventManagers = new List<EventManager> ();
	        
		}
		
		void Update () {
			this.tick();
		}

	    /*
	     * Event methods
	     */

		public void enqueueEvent(IGameEvent ge){
			if(ge == null)
				return;
	        Debug.Log(ge.Name);
			this.events.Enqueue(ge);
		}
	    
		public void eventFinished(IGameEvent ge, Dictionary<string, object> extraParameters = null){
			object sync = ge.getParameter("synchronous");
			if(sync!=null && ((bool)sync)){
				GameEvent f = new GameEvent();
				f.Name = "event finished";
				f.setParameter("event", ge);
	            // Put the extra parameters
	            if (extraParameters != null) foreach (var kv in extraParameters) f.setParameter(kv.Key, kv.Value);
				this.enqueueEvent(f);
			}
		}

	    // Private method used to broadcast the events in main tick
	    private void broadcastEvent(IGameEvent ge){
	        foreach (EventManager manager in eventManagers)
	            manager.ReceiveEvent(ge);
	    }

	    /*
	     * As the player input isnt so frecuent, it's only checked each ms to improve performance
	     */
	    //private float timeToController = 100 / 1000;
	    //private float currentTimeToController = 0;

		public void tick(){

	        // Main Tick

	        // Events launch
			while(events.Count > 0)
			{
				IGameEvent ge = events.Dequeue();
				broadcastEvent(ge);
			}

	        // EventManagers ticks
			foreach(EventManager manager in eventManagers)
				manager.Tick();
		}

		/**
		 * EventManager management
		 **/

		public void RegisterEventManager(EventManager em){
	        if (em == null)
	            return;

			if(!this.eventManagers.Contains(em))
				this.eventManagers.Add (em);
		}

		public void DeRegisterEventManager(EventManager em){
	        if (em == null)
	            return;

	        if (this.eventManagers.Contains(em))
				this.eventManagers.Remove (em);
		}

	    static bool quitting = false;
	    void OnApplicationQuit()
	    {
	        quitting = true;
	    }

	    void OnDestroy()
	    {

	    }

	}
}