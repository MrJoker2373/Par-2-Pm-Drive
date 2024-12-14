namespace CarGame
{
    using System.Collections.Generic;
    using UnityEngine;

    public partial class Inventory : MonoBehaviour
    {
        private static Inventory _instance;
        private int _money;
        private int _boosts;
        private int _punches;
        private int _shields;
        private int _playCount;
        private int _loseCount;
        private int _winCount;
        private List<CarType> _purchasedCars;
        private List<int> _completedRewards;
        private List<int> _takenRewards;
        private CarType _currentCar;
        private float _musicVolume;
        private float _sfxVolume;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
        }

        private void Start()
        {
            _money = PlayerPrefs.GetInt("Money");
            _boosts = PlayerPrefs.GetInt("Boosts");
            _punches = PlayerPrefs.GetInt("Punches");
            _shields = PlayerPrefs.GetInt("Shields");
            _playCount = PlayerPrefs.GetInt("PlayCount");
            _loseCount = PlayerPrefs.GetInt("LoseCount");
            _winCount = PlayerPrefs.GetInt("WinCount");

            if (PlayerPrefs.HasKey("CurrentCar"))
            {
                var carJson = PlayerPrefs.GetString("CurrentCar");
                _currentCar = JsonUtility.FromJson<CarType>(carJson);
            }
            else
                _currentCar = CarType.Default;

            if (PlayerPrefs.HasKey("PurchasedCars"))
            {
                var purchasedCarsJson = PlayerPrefs.GetString("PurchasedCars");
                _purchasedCars = JsonUtility.FromJson<List<CarType>>(purchasedCarsJson);
            }
            else
                _purchasedCars = new() { _currentCar };

            if (PlayerPrefs.HasKey("CompletedRewards"))
            {
                var completedRewardsJson = PlayerPrefs.GetString("CompletedRewards");
                _completedRewards = JsonUtility.FromJson<List<int>>(completedRewardsJson);
            }
            else
                _completedRewards = new();

            if (PlayerPrefs.HasKey("TakenRewards"))
            {
                var takenRewardsJson = PlayerPrefs.GetString("TakenRewards");
                _takenRewards = JsonUtility.FromJson<List<int>>(takenRewardsJson);
            }
            else
                _takenRewards = new();

            if (PlayerPrefs.HasKey("MusicVolume"))
                _musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            else
                _musicVolume = 0.5f;

            if (PlayerPrefs.HasKey("SFXVolume"))
                _sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
            else
                _sfxVolume = 0.5f;
        }

        public static Inventory GetInstance()
        {
            return _instance;
        }

        public int GetMoney()
        {
            return _money;
        }

        public int GetBoosts()
        {
            return _boosts;
        }

        public int GetPunches()
        {
            return _punches;
        }

        public int GetShields()
        {
            return _shields;
        }

        public CarType GetCurrentCar()
        {
            return _currentCar;
        }

        public bool IsPurchasedCar(CarType car)
        {
            return _purchasedCars.Contains(car);
        }

        public int GetCarCount()
        {
            return _purchasedCars.Count;
        }

        public bool IsRewardCompleted(int rewardID)
        {
            return _completedRewards.Contains(rewardID);
        }

        public bool IsRewardTaken(int rewardID)
        {
            return _takenRewards.Contains(rewardID);
        }

        public int GetPlayCount()
        {
            return _playCount;
        }

        public int GetLoseCount()
        {
            return _loseCount;
        }

        public int GetWinCount()
        {
            return _winCount;
        }

        public float GetMusicVolume()
        {
            return _musicVolume;
        }

        public float GetSFXVolume()
        {
            return _sfxVolume;
        }

        public void SetMoney(int money)
        {
            if (money >= 0)
            {
                _money = money;
                PlayerPrefs.SetInt("Money", _money);
                PlayerPrefs.Save();
            }
        }

        public void SetBoosts(int boosts)
        {
            if (boosts >= 0)
            {
                _boosts = boosts;
                PlayerPrefs.SetInt("Boosts", _boosts);
                PlayerPrefs.Save();
            }
        }

        public void SetPunches(int punches)
        {
            if (punches >= 0)
            {
                _punches = punches;
                PlayerPrefs.SetInt("Punches", _punches);
                PlayerPrefs.Save();
            }
        }

        public void SetShields(int shields)
        {
            if (shields >= 0)
            {
                _shields = shields;
                PlayerPrefs.SetInt("Shields", _shields);
                PlayerPrefs.Save();
            }
        }

        public void PurchaseCar(CarType car)
        {
            _purchasedCars.Add(car);
            string json = JsonUtility.ToJson(_purchasedCars);
            PlayerPrefs.SetString("PurchasedCars", json);
            PlayerPrefs.Save();
        }

        public void SetCar(CarType car)
        {
            _currentCar = car;
            string json = JsonUtility.ToJson(_currentCar);
            PlayerPrefs.SetString("CurrentCar", json);
            PlayerPrefs.Save();
        }

        public void CompleteReward(int rewardID)
        {
            _completedRewards.Add(rewardID);
            string json = JsonUtility.ToJson(_completedRewards);
            PlayerPrefs.SetString("CompletedRewards", json);
            PlayerPrefs.Save();
        }

        public void TakeReward(int rewardID)
        {
            _takenRewards.Add(rewardID);
            string json = JsonUtility.ToJson(_takenRewards);
            PlayerPrefs.SetString("TakenRewards", json);
            PlayerPrefs.Save();
        }

        public void IncreasePlayCount()
        {
            _playCount++;
            PlayerPrefs.SetInt("PlayCount", _playCount);
            PlayerPrefs.Save();
        }

        public void IncreaseLoseCount()
        {
            _loseCount++;
            PlayerPrefs.SetInt("LoseCount", _loseCount);
            PlayerPrefs.Save();
        }

        public void IncreaseWinCount()
        {
            _winCount++;
            PlayerPrefs.SetInt("WinCount", _winCount);
            PlayerPrefs.Save();
        }

        public void SetMusicVolume(float volume)
        {
            _musicVolume = volume;
            PlayerPrefs.SetFloat("MusicVolume", _musicVolume);
            PlayerPrefs.Save();
        }

        public void SetSFXVolume(float volume)
        {
            _sfxVolume = volume;
            PlayerPrefs.SetFloat("SFXVolume", _sfxVolume);
            PlayerPrefs.Save();
        }
    }
}