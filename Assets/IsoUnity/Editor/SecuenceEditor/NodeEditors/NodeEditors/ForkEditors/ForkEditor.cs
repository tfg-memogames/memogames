using UnityEngine;
using System.Collections;

namespace Isometra.Sequences {
	public interface ForkEditor  {

		void draw();
		Checkable Result { get; }
		string ForkName{ get; }
		ForkEditor clone();
		bool manages(Checkable c);
		void useFork(Checkable c);
	}
}