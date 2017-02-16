using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[NodeContent(2)]
public abstract class Checkable : ScriptableObject, IFork
{
	public abstract bool check();

}