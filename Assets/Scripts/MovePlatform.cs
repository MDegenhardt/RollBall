using UnityEngine;
using System.Collections;

public class MovePlatform : MonoBehaviour {

	private Vector3 p0;
	public float speed = 0.1f;
	public float amplitude = 3f;
	private Vector3 direction;
	public int axis = 0;

	void Start(){
		p0 = transform.position;
		direction = Vector3.left;

		switch (axis) {
		case 0:
			direction = Vector3.left;
			break;
		case 1:
			direction = Vector3.up;
			break;
		case 2:
			direction = Vector3.back;
			break;

		}

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 offset =  amplitude * direction * Mathf.Sin(6.28f * speed * Time.time);
		transform.position = p0 + offset;
	}
}
