using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Projectiles;
using GK.Core;

namespace GK.Enemies
{
    /// <summary>
    /// A form of attacking for an enemy where projectiles are fired in a predictable pattern.
    /// </summary>
    public class EnemyPatternAttacking : ProjectileShooter
    {
        [Header("Attack")]
        [SerializeField] private List<AttackPattern> _patterns;

        private int _currentPatternIndex;

        private void Start()
        {
            _currentPatternIndex = -1;
            StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            NextPattern();

            AttackPattern pattern = _patterns[_currentPatternIndex];

            yield return new WaitForSeconds(pattern.Delay.GetValue());
            foreach (Vector2 projectile in pattern.Projectiles)
            {
                Vector3 direction = DetermineDirection(projectile, pattern.UseLocalSpace);
                CreateProjectile(direction, pattern.Speed, pattern.Damage);
            }

                     
            yield return Attack();
        }

        /// <summary>
        /// Increments and wraps the pattern index to get the next
        /// pattern in list of patterns.
        /// </summary>
        private void NextPattern()
        {
            _currentPatternIndex += 1;
            if (_currentPatternIndex == _patterns.Count)
                _currentPatternIndex = 0;
        }

        /// <summary>
        /// Converts the 2d direction of an attack to either local or world space 3d vector.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private Vector3 DetermineDirection(Vector2 patternDirection, bool localSpace)
        {
            Vector3 direction = new Vector3(patternDirection.x, 0f, patternDirection.y).normalized;
            return (localSpace) ? transform.TransformDirection(direction) : direction;
        }
    }
    
    [System.Serializable]
    public class AttackPattern
    {
        public float Damage;
        public float Speed;
        public bool UseLocalSpace;
        public RandomRange Delay; // Before next attack.
        public List<Vector2> Projectiles;        
    }
}