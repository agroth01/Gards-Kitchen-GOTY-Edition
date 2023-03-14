using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Core;

namespace GK.NewBoss
{
    /// <summary>
    /// This is an attack for the coke lord where he will summon down smaller coke bottles
    /// that will hit the ground and deal damage in an area. These coke bottles will have
    /// telegraphs that shows where they will land, then quickly slam down one at a time, rapidly.
    /// </summary>
    public class CL_BottleAttack : Attack
    {
        [Header("Bottle settings")]
        [SerializeField] private GameObject _bottlePrefab;
        [SerializeField] private int _bottleAmount = 5;
        [SerializeField] private float _spawnRadius;       
        [SerializeField] private float _delayBeforeSlam;
        [SerializeField] private float _spawnHeight;

        [Header("Timing")]
        [SerializeField] private float _timeBetweenSlams;
        [SerializeField] private float _slamSpeed;

        [Header("Feedbacks")]
        [SerializeField] private MMF_Player _feedback;

        [Header("Telegraphing")]
        [SerializeField] private Material _telegraphMaterial;
        [SerializeField] private float _telegraphSize;

        [Header("Damage")]
        [SerializeField] private DamageArea _damageAreaPrefab;
        [SerializeField] private float _bottleDamage;
        [SerializeField] private float _damageDuration;

        [Header("Bottle shrinking")]
        [SerializeField] private float _shrinkSpeed;
        [SerializeField] private float _timeBeforeShrinking;

        private int _bottlesToSlam;
        private bool _bottlesSpawned;

        protected override void OnAttack()
        {
            StartCoroutine(BottleAttack());
        }

        private void Update()
        {
            if (_bottlesToSlam <= 0 && _bottlesSpawned)
            {
                _bottlesSpawned = false;

                CompleteAttack();
            }
        }

        /// <summary>
        /// This is used to start more coroutines that is responsible for controlling a single
        /// bottle. It handles the logic for spawning the coke bottles, then setting each bottle
        /// into it's own coroutine.
        /// </summary>
        /// <returns></returns>
        private IEnumerator BottleAttack()
        {
            _bottlesToSlam = 0;

            yield return new WaitForSeconds(Delay);

            float slamOffset = _delayBeforeSlam;
            for (int i = 0; i < _bottleAmount; i++)
            {
                // Create the bottle
                _bottlesToSlam++;

                float extraOffset = _timeBetweenSlams * i;
                float realOffset = slamOffset + extraOffset;

                Vector3 position = (Random.insideUnitSphere * _spawnRadius).WithY(_spawnHeight);
                GameObject bottleGO = Instantiate(_bottlePrefab, position, Quaternion.identity);
                bottleGO.transform.parent = Phase.transform;
                StartCoroutine(HandleBottle(bottleGO, realOffset));

                // Set the telegraph at same position
                TelegraphMaker.Instance.CreateTelegraph(_telegraphMaterial, new Vector2(position.x, position.z), _telegraphSize, realOffset + 1 / _slamSpeed);
            }

            _bottlesSpawned = true;
        }

        private IEnumerator HandleBottle(GameObject bottle, float slamOffset)
        {
            yield return new WaitForSeconds(slamOffset);

            // Animate that the bottle gets slammed into the ground. This is going to lerp the bottle
            // from spawnPoint to y0, check for co
            float timer = 0f;
            float slamTime = 1 / _slamSpeed;
            Vector3 startPos = bottle.transform.position;
            Vector3 endPos = startPos.WithY(0f);

            while (timer < slamTime)
            {
                timer += Time.deltaTime;
                bottle.transform.position = Vector3.Lerp(startPos, endPos, timer / slamTime);
                yield return null;
            }

            // Feedbacks of screenshake since bottle hit the ground
            _feedback.PlayFeedbacks();

            // Create the damage area
            DamageArea damageArea = Instantiate(_damageAreaPrefab, bottle.transform.position, Quaternion.identity);
            damageArea.Damage(_damageDuration, _bottleDamage, Affiliation.Hostile);

            // Wait for a bit before starting to shrink the bottle
            yield return new WaitForSeconds(_timeBeforeShrinking);

            // Start shrinking the bottle
            timer = 0f;
            float shrinkTime = 1 / _shrinkSpeed;
            Vector3 startScale = bottle.transform.localScale;
            Vector3 endScale = Vector3.zero;

            while (timer < shrinkTime)
            {
                timer += Time.deltaTime;
                bottle.transform.localScale = Vector3.Lerp(startScale, endScale, timer / shrinkTime);
                yield return null;
            }

            // Destroy the bottle
            Destroy(bottle);

            // Decrease the amount of bottles to slam for ending attack
            _bottlesToSlam--;
        }
    }
}