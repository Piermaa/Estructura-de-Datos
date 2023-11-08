using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPistol : Weapon
{
    //################ #################
    //----------CLASS METHODS-----------
    //################ #################

    //--------IWEAPON-----------

    public override void Shoot(WeaponHolder weaponHolder)
    {
        _audioSource.Play();
        Bullet bullet = (Bullet)weaponHolder.EquippedWeaponBulletPool.TryGetPooledObject(transform.position, transform.rotation);
        bullet.InitBullet(this, transform.forward);
//        print(this.gameObject.name + " disparo pew pew nunca se me acaban las balas porque eso me dijeron que haga");
    }
}
