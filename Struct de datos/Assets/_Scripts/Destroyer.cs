using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private float timer;
    [SerializeField] private float cooldownToDestroy;

    private void Awake()
    {
        timer = cooldownToDestroy;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
