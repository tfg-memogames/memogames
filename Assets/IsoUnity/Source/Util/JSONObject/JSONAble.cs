using UnityEngine;
using System.Collections;

namespace Isometra {
	public interface JSONAble 
	{
	    JSONObject toJSONObject();
	    void fromJSONObject(JSONObject json);
	}
}