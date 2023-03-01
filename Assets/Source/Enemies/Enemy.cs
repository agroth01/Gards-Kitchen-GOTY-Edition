using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Entities;
using GK.Damage;
using System;

namespace GK.Enemies
{
    public class Enemy : Entity
    {
        [Header("Enemy settings")]
        [SerializeField] private int _coinDropAmount;

        protected override void Death()
        {
            Destroy(gameObject);
        }

        protected override void TakeDamage(DamageInfo damageInfo)
        {
            Health = Health - damageInfo.Amount;
        }
    }
}