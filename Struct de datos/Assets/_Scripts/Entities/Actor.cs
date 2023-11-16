using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Actor : MonoBehaviour, IDamageable
{
    public class DamageEffects
    {
        public Material[] DefaultMaterials;
    }
    #region IDAMAGEABLE_PROPERTIES

    public int MaxLife => maxLife;
    public int CurrentLife => currentLife;

    #endregion

    #region PROTECTED_PROPERTIES

    protected Rigidbody actorRB;

    #endregion

    #region PRIVATE_PROPERTIES

    [SerializeField] private Material[] _takingDamageMaterial;
    [SerializeField] private AudioSource _takeDamageSource;
    [SerializeField] private int maxLife;
    [SerializeField] private int currentLife;

    private MeshRenderer[] _meshes;
    private List<DamageEffects> _damageEffects = new();
    #endregion

    #region UNITY_METHODS

    protected virtual void Start()
    {
        actorRB = GetComponent<Rigidbody>();
        currentLife = MaxLife;
        _meshes = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < _meshes.Length; i++)
        {
            _damageEffects.Add(new());
            _damageEffects[i].DefaultMaterials=_meshes[i].materials;
        }
    }

    #endregion

    #region IDAMAGEABLE_METHODS

    public void TakeDamage(int damage)
    {
        if (currentLife>0)
        {
            currentLife -= damage;
            if (CurrentLife <= 0)
            {
                Die();
            }
            else
            {
                TakingDamageFX();
            }
        }
    }
    [ContextMenu("TD")]
    private void TakingDamageFX()
    {
        StartCoroutine(TakingDamage());
    }

    private IEnumerator TakingDamage()
    {
        _takeDamageSource.pitch = Random.Range(.85f, 1.2f);
        _takeDamageSource.PlayOneShot(_takeDamageSource.clip);
        
        for (int i = 0; i < _meshes.Length; i++)
        {
            _meshes[i].materials = _takingDamageMaterial;
        }
        yield return new WaitForSeconds(.1f);
        for (int i = 0; i < _meshes.Length; i++)
        {
            _meshes[i].materials = _damageEffects[i].DefaultMaterials;
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    #endregion
}


