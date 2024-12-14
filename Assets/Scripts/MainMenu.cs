namespace CarGame
{
    using System.Collections;
    using UnityEngine;

    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Menu _main;
        [SerializeField] private Menu _settings;
        [SerializeField] private Menu _shop;
        [SerializeField] private Menu _awards;
        private Menu _current;
        private bool _isOpen;

        private void Start()
        {
            StartCoroutine(Open(_main));
        }

        public void OpenMain()
        {
            StartCoroutine(Open(_main));
        }

        public void OpenSettings()
        {
            StartCoroutine(Open(_settings));
        }

        public void OpenShop()
        {
            StartCoroutine(Open(_shop));
        }

        public void OpenAwards()
        {
            StartCoroutine(Open(_awards));
        }

        public void PlayGame()
        {
            SceneLoader.GetInstance().LoadSceneQuick("Gameplay");
        }

        private IEnumerator Open(Menu menu)
        {
            if (_isOpen == false)
            {
                _isOpen = true;
                if (_current != null)
                    yield return _current.Close();
                _current = menu;
                yield return _current.Open();
                _isOpen = false;
            }
        }
    }
}