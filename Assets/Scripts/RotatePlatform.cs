using UnityEngine;
using System.Collections;

public class RotatePlatform : MonoBehaviour {

	public float speed = 0.3f;
	public int axis = 0;

	// Update is called once per frame
	void Update () {
		switch (axis) {
		case 0:
			transform.Rotate (new Vector3 (speed * 90, 0, 0) * Time.deltaTime);
			break;
		case 1:
			transform.Rotate (new Vector3 (0, speed * 90, 0) * Time.deltaTime);
			break;
		case 2:
			transform.Rotate (new Vector3 (0, 0, speed*90)*Time.deltaTime);
			break;

		}
//		transform.Rotate (new Vector3 (0, speed*90, 0)*Time.deltaTime);
	}
}
