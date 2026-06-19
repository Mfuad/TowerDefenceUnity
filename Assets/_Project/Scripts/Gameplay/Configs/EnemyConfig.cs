using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyConfig", menuName = "Config/Enemy")]
public class EnemyConfig: ScriptableObject
{
    [Range(0,100)]
    public float Rarity;
    public int GoldOnDeath;
    public float MoveSpeed;
    public int Damage;
    public int MaxHealth;
    public Sprite Sprite;

}
