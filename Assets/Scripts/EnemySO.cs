using UnityEngine;

[CreateAssetMenu(fileName = "New EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public Enemy prefab;
    public float rarity;
}
