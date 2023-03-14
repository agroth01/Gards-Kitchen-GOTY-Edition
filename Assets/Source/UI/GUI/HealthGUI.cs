using GK.Damage;
using GK.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.UI
{
    /// <summary>
    /// The GUI for the player health. Consists of 5 health units that can either be filled or empty.
    /// Every time the player takes damage, one unit will become empty.
    /// </summary>
    public class HealthGUI : MonoBehaviour
    {
        private List<PlayerHealthUnit> _healthBarUnits;
        private int _unitIndex;

        private void Awake()
        {
            _healthBarUnits = new List<PlayerHealthUnit>(GetComponentsInChildren<PlayerHealthUnit>());

            _unitIndex = _healthBarUnits.Count - 1;

            FindObjectOfType<Player.Player>().OnDamageTaken += OnDamageTaken;
        }

        private void OnDamageTaken(DamageInfo info)
        {
            EmptyNextUnit();
        }

        private void EmptyNextUnit()
        {
            if (_unitIndex < 0)
                return;

            _healthBarUnits[_unitIndex].SetFilled(false);
            _unitIndex--;
        }
    }
}