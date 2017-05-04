using UnityEngine;
using UnityEditor;

namespace Isometra {
	public class TextureMenu {

		[MenuItem("Assets/Create/IsoTexture")]
		public static void createIsoTextureAsset(){
			IsoAssetsManager.CreateAssetInCurrentPathOf ("IsoTexture");
		}
	}
}