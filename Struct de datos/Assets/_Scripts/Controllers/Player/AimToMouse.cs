using UnityEngine;

public class AimToMouse : MonoBehaviour
{
	private Camera _camera;

	private void Awake()
	{
		_camera=Camera.main;
	}

	private void Update ()
	{
		Aim();
	}

    public void Aim()
    {
        Vector3 positionOnScreen = _camera.WorldToViewportPoint(transform.position);

        Vector3 mouseOnScreen = _camera.ScreenToViewportPoint(Input.mousePosition);

        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        transform.rotation = Quaternion.Euler(new Vector3(0f, -angle, 0f));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2((a.y - b.y) * 9, (a.x - b.x) * 16) * Mathf.Rad2Deg;
    }
}

