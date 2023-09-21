using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Actor : MonoBehaviour, IDamageable
{
    #region IDAMAGEABLE_PROPERTIES

    public int MaxLife => maxLife;
    public int CurrentLife => currentLife;

    #endregion

    #region PROTECTED_PROPERTIES

    protected Rigidbody actorRB;

    #endregion

    #region PRIVATE_PROPERTIES

    [SerializeField] private int maxLife;
    [SerializeField] private int currentLife;

    #endregion

    #region UNITY_METHODS

    protected virtual void Start()
    {
        actorRB = GetComponent<Rigidbody>();
        currentLife = MaxLife;
    }

    #endregion

    #region IDAMAGEABLE_METHODS

    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        if (CurrentLife <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    #endregion
}


