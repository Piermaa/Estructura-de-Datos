using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsUI : MonoBehaviour
{
    [SerializeField] private WeaponHolder playerHolder;

    [SerializeField] private LayoutGroup weaponsUIPanel;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddWeaponToUI(IWeapon addedWeapon)
    {
        Instantiate(addedWeapon.GameObject, weaponsUIPanel.transform);
    }
    
    void DeleteWeaponFromUI(IWeapon deletedWeapon)
    {
        
    }
}
