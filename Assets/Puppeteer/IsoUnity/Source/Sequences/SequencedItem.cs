using UnityEngine;
using IsoUnity.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using IsoUnity.Entities;

namespace IsoUnity.Sequences {

    public class SequencedItem : Item {

        [SerializeField]
        SequenceAsset s;
        [SerializeField]
        private string description;
        [SerializeField]
        private Texture2D image;


        #region implemented abstract members of Item



        public void Init(){
            var sa = CreateInstance<SequenceAsset>();
            s = sa;
            #if UNITY_EDITOR
            AssetDatabase.SaveAssets();
            AssetDatabase.AddObjectToAsset(sa, this);
            AssetDatabase.SaveAssets();
            sa.InitAsset();
            #endif
        }

        public override void use()
        {
            var ge = new GameEvent("start sequence");
            ge.setParameter("sequence", s);
            Game.main.enqueueEvent(ge);
        }

        public override bool isEqualThan(Item other)
        {
            return other == this;
        }

        public override string Name { get { return this.name; } set { this.name = value; } }
        public override string Description { get { return this.description; } set { this.description = value; } }
        public override Texture2D Image { get { return this.image; } set { this.image = value; } }
        
        #endregion
    }

}

