using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GK.NewBoss
{
    /// <summary>
    /// Manager for all the phases of a boss. Is responsible for tracking the health (or other conditions)
    /// and changing the phase accordingly.
    /// </summary>
    public class BossPhases : MonoBehaviour
    {
        [System.Serializable]
        private class PhaseThreshold
        {
            public Phase Phase;
            [Range(0,1)]
            public float HealthThreshold;
        }

        [SerializeField]
        private List<PhaseThreshold> _phases;
        
        private Boss _boss;
        private int _phaseIndex = -1;

        private PhaseThreshold CurrentPhase
        {
            get { return _phases[_phaseIndex]; }
        }

        private void Awake()
        {
            _boss = GetComponentInParent<Boss>();
            VerifyPhaseThresholds();

            // Disable all phases except for first
            foreach (PhaseThreshold phase in _phases)
            {
                phase.Phase.gameObject.SetActive(false);
            }

            _phases[0].Phase.gameObject.SetActive(true);
        }

        private void Start()
        {
            NextPhase();
        }

        private void Update()
        {
            if (_boss.HealthPercentage < CurrentPhase.HealthThreshold)
                NextPhase();
        }

        private void NextPhase()
        {
            if (_phaseIndex >= 0)
                CurrentPhase.Phase.gameObject.SetActive(false);

            // Increase the phase only if there are more phases, otherwise we stay at the last phase.
            if (_phaseIndex < _phases.Count - 1)
                _phaseIndex++;

            CurrentPhase.Phase.gameObject.SetActive(true);
            CurrentPhase.Phase.EnterPhase(_boss);
        }

        /// <summary>
        /// Ensures that the phase tresholds are correct in regards to the actual
        /// thresholds being in a decending order, as we don't want to skip phases.
        /// </summary>
        private void VerifyPhaseThresholds()
        {
            bool valid = true;
            float lastPhaseThreshold = 1f;
            foreach (PhaseThreshold phase in _phases)
            {
                if (phase.HealthThreshold > 1 || phase.HealthThreshold < 0 || phase.HealthThreshold > lastPhaseThreshold)
                {
                    valid = false;
                    break;
                }

                lastPhaseThreshold = phase.HealthThreshold;
            }
            
            if (!valid)
            {
                Debug.LogError("Phase thresholds are not valid. Please ensure that the thresholds are in a decending order and between 0 and 1.");
            }
        }
    }
}