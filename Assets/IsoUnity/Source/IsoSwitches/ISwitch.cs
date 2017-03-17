using UnityEngine;
using System.Collections;

[System.Serializable]
public class ISwitch : ScriptableObject {

	void Awake(){
		if(state == null){
			state = ScriptableObject.CreateInstance<IsoUnityBasicType>();

        }
    }

#if UNITY_EDITOR
    public void Persist()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            //state.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
            UnityEditor.AssetDatabase.AddObjectToAsset(state, this);
        }
    }

    void OnDestroy()
    {
        ScriptableObject.DestroyImmediate(state, true);
    }
#endif

    [SerializeField]
	public string id = "";

	[SerializeField]
	private IsoUnityBasicType state;
	public object State {
		get{ return state.Value;}
		set{ state.Value = value;}
	}
}
