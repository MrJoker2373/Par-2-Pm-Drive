namespace CarGame
{
    using UnityEngine;
    using UnityEngine.Audio;
    using UnityEngine.UI;

    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;
        private float _musicVolume;
        private float _sfxVolume;

        private void Awake()
        {
            var inventory = Inventory.GetInstance();
            _musicVolume = inventory.GetMusicVolume();
            _sfxVolume = inventory.GetSFXVolume();
            _musicSlider.normalizedValue = _musicVolume;
            _sfxSlider.normalizedValue = _sfxVolume;
            Refresh();
        }

        public void OnMusicChanged(float value)
        {
            _musicVolume = value;
            Inventory.GetInstance().SetMusicVolume(value);
            Refresh();
        }

        public void OnSFXChanged(float value)
        {
            _sfxVolume = value;
            Inventory.GetInstance().SetSFXVolume(value);
            Refresh();
        }

        private void Refresh()
        {
            _mixer.SetFloat("MusicVolume", Mathf.Log10(_musicVolume) * 20f);
            _mixer.SetFloat("SFXVolume", Mathf.Log10(_sfxVolume) * 20f);
        }
    }
}