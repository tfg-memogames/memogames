using UnityEditor;

public class SequenceMenu {

    [MenuItem("Assets/Create/Sequence")]
    public static void createIsoTextureAsset()
    {
        var seq = IsoAssetsManager.CreateAssetInCurrentPathOf("SequenceAsset") as Sequence;
        //seq.init();
    }
}
