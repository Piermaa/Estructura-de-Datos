using UnityEngine;

namespace ScriptsEnemies.Entities
{
    public class Actor : MonoBehaviour, IDamageable, IMovable
    {
        #region IDAMAGEABLE_PROPERTIES
        public int MaxLife => maxLife;
        public int CurrentLife => currentLife;
        public float MovementSpeed => movementSpeed;
        #endregion

        #region PRIVATE_VARIABLES
        
        #endregion

        #region PRIVATE_PROPERTIES
        [SerializeField] private int maxLife;
        [SerializeField] private int currentLife;
        [SerializeField] private float movementSpeed;
        #endregion

        #region UNITY_METHODS

        protected virtual void Start()
        {
            currentLife = MaxLife;
            print(currentLife);
        }

        public virtual void Update()
        {
            Move();
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
        public void Move()
        {
            // Calculate inputs
            float xMovement = Input.GetAxis("Horizontal");
            float yMovement = Input.GetAxis("Vertical");

            // Calculate movement
            transform.Translate(Time.deltaTime * xMovement * movementSpeed * Vector3.right, Space.World);

            transform.Translate(Time.deltaTime * yMovement * movementSpeed * new Vector3(0, 0, 1), Space.World);
        }
        #endregion
    }
}

