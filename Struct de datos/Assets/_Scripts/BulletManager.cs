using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
   [SerializeField]
   private List<Bullet> _bullets = new();

   public void AddBullet(Bullet bullet)
   {
      _bullets.Add(bullet);
   }
   
   public void RemoveBullet(Bullet bullet)
   {
      _bullets.Remove(bullet);
   }
}
