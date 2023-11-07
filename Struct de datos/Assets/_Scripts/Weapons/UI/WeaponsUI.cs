using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsUI : MonoBehaviour
{
    [SerializeField] private Image imagePlaceHolder;
    private Image _equippedWeapon;
    
    private PilaTF<Image> _pilaImagenesPlaceHolder;
    
    public void InitializeUI(int pileLength)
    {
        _pilaImagenesPlaceHolder = new PilaTF<Image>();
        _pilaImagenesPlaceHolder.Init(pileLength);
    }

    public void AddWeaponToUI(Sprite addedWeaponSprite)
    {
        _equippedWeapon = Instantiate(imagePlaceHolder, transform);
        _equippedWeapon.sprite = addedWeaponSprite;
        _pilaImagenesPlaceHolder.Add(_equippedWeapon);
    }

    public void DeleteWeaponFromUI()
    {
        Destroy(_equippedWeapon.gameObject);
        _equippedWeapon =  _pilaImagenesPlaceHolder.Pop();
    }
}
