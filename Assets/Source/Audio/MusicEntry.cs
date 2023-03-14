using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Audio
{
    [System.Serializable]
    public class MusicEntry
    {
        public AudioClip Clip;
        public string DisplayName;
        public string InternalName;
        public float Volume = 1f;
        public float Pitch = 1f;
    }
}