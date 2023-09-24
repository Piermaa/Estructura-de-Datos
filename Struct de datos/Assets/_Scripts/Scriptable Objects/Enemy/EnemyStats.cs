using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private EnemyStatsStruct _stats;

    public int MaxLife => _stats.MaxLife; //No se usa, ahora mismo usa la del actor
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
