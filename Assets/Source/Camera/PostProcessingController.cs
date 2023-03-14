using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace GK.Camera
{
    public class PostProcessingController : MonoBehaviour
    {
        public static PostProcessingController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PostProcessingController>();
                }

                return _instance;
            }
        }

        private static PostProcessingController _instance;

        [SerializeField] private PostProcessVolume _postProcessVolume;

        public void SetSaturation(float saturation)
        {
            ColorGrading colorGrading;
            if (_postProcessVolume.profile.TryGetSettings(out colorGrading))
            {
                colorGrading.saturation.value = saturation;
            }
        }
    }
}
