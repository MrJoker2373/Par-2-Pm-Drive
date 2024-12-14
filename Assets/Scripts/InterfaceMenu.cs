namespace CarGame
{
    using UnityEngine;
    using TMPro;
    using System.Collections;

    public class InterfaceMenu : MonoBehaviour
    {
        [SerializeField] private Menu _interface;
        [SerializeField] private Menu _win;
        [SerializeField] private int _coinDelta;
        [SerializeField] private TextMeshProUGUI _rewardLabel;
        [SerializeField] private Menu _lose;
        [SerializeField] private Menu _pause;
        [SerializeField] private AudioSource _winAudio;

        private IEnumerator Start()
        {
            yield return _interface.Open();
        }

        public void Win()
        {
            StartCoroutine(WinAsync());
        }

        public void Lose()
        {
            StartCoroutine(LoseAsync());
        }

        private IEnumerator WinAsync()
        {
            yield return _interface.Close();
            yield return _win.Open();
            _winAudio.Play();
            var inventory = Inventory.GetInstance();
            int reward = Mathf.RoundToInt(_coinDelta);
            _rewardLabel.text = $"You got: {reward} coins!";
            inventory.SetMoney(inventory.GetMoney() + reward);
        }

        private IEnumerator LoseAsync()
        {
            yield return _interface.Close();
            yield return _lose.Open();
        }

        public void Restart()
        {
            SceneLoader.GetInstance().LoadSceneQuick("Gameplay");
        }

        public void MainMenu()
        {
            SceneLoader.GetInstance().LoadSceneQuick("Menu");
        }

        public void Pause()
        {
            StartCoroutine(PauseAsync());
        }

        private IEnumerator PauseAsync()
        {
            yield return _interface.Close();
            yield return _pause.Open();
            Time.timeScale = 0;
        }

        public void Resume()
        {
            StartCoroutine(ResumeAsync());
        }

        private IEnumerator ResumeAsync()
        {
            yield return _interface.Open();
            yield return _pause.Close();
            Time.timeScale = 1;
        }
    }
}