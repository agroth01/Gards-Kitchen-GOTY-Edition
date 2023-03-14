using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Core;
using GK.Damage;

namespace GK.NewBoss
{
    /// <summary>
    /// Quickly turns on a trigger collider that will damage the player if inside of.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class DamageArea : MonoBehaviour
    {
        Affiliation _affiliation;
        private float _damage;
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }


        public void Damage(float duration, float damage, Affiliation affiliation)
        {
            _damage = damage;
            _affiliation = affiliation;
            StartCoroutine(TemporaryEnable(duration));
        }

        private IEnumerator TemporaryEnable(float t)
        {
            _collider.enabled = true;
            yield return new WaitForSeconds(t);
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);

            if (other.CompareTag(_affiliation.GetOppositeTag()))
            {
                IDamagable damagable = other.GetComponent<IDamagable>();

                DamageInfo di = new DamageInfo();
                di.Receiver = damagable;
                di.Amount = _damage;

                damagable.Damage(di);
            }
        }
    }
}
