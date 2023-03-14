using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GK.Audio;

namespace GK.NewBoss
{
    /// <summary>
    /// A single phase for a boss. Consists of multiple attacks that will be either cycled through or picked at random.
    /// </summary>
    public class Phase : MonoBehaviour
    {
        [Header("Attack settings")]
        [Tooltip("How long to wait after entering the phase before starting the first attack")]
        [SerializeField] private float _initialDelay;
        [SerializeField] private float _baseDelay;

        [Header("Audio")]
        [Tooltip("The music associated with the given phase. If left empty, the music will not change from last.")]
        [SerializeField] private AudioClip _phaseMusic;

        [Tooltip("The volume of the music.")]
        [SerializeField] private float _musicVolume = 1f;

        private List<Attack> _attacks = new List<Attack>();
        private Boss _boss;

        private float _timer;
        private bool _inAttack;
        private int _attackIndex = -1;

        private Attack CurrentAttack
        {
            get { return _attacks[_attackIndex]; }
        }

        private void Awake()
        {
            _attacks = GetComponentsInChildren<Attack>().ToList();
        }

        private void Update()
        {
            UpdatePhase();
        }

        public void EnterPhase(Boss boss)
        {
            _boss = boss;

            if (_phaseMusic != null)
            {
                PlayMusic();
            }

            OnPhaseEnter();
        }

        protected virtual void OnPhaseEnter()
        {
            
        }

        /// <summary>
        /// Plays the music associated with this phase from the music manager.
        /// </summary>
        private void PlayMusic()
        {
            MusicManager.Instance.PlaySong(_phaseMusic, _musicVolume);
            MusicManager.Instance.EaseIn(5f);
        }

        private void UpdatePhase()
        {
            if (_inAttack)
                return;

            _timer -= Time.deltaTime;

            // Perform new attack every time the timer reaches 0f.
            if (_timer <= 0f)
            {
                NextAttack();
            }
        }

        public void NextAttack()
        {
            _attackIndex++;
            if (_attackIndex >= _attacks.Count)
            {
                _attackIndex = 0;
            }

            Debug.Log(_attackIndex);

            _inAttack = true;
            _timer = _baseDelay;
            CurrentAttack.Boss = _boss;
            CurrentAttack.Phase = this;
            CurrentAttack.Exectute(OnAttackComplete);
        }

        /// <summary>
        /// Callback from the attack when it is finished. This is used to trigger the next attack.
        /// </summary>
        private void OnAttackComplete()
        {
            _inAttack = false;
        }
    }
}