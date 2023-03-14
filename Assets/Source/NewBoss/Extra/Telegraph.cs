using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.NewBoss
{
    /// <summary>
    /// A telegraph for an attack performed by a boss.
    /// </summary>
    public class Telegraph : MonoBehaviour
    {
        private Projector _projector;
        private Material _material;

        [SerializeField] private float _destructionTimer;

        private void Update()
        {
            _destructionTimer -= Time.deltaTime;
            if (_destructionTimer <= 0f)
            {
                Destroy(gameObject);
            }
        }

        public void Initialize(Material material, Vector2 position, float scale, float lifeTime)
        {
            _projector = GetComponent<Projector>();
            _material = material;
            _projector.material = _material;

            transform.position = new Vector3(position.x, 10f, position.y);
            _projector.orthographicSize = scale;

            _destructionTimer = lifeTime;
        }

        /// <summary>
        /// Sets the color property of the material directly.
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color color)
        {
            _material.SetColor("_Color", color);
        }

        public void SetScale(float newScale, float time=0f)
        {
            if (time == 0f)
            {
                _projector.orthographicSize = newScale;
            }
            else
            {
                StartCoroutine(LerpScale(newScale, time));
            }
        }

        private IEnumerator LerpScale(float ns, float t)
        {
            float timer = 0f;
            float startScale = _projector.orthographicSize;
            while (timer < t)
            {
                timer += Time.deltaTime;
                _projector.orthographicSize = Mathf.Lerp(startScale, ns, timer / t);
                yield return null;
            }
        }
    }
}
