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
            _audioSource.Play();
            remainingBullets--;
            Bullet bullet = (Bullet)weaponHolder.EquippedWeaponBulletPool.TryGetPooledObject(transform.position, transform.rotation);
            bullet.InitBullet(this, transform.forward);
        }
        else
        {
            OnWeaponMagazineEmpty();
        }
    }
}
