using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Input
{
    /// <summary>
    /// Data about the input gathered from an InputListener and passed to an IInputListener.
    /// </summary>
    public struct InputData
    {
        public Vector3 MovementDirection;
        public bool Fire;
        public bool Dash;
    }
}