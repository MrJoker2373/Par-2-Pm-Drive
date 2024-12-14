namespace CarGame
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Menu _loading;
        private static SceneLoader _instance;
        private string _lastScene;
        private bool _isLoading;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
        }

        private void Start()
        {
            LoadScene("Menu");
        }

        public static SceneLoader GetInstance()
        {
            return _instance;
        }

        public bool IsLoading()
        {
            return _isLoading;
        }

        public void LoadScene(string name)
        {
            if (_isLoading == false)
                StartCoroutine(LoadSceneAsync(name));
        }

        public void LoadSceneQuick(string name)
        {
            if (string.IsNullOrEmpty(_lastScene) == false)
                SceneManager.UnloadScene(_lastScene);

            _lastScene = name;
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        }

        private IEnumerator LoadSceneAsync(string name)
        {
            _isLoading = true;
            Time.timeScale = 0;
            yield return _loading.Open();

            if (string.IsNullOrEmpty(_lastScene) == false)
            {
                var unload = SceneManager.UnloadSceneAsync(_lastScene);
                while (unload.isDone == false)
                    yield return null;
            }

            var load = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            while (load.isDone == false)
                yield return null;

            _lastScene = name;
            yield return _loading.Close();

            Time.timeScale = 1;
            _isLoading = false;
        }
    }
}