using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Entities;

namespace GK.Damage
{
    /// <summary>
    /// Info about a damage event that is created by a damage source and passed to a damage receiver.
    /// </summary>
    public struct DamageInfo
    {
        public IDamagable Receiver;
        public float Amount;
        public Vector3 OriginPoint;
    }
}
