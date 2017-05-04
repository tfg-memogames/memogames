using UnityEngine;
using System.Collections.Generic;

namespace Isometra.Sequences {
	[System.Serializable]
	public abstract class Checkable : ScriptableObject, IFork
	{
		public abstract bool check();

	}
}