using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private EnemyStatsStruct _stats;

    public int MaxLife => _stats.MaxLife;
    public int Damage => _stats.Damage;
    public int DifficultyLevel => _stats.DifficultyLevel;

    [System.Serializable]
    public struct EnemyStatsStruct
    {
        public int MaxLife;
        public int Damage;
        public int DifficultyLevel;
    }
}
