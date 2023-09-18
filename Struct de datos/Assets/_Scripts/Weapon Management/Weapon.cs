using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    public Transform WeaponTransform => transform;
    public int RemainingBullets => remainingBullets;
    public int Damage => damage;
    public float BulletTravelDistance => bulletTravelDistance;
    
    [SerializeField]private int damage;
    [SerializeField]private float bulletTravelDistance;
    [SerializeField]private LayerMask enemyLayer;
    [SerializeField]private Transform origin;
    
    private int remainingBullets=10;

    private void Update()
    {
        Debug.DrawRay(transform.position,transform.up*bulletTravelDistance,Color.yellow);
    }
/// <summary>
/// Check collision using raycast
/// </summary>
/// <param name="direction">Direction given by parameter, made for shotguns or low precision weapons</param>
/// <param name="hit">Hitted collider</param>
/// <returns>True if raycast hits</returns>
    protected bool TraceBullet(Vector3 direction, out RaycastHit hit)
    {
        return (Physics.Raycast(origin.position, direction, out hit, bulletTravelDistance, enemyLayer));
    }
/// <summary>
/// Checks by raycast if an enemy must be hit. Deals damage using weapon data
/// </summary>
    public virtual void Shoot()
    {
        if (remainingBullets > 0)
        {
            remainingBullets--;
            if (TraceBullet(transform.up, out var hit))
            {
                print(hit.collider.name);
               // hit.transform.GetComponent<Health>().TakeDamage(damage);
            }
        }
        else
        {
            //TODO: Avisar de alguna forma que ya no hay mas balas y desequipar este arma
        }
    }
}
