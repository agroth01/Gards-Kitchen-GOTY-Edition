using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Entities;
using GK.Damage;

namespace GK.Player
{
    public class Player : Entity
    {
        protected override void Death()
        {

        }

        protected override void TakeDamage(DamageInfo damageInfo)
        {
            // For now, the health is simply reduced, ignoring anything else.
            Health = Health - damageInfo.Amount;
        }
    }
}