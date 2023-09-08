using UnityEngine;

public interface IMovable
{
    public float MovementSpeed { get; }

    void Move(Vector3 direction);
}
