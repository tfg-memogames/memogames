using UnityEditor;

namespace Isometra.Sequences {
	public class SequenceMenu {

	    [MenuItem("Assets/Create/Sequence")]
	    public static void createIsoTextureAsset()
	    {
	        var seq = IsoAssetsManager.CreateAssetInCurrentPathOf("SequenceAsset") as SequenceAsset;
	        seq.InitAsset();
	        //seq.init();
	    }
	}
}