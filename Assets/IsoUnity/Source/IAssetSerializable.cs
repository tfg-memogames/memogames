using UnityEngine;
using System.Collections;

namespace Isometra.Sequences {
	public interface IAssetSerializable {

	    void SerializeInside(Object assetObject);

	}
}