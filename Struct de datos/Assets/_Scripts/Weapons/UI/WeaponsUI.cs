using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsUI : MonoBehaviour
{
    [SerializeField] private Image imagePlaceHolder;
    private Image _lastPickedUp;
    
    private PilaTF<Image> _pilaImagenesPlaceHolder;
    
    public void InitializeUI(int pileLength)
    {
        _pilaImagenesPlaceHolder = new PilaTF<Image>();
        _pilaImagenesPlaceHolder.Init(pileLength);
    }

    public void AddWeaponToUI(Sprite addedWeaponSprite)
    {
        if (_lastPickedUp!=null)
        {
            _pilaImagenesPlaceHolder.Add(_lastPickedUp);
        }
        
        _lastPickedUp = Instantiate(imagePlaceHolder, transform);
        _lastPickedUp.sprite = addedWeaponSprite;
        // _lastPickedUp.name = addedWeapon.GameObject().name;
    }

    public void DeleteWeaponFromUI()
    {
        Destroy(_lastPickedUp.gameObject); //
        _lastPickedUp =  _pilaImagenesPlaceHolder.Pop();
    }
}
