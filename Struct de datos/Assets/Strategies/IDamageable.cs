public interface IDamageable
{
    public int MaxLife { get; }
    public int CurrentLife { get; }

    void TakeDamage(int damage);
    void Die();
}
