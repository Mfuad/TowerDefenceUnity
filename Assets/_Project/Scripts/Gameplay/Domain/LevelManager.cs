using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TowerDefense.Assets._Project.Scripts.Utilities;
using VContainer.Unity;

namespace TowerDefense.Assets._Project.Scripts.Gameplay.Domain
{
    public class LevelManager
    {
        private int _health;
        private int _balance;
        private int _totalKill;
        public event Action<int> OnBalanceChanged;
        public event Action OnGameOver;

        private ConfigProvider _configProvider;
        private LevelConfig _config;
        public string CurrentLevel => _config.Name;


        public LevelManager(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _config = await _configProvider.Get<LevelConfig>(Config.Level, "Level_001");
        }


        public void OnEnemyKilled(int goldOnKill)
        {
            _totalKill++;
            _balance += goldOnKill;
            OnBalanceChanged?.Invoke(_balance);
        }

        public bool TrySpendGold(int amount)
        {
            if (_balance < amount) return false;

            _balance -= amount;
            OnBalanceChanged?.Invoke(_balance);
            return true;
        }
    }
}
