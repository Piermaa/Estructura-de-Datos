using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform camFollowTarget;
    [SerializeField] private float camMoveSpeed = 3.0f;
    [SerializeField] private float camZoom = 9.5f;

    private void FixedUpdate()
    {
        if (camFollowTarget != null)
        {
            float interpolation = camMoveSpeed * Time.deltaTime;
            Vector3 position = transform.position;
            position.z = Mathf.Lerp(transform.position.z, camFollowTarget.position.z, interpolation);
            position.x = Mathf.Lerp(transform.position.x, camFollowTarget.position.x, interpolation);
            position.y = camZoom;
            transform.position = position;
        }
    }
}
