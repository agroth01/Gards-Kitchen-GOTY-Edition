using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Entities;
using GK.Damage;
using MoreMountains.Feedbacks;
using GK.Camera;
using GK.Audio;

namespace GK.Player
{
    public class Player : Entity
    {
        [Header("Feedback")]
        [SerializeField] private MMF_Player _feedback;

        private void Start()
        {
            Application.runInBackground = false;
        }

        protected override void Death()
        {
            PostProcessingController.Instance.SetSaturation(-100f);
            Time.timeScale = 0f;
            MusicManager.Instance.EaseOut(2f);
        }

        protected override void TakeDamage(DamageInfo damageInfo)
        {
            _feedback.PlayFeedbacks();

            // For now, the health is simply reduced, ignoring anything else.
            Health = Health - damageInfo.Amount;
        }
    }
}