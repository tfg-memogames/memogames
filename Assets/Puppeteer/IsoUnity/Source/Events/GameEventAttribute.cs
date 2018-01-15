namespace IsoUnity.Events {

    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class GameEventAttribute : System.Attribute
    {
        public bool AutoFinish { get; private set; }
        public bool RequiresReference { get; private set; }
        public GameEventAttribute(bool autoFinish = true, bool requiresReference = true)
        {
            AutoFinish = autoFinish;
            RequiresReference = requiresReference;
        }
    }
}