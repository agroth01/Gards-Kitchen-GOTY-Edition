using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Damage
{
    /// <summary>
    /// The interface for any class that wants to be able to recieve damage.
    /// </summary>
    public interface IDamagable
    {
        public void Damage(DamageInfo damageInfo);
    }
}