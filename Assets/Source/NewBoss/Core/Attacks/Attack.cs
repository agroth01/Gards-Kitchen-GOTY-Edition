using GK.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.NewBoss
{
    public abstract class Attack : MonoBehaviour
    {
        public Boss Boss;
        public Phase Phase;

        [Header("Timing")]
        [SerializeField] private RandomRange _attackDelay;

        [Header("Audio")]
        [SerializeField] private AudioClip _attackSound;

        private Action _onAttackComplete;
        

        protected float Delay
        {
            get { return _attackDelay.GetValue(); }
        }

        public void Exectute(Action completeCallback)
        {
            _onAttackComplete = completeCallback;
            OnAttack();
        }

        protected abstract void OnAttack();

        protected void CompleteAttack()
        {
            _onAttackComplete?.Invoke();
        }
    }
}