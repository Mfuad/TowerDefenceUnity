using UnityEngine;

namespace TowerDefense.Assets._Project.Scripts.Gameplay.Configs
{
    [CreateAssetMenu(fileName = "WaveConfig", menuName = "Scriptable Objects/WaveConfig")]
    public class WaveConfig : ScriptableObject
    {
        public int MaxWave;
        public float WaveCooldown;
        public float EnemySpawnRate;
    }
}
