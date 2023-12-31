using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    void OnTriggerEnter(Collider other);
    void Pickup(IPickupable item);
}
