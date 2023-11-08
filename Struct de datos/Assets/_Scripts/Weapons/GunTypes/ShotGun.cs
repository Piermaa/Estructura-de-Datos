using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon
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
            _audioSource.Play();
            for (int i = 0; i < weaponHolder.EquippedWeapon.WeaponStats.BulletsPerShot; i++)
            {
                Vector3 spread = Random.insideUnitSphere * 1;

                //LA PRIMER BALA SIEMPRE AL CENTRO
                if (i == 0)
                    spread = Vector3.zero;

                Vector3 randomShellPosition = new Vector3(transform.position.x + spread.x, transform.position.y, 
                    transform.position.z + spread.z);
                Bullet bullet = (Bullet)weaponHolder.EquippedWeaponBulletPool.TryGetPooledObject(randomShellPosition, Quaternion.identity);
                bullet.InitBullet(this, transform.forward);
            }
     //       print(this.gameObject.name + " disparo pew pew me quedan " + remainingBullets + " balas");
        }
        else
        {
       //     print(this.gameObject.name + " se me acabaron las balas entonces descarto el arma porque ya no sirve mas");
            OnWeaponMagazineEmpty();
        }
    }
}
