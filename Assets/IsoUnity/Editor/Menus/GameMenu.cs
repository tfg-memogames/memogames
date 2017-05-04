using UnityEngine;
using UnityEditor;
using Isometra.Sequences;

namespace Isometra {
	public class GameMenu {

		[MenuItem("GameObject/IsoUnity/IsoUnity Game", false, 10)]
		public static void createGame(MenuCommand menuCommand){

			GameObject go = new GameObject ("Game");

			go.AddComponent<Game> ();
			go.AddComponent<SequenceManager> ();
			go.AddComponent<IsoSwitchesEventManager> ();

			Selection.activeObject = go;    
		}

	}
}