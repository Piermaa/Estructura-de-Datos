using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsUI : MonoBehaviour
{
    private Image _lastPickedUp;
    
    private PilaTF<Image> _pilaImagenesPlaceHolder;
    [SerializeField] private Image imagePlaceHolder;
    
    public void InitializeUI(int pileLength)
    {
        _pilaImagenesPlaceHolder = new PilaTF<Image>();
        _pilaImagenesPlaceHolder.Init(pileLength);
    }

    public void AddWeaponToUI(GameObject addedWeapon)
    {
        if (_lastPickedUp!=null)
        {
            _pilaImagenesPlaceHolder.Add(_lastPickedUp);
        }
        
        _lastPickedUp = Instantiate(imagePlaceHolder, transform);
        
        _lastPickedUp.name = addedWeapon.GameObject().name;
    }

    public void DeleteWeaponFromUI()
    {
        Destroy(_lastPickedUp.gameObject); //
        _lastPickedUp =  _pilaImagenesPlaceHolder.Pop();
    }
}
