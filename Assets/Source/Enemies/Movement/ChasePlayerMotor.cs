using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Core;

namespace GK.Enemies
{
    public class ChasePlayerMotor : EnemyMotor
    {
        [Header("Chase settings")]
        [Tooltip("How close the enemy will try to get to the player. (Does not account for width/height of either enemy or player.)")]
        [SerializeField] private float _targetDistance;
        
        private Transform _player;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag(Affiliation.Friendly.GetTag()).transform;
        }

        private void Update()
        {
            if (_player != null)
            {
                float distance = transform.DistanceTo(_player);
                if (distance > _targetDistance)
                {
                    Vector3 destination = _player.position.WithY(transform.position.y);
                    SetDestination(destination);
                }

                else
                {
                    StopMoving();
                }
                
            }
        }
    }
}