using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private EnemyStatsStruct _stats;

    public int Damage => _stats.Damage;
    public int DifficultyLevel => _stats.DifficultyLevel;
    public float Speed => _stats.Speed;

    [System.Serializable]
    public struct EnemyStatsStruct
    {
        public int Damage;
        public int DifficultyLevel;
        public float Speed;
    }
}
