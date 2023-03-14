using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.UI
{
    public class PlayerHealthUnit : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _filled;
        [SerializeField] private GameObject _empty;

        public void SetFilled(bool value)
        {
            _filled.SetActive(value);
            _empty.SetActive(!value);
        }
    }
}