using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour
{
    public int DiscSize => _size;
    [SerializeField] private int _size;

    public Tower TowerOwner
    {
        get { return _towerOwner; }
        set { _towerOwner = value; }
    }
    private Tower _towerOwner;

}
