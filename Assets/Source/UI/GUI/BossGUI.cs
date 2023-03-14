using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GK.Damage;
using GK.Core;
using GK.Entities;
using GK.NewBoss;

namespace GK.UI
{
    public class BossGUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text _nameLabel;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private NewBoss.Boss _boss;

        private void Awake()
        {
            _boss.OnDeath += Hide;
        }

        private void Start()
        {
            _boss.OnDamageTaken += UpdateUI;

            UpdateUI(new DamageInfo());
        }

        private void UpdateUI(DamageInfo info)
        {
            float health = _boss.HealthPercentage;
            _healthSlider.value = health;
        }

        private void Hide(Entity boss)
        {
            // hide all child objcets of this gameobject
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}