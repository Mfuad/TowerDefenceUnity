using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Splines;

[CreateAssetMenu(fileName = "New LevelConfig", menuName = "Config/Level")]
public class LevelConfig : ScriptableObject
{
    public Guid Id = Guid.NewGuid();
    public string Name;
    public int MaxHealth;
    public SplineContainer Path;
    public AssetReferenceT<SceneAsset> LevelScene;
    public EnemyConfig[] EnemyTypes;
}



