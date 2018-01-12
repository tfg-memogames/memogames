using UnityEngine;
using System.Collections;
using IsoUnity.Entities;

namespace IsoUnity.Events
{
	public interface IGameEvent : JSONAble {

		string Name {
			get; set;
		}

		/**
		 * Parameters
		 **/

		object getParameter (string param);
		void setParameter (string param, object content);
		void removeParameter (string param);
		string[] Params {
			get;
		}

        /**
		 * Belongs
		 **/

        bool belongsTo(MonoBehaviour mb);
        bool belongsTo(EventManager em);
        bool belongsTo (GameObject g);
		bool belongsTo (ScriptableObject so);
		bool belongsTo (string tag);

        bool belongsTo(MonoBehaviour mb, string parameter);
        bool belongsTo(EventManager em, string parameter);
        bool belongsTo (GameObject g, string parameter);
		bool belongsTo (ScriptableObject so, string parameter);
		bool belongsTo (string tag, string parameter);

		/**
	     * Operators 
	     **/
		int GetHashCode ();
		bool Equals (object o);
		//static bool operator ==(IGameEvent ge1, IGameEvent ge2);
		//static bool operator !=(IGameEvent ge1, IGameEvent ge2);

	}
}