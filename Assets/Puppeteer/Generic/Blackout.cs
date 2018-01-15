using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    [ExecuteInEditMode]
    [AddComponentMenu("Image Effects/Color Adjustments/Blackout")]
    public class Blackout : ImageEffectBase
    {
        [Range(-1.0f, 1.0f)]
        public float rampOffset;
        public float RampOffset
        {
            get
            {
                return rampOffset;
            }
            set
            {
                rampOffset = value;
            }
        }

        // Called by camera to apply image effect
        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            material.SetFloat("_RampOffset", rampOffset);
            Graphics.Blit(source, destination, material);
        }
    }
}
