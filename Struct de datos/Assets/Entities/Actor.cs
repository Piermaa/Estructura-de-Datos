using UnityEngine;

public class Actor : MonoBehaviour, IDamageable, IMovable
{
    #region IDAMAGEABLE_PROPERTIES
    public int MaxLife => maxLife;
    public int CurrentLife => currentLife;
    #endregion

    #region IMOVABLE_PROPERTIES
    public float MovementSpeed => movementSpeed;
    #endregion
    
    #region PRIVATE_PROPERTIES
    [SerializeField] private int maxLife;
    [SerializeField] private int currentLife;
    [SerializeField] private float movementSpeed;
    #endregion

    #region UNITY_METHODS

    protected void Start()
    {
        currentLife = MaxLife;
        print(currentLife);
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

    public void Die()
    {
        Destroy(gameObject);
    }
    #endregion

    #region IMOVABLE_METHODS
    public void Move(Vector3 direction)
    {
        transform.position += direction * (MovementSpeed * Time.deltaTime);
    }
    #endregion
}

