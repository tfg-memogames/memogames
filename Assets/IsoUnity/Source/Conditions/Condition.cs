using UnityEngine;
using System.Collections;

namespace Isometra.Sequences {
	public abstract class Condition : ScriptableObject {

		public abstract bool Eval();
	}
}