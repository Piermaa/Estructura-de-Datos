using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubFusil : Weapon
{
    //################ #################
    //----------CLASS METHODS-----------
    //################ #################

    //--------IWEAPON-----------

    public override void Shoot(WeaponHolder weaponHolder)
    {
        if (remainingBullets > 0)
        {
            print("nada implementado aun");
            //remainingBullets--;
            //print(this.gameObject.name + " disparo pew pew me quedan " + remainingBullets + " balas");
        }
        else
        {
            //print(this.gameObject.name + " se me acabaron las balas entonces descarto el arma porque ya no sirve mas");
            //OnWeaponMagazineEmpty();
        }
    }
}
