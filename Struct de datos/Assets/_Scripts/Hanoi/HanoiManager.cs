using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HanoiManager : MonoBehaviour
{
    public Disc[] GameDiscs => _discs;
    [SerializeField] private Disc[] _discs;

    private Disc _currentSelectedDisc;
    private Tower _currentSelectedDiscTowerOwner;

    //bien croto porque paja
    public static HanoiManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void OnDiscClick(Disc clickedDisc)
    {
        _currentSelectedDisc = clickedDisc;
        _currentSelectedDiscTowerOwner = clickedDisc.TowerOwner;
        print("click" + clickedDisc.name);
    }

    public void OnTowerClick(Tower clickedTower)
    {
        print("click" + clickedTower.name);

        if (_currentSelectedDisc == null)
            print("No hay disco selected");
        else
        {
            clickedTower.TryPlaceDiscInNewTower(_currentSelectedDiscTowerOwner, _currentSelectedDisc);
        }
    }
}
