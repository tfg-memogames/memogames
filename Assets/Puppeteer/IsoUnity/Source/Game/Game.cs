using UnityEngine;
using System.Collections.Generic;
using IsoUnity.Events;

namespace IsoUnity
{
    [RequireComponent(typeof(IsoSwitchesEventManager))]
    public class Game : MonoBehaviour
    {
        public bool debugEvents = false;

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
        private List<IEventManager> eventManagers;
        
        /*
         * Screen controls default controller.
         */
        public bool onScreenControls;

        /*
         * Static main game instance
         */
        private static Game m;
        public static Game main
        {
            get
            {
                if (m == null)
                {
                    m = FindObjectOfType<Game>();
                    if(m!=null) m.Awake();
                }
                return m;
            }
        }

        /*
         * Game initialization
         */
        private bool awakened = false;
        void Start ()
        {
        }

        void Awake()
        {
            if (awakened)
                return;
            awakened = true;

            if (Game.main != this)
            {
                if (shouldReplacePreviousGame)
                {
                    GameObject.DestroyImmediate(Game.main.gameObject);
                }
                else
                {
                    if (Game.main != null)
                    {
                        Game.main.transform.position = this.transform.position;
                        Camera.main.backgroundColor = this.GetComponent<Camera>().backgroundColor;
                        GameObject.DestroyImmediate(this.gameObject);
                        return;
                    }
                }
            }


            Game.m = this;
            if (Application.isPlaying)
                GameObject.DontDestroyOnLoad(this.gameObject);

            // Event Queue
            events = new Queue<IGameEvent>();

            // Main Managers initialization
            // TODO Make they event managers as the rest
            IsoSwitchesManager.getInstance().getIsoSwitches();

            // Event Managers Creation
            eventManagers = new List<IEventManager>();
        }
        void Update()
        {
            this.tick();
        }

        /*
         * Event methods
         */

        public void enqueueEvent(IGameEvent ge)
        {
            if (ge == null)
                return;
            if (debugEvents)
            {
                Debug.Log(ge.Name);
            }
            this.events.Enqueue(ge);
        }

        public void eventFinished(IGameEvent ge, Dictionary<string, object> extraParameters = null)
        {
            object sync = ge.getParameter("synchronous");
            if (sync != null && ((bool)sync))
            {
                GameEvent f = new GameEvent();
                f.Name = "event finished";
                f.setParameter("event", ge);
                // Put the extra parameters
                if (extraParameters != null) foreach (var kv in extraParameters) f.setParameter(kv.Key, kv.Value);
                this.enqueueEvent(f);
            }
        }

        // Private method used to broadcast the events in main tick
        private void broadcastEvent(IGameEvent ge)
        {
            foreach (IEventManager manager in eventManagers)
                manager.ReceiveEvent(ge);
        }

        /*
         * As the player input isnt so frecuent, it's only checked each ms to improve performance
         */
      //  private float timeToController = 100 / 1000;
        //private float currentTimeToController = 0;

        public void tick()
        {
            FlushRegistrations();

            // Events launch
            while (events.Count > 0)
            {
                IGameEvent ge = events.Dequeue();
                broadcastEvent(ge);
            }

            // EventManagers ticks
            foreach (var manager in eventManagers)
                manager.Tick();
        }

        /**
         * EventManager management
         **/
        List<IEventManager> toRegister = new List<IEventManager>();
        public void RegisterEventManager(IEventManager em)
        {
            if (!this.eventManagers.Contains(em))
            {
                toRegister.Add(em);
            }
        }

        List<IEventManager> toDeregister = new List<IEventManager>();
        public void DeRegisterEventManager(IEventManager em)
        {
            if (this.eventManagers.Contains(em) || toRegister.Contains(em))
                toDeregister.Add(em);
        }

        void FlushRegistrations()
        {
            foreach (var em in toRegister)
                eventManagers.Add(em);
            toRegister.Clear();

            foreach (var em in toDeregister)
                eventManagers.Remove(em);
            toDeregister.Clear();
        }

    }
}