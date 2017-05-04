using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isometra.Sequences {
	public interface NodeContent {
	    
	    string[] ChildNames { get; }
	    int ChildSlots { get; }
	}
}