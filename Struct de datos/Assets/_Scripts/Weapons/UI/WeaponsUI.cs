using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsUI : MonoBehaviour
{
    private PilaTF<GameObject> _weaponsInUI;
    private Transform weaponsUIPanel;

    private PilaTF<Image> _pilaImagenesPlaceHolder;
    [SerializeField] private Image imagePlaceHolder;
    
    public void InitializeUI(int pileLength)
    {
        _weaponsInUI = new PilaTF<GameObject>();
        _weaponsInUI.Init(pileLength);
        _pilaImagenesPlaceHolder = new PilaTF<Image>();
        _pilaImagenesPlaceHolder.Init(pileLength);
        weaponsUIPanel = GetComponent<Transform>();
    }

    public void AddWeaponToUI(GameObject addedWeapon)
    {
        // GameObject obj = Instantiate(addedWeapon, weaponsUIPanel);
        // if (obj != null)
        // {
        //     _weaponsInUI.Add(obj);
        // }
        Image img = Instantiate(imagePlaceHolder, weaponsUIPanel);
        Debug.Log($"Añado {img} a la UI");
        _pilaImagenesPlaceHolder.Add(img);
    }
    
    public void DeleteWeaponFromUI()
    {
        // GameObject obj = _weaponsInUI.Peek();
        // Destroy(obj);

        GameObject img = _pilaImagenesPlaceHolder.Peek().gameObject;
        Debug.Log($"Elimino {img} de la UI");
        Destroy(img);
        _pilaImagenesPlaceHolder.Pop();
    }
}
