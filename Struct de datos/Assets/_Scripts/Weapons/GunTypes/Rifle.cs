using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    //################ #################
    //----------CLASS METHODS-----------
    //################ #################

    //--------IWEAPON-----------

    public override void Shoot(WeaponHolder weaponHolder)
    {
        if (remainingBullets > 0)
        {
            remainingBullets--;
            Bullet bullet = (Bullet)weaponHolder.EquippedWeaponBulletPool.TryGetPooledObject();
            bullet.InitBullet(transform.forward);
            print(this.gameObject.name + " disparo pew pew me quedan " + remainingBullets + " balas");
        }
        else
        {
            print(this.gameObject.name + " se me acabaron las balas entonces descarto el arma porque ya no sirve mas");
            OnWeaponMagazineEmpty();
        }
    }
}

