using UnityEngine;
using System.Collections;

namespace Isometra {
	public abstract class EventManager : MonoBehaviour{

		void OnEnable(){
			Game.main.RegisterEventManager (this);
		}

		void OnDisable(){
			if(Game.main)
	            Game.main.DeRegisterEventManager (this);
		}

		public abstract void ReceiveEvent (IGameEvent ev);
		public abstract void Tick ();

	}
}