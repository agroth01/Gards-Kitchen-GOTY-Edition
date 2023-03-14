using GK.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.NewBoss
{
    /// <summary>
    /// A class that can create telegraphs for boss attacks.
    /// </summary>
    public class TelegraphMaker : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Telegraph _telegraphPrefab;

        public static TelegraphMaker Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<TelegraphMaker>();
                }

                return _instance;
            }
        }

        private static TelegraphMaker _instance;

        public Telegraph CreateTelegraph(Material material, Vector2 position, float scale, float lifeTime)
        {
            return InternalCreation(material, position, scale, lifeTime);
        }

        private Telegraph InternalCreation(Material mat, Vector2 pos, float scale, float lt)
        {
            GameObject telegraphGO = Instantiate(_telegraphPrefab.gameObject, transform);
            telegraphGO.transform.position = new Vector3(pos.x, 10f, pos.y);

            Telegraph telegraph = telegraphGO.GetComponent<Telegraph>();
            telegraph.Initialize(mat, pos, scale, lt);

            return telegraph;
        }
    }
}