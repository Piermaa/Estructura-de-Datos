using UnityEngine;

public class AimToMouse : MonoBehaviour
{
	// Update is called once per frame
	private Camera _main;

	private void Awake()
	{
		_main=Camera.main;
	}

	private void Update ()
	{
		Aim();
	}

	private void Aim()
	{
		//Get the Screen positions of the object
		Vector2 positionOnScreen = _main.WorldToViewportPoint (transform.position);
		
		//Get the Screen position of the mouse
		Vector2 mouseOnScreen = _main.ScreenToViewportPoint(Input.mousePosition);
		
		//Get the angle between the points
		float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

		//Ta Da
		transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,angle));
	}

	private float AngleBetweenTwoPoints(Vector3 a, Vector3 b) 
	{
		return Mathf.Atan2((a.y - b.y) * 9, (a.x - b.x) * 16) * Mathf.Rad2Deg;
	}
}

