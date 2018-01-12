using UnityEngine;
using System.Collections;
using System;
using System.Linq;

namespace IsoUnity.Types
{
	public class IsoUnityEnumType : IsoUnityType
	{
        [SerializeField]
	    private int enumerated = 0;
        [SerializeField]
        private string type = "";

        private void OnEnable()
        {
        }

        public override bool canHandle(object o)
	    {
	        return o is System.Enum;
	    }

	    public override IsoUnityType clone()
	    {
	        return IsoUnityEnumType.CreateInstance<IsoUnityEnumType>();
	    }

        public static Type GetEnumType(string name)
        {
            // Since unity has an specific assembly for Plugins, this is neccesary
            return
             (from assembly in AppDomain.CurrentDomain.GetAssemblies()
              let type = assembly.GetType(name)
              where type != null
                 && type.IsEnum
              select type).FirstOrDefault();
        }

        public override object Value
	    {
	        get
	        {
                var t = GetEnumType(type);
                var r =  t != null ? Enum.ToObject(t, enumerated): enumerated;
                return r;
	        }
	        set
	        {
	            enumerated = (int)value;
                type = value.GetType().FullName;
	        }
	    }

	    public override JSONObject toJSONObject()
	    {
	        throw new System.NotImplementedException();
	    }

	    public override void fromJSONObject(JSONObject json)
	    {
	        throw new System.NotImplementedException();
	    }
	}
}