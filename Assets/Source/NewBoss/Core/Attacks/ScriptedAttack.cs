using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.NewBoss
{
    /// <summary>
    /// An attack where the projectiles/others are manually controlled through script. This is one possible way to create attacks,
    /// where the other one is to launch a collection of projectiles in a prefab instead.
    /// </summary>
    public class ScriptedAttack : Attack
    {
        [System.Serializable]
        private class SingleScriptedAttack
        {
            public float Delay;
            public GameObject Projectile;
            public Vector2 Direction;
        }

        [Header("Attack")]
        [SerializeField] private bool _useWorldSpace;
        [SerializeField] private List<SingleScriptedAttack> _projectiles;
        
        protected override void OnAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}