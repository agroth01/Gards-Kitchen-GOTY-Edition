using GK.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GK.UI
{
    /// <summary>
    /// Contains method for reloading the current scene.
    /// </summary>
    public class RestartButton : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _text;

        private void Awake()
        {
            FindObjectOfType<Player.Player>().OnDeath += Show;
        }

        private void Show(Entity entity)
        {
            _image.enabled = true;
            _text.SetActive(true);
        }

        public void Reload()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
