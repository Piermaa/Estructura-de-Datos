using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---COMO FUNCA: CADA WEAPON HEREDA DE IWEAPON (QUE ES UN IPICKUPABLE). EN SU ONTRIGGER ENTER LLAMA A SU PICKUP, QUE ESTA SIENDO ESCUCHADO
//---POR EL WEAPON HOLDER. AHI SE AGREGA EN LA PILA, Y DE PASO INICIALIZA EL POOL DE BALAS PARA LA ARMA LEVANTADA. EL SHOOTINGCONTROLLER
//---SE FIJA EN EL WEAPONHOLDER A VER QUE ARMA HAY, Y CUANDO EL JUGADOR DA LA ORDEN PARA DISPARAR, LLAMA AL SHOOT DEL ARMA QUE ESTA AHI DENTRO
//---EL ARMA PROPIA MANEJA SU PROPIA LOGICA DE DISPARO Y MANEJA COMO LA BALA SE OBTIENE DEL POOL QUE ESTA EN EL WEAPONHOLDER QUE LA CONTIENE (AL ARMA).
//---EL WEAPONHOLDER ESTA ESCUCHANDO AL MOMENTO DE QUE ACABEN LAS BALAS, AHI FLETA EL ARMA: SE DESEQUIPA, SE SALE DE LA PILA, DESTRUYE EL
//---EL POOL Y SE DESUSCRIBE DE ESA ARMA.


[RequireComponent(typeof(WeaponHolder))]
public class ShootingController : MonoBehaviour
{
    [SerializeField] private WeaponsUI _weaponsUI;
    private WeaponHolder weaponHolder;
    private float shootCooldownTimer = 0;
    
    //################ #################
    //----------UNITY EV FUNC-----------
    //################ #################
    private void Start()
    {
        weaponHolder = GetComponent<WeaponHolder>();
    }

    private void Update()
    {
        shootCooldownTimer += Time.deltaTime;
        ListenForShootInput();
    }

    //################ #################
    //----------CLASS METHODS-----------
    //################ #################

    private void ListenForShootInput()
    {
        if (Input.GetButton("Fire1"))
        {
            if (weaponHolder.EquippedWeapon != null)
                CallWeaponShoot();
            else
                print("No tenes un arma equipado wacho.");
        }
    }

    private void CallWeaponShoot()
    {
        if (shootCooldownTimer >= weaponHolder.EquippedWeapon.WeaponStats.FireRate 
            && weaponHolder.EquippedWeaponBulletPool.IsPoolInited)
        {
            weaponHolder.EquippedWeapon.Shoot(weaponHolder);
            _weaponsUI.UpdateBulletsText(weaponHolder.EquippedWeapon.RemainingBullets);
            shootCooldownTimer = 0;
        }
    }
}
