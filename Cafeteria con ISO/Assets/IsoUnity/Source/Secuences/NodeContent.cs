using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface NodeContent {
    
    string[] ChildNames { get; }
    int ChildSlots { get; }
}
