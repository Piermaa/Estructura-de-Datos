using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    [SerializeField] private Actor actor;
    private Image _hpBar;
    void Start()
    {
        _hpBar = GetComponent<Image>();
    }
    
    void Update()
    {
        _hpBar.fillAmount = (float)actor.CurrentLife / actor.MaxLife;
    }
}
